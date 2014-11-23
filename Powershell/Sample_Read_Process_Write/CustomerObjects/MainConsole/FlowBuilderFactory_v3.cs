using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Customer.DataProcessing;
using Customer.TextData;

namespace MainConsole
{
    public class FlowBuilderFactory_v3
    {
        readonly public List<IDataflowBlock> Flow = new List<IDataflowBlock>();

        public FlowBuilderFactory_v3 DataFromFolder(string searchPattern)
        {
            var block = new TransformManyBlock<object, object>(pathToFolderObject =>
            {
                var pathToFolder = (PathToFolder)pathToFolderObject;
                string[] files = Directory.GetFiles(pathToFolder.Folder, searchPattern, SearchOption.AllDirectories);
                var queue = new ConcurrentQueue<PathToFile>();
                Array.ForEach(files, file => queue.Enqueue(new PathToFile(file)));
                return queue;
            });
            Flow.Add(block);
            return this;
        }
        public FlowBuilderFactory_v3 LoadData()
        {
            var block = new TransformBlock<object, object>(pathToFileObject =>
            {
                var pathToFile = (PathToFile)pathToFileObject;
                var factory = new CustomerTextDataFactory();
                return new BuilderCustomerData { PathToFile = pathToFile, CustomerData = factory.LoadFromFile(pathToFile.File) };
            });
            Flow.Add(block);
            return this;
        }
        public FlowBuilderFactory_v3 ToConsole()
        {
            var action = new ActionBlock<object>(dataObject => Console.WriteLine("thread: {0}, object={1}", Thread.CurrentThread.ManagedThreadId, dataObject));
            Flow.Add(action);
            return this;
        }
        public FlowBuilderFactory_v3 Filter(int minimumSymbols)
        {
            var block = new TransformBlock<object, object>(dataObject =>
            {
                var data = (BuilderCustomerData)dataObject;
                var filter = new FilterTextData(minimumSymbols);
                bool enabled = filter.Run(data.CustomerData);
                return new BuilderCustomerData
                {
                    PathToFile = data.PathToFile,
                    Enabled = enabled,
                    CustomerData = data.CustomerData
                };
            });
            Flow.Add(block);
            return this;
        }
        public FlowBuilderFactory_v3 Processing()
        {
            var block = new TransformBlock<object, object>(dataObject =>
            {
                var data = (BuilderCustomerData)dataObject;
                if (data.Enabled)
                {
                    var weight = new WeightTextData();
                    int weightResult = weight.Run(data.CustomerData);
                    return new ItemReport { Enabled = true, FilePath = data.PathToFile.File, Weight = weightResult };
                }
                return new ItemReport { Enabled = false, FilePath = data.PathToFile.File };
            });
            Flow.Add(block);
            return this;
        }
        public FlowBuilderFactory_v3 ToReport()
        {
            var batchBlock = new BatchBlock<object>(int.MaxValue);
            Flow.Add(batchBlock);

            var block = new TransformBlock<object, object>(dataObject =>
            {
                //var items = (ItemReport[]) dataObject;
                var items = Array.ConvertAll((object[])dataObject, dataItem => dataItem as ItemReport);
                var processingReport = new ProcessingReport();
                foreach (ItemReport itemReport in items)
                {
                    processingReport.ReportItems.Add(itemReport);
                }
                return processingReport;
            });
            Flow.Add(block);
            return this;
        }
        public FlowBuilderFactory_v3 ReportToFile(PathToFile pathToReportFile)
        {
            var block = new ActionBlock<object>(reportObject =>
            {
                var report = (ProcessingReport)reportObject;
                var helper = new ProcessingReportHelper();
                helper.WriteTo(report, pathToReportFile.File);
            });
            Flow.Add(block);
            return this;
        }

        public FlowBuilderFactory_v3 CreateSequentialFlow()
        {
            for (int i = 0; i < Flow.Count - 1; i++)
            {
                var source = Flow[i] as ISourceBlock<object>;
                var next = Flow[i + 1] as ITargetBlock<object>;
                source.LinkTo(next);
                source.Completion.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        next.Fault(t.Exception);
                    else
                        next.Complete();
                });
            }
            return this;
        }
        public FlowBuilderFactory_v3 Post<T>(T inputData)
        {
            var startBlock = Flow[0] as ITargetBlock<T>;
            startBlock.Post(inputData);
            return this;
        }
        public void Wait()
        {
            var startBlock = Flow.First() as ITargetBlock<object>;
            var finishBlock = Flow.Last() as ITargetBlock<object>;
            startBlock.Complete();
            finishBlock.Completion.Wait();
        }
    }
}