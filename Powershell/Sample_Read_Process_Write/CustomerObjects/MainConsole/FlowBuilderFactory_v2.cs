using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Customer.DataProcessing;
using Customer.TextData;

namespace MainConsole
{
    public class FlowBuilderFactory_v2
    {
        public TransformManyBlock<PathToFolder, PathToFile> InputFilesGeneration(string searchPattern)
        {
            var block = new TransformManyBlock<PathToFolder, PathToFile>(pathToFolder =>
            {
                string[] files = Directory.GetFiles(pathToFolder.Folder, searchPattern, SearchOption.AllDirectories);
                var queue = new ConcurrentQueue<PathToFile>();
                Array.ForEach(files, file => queue.Enqueue(new PathToFile(file)));
                return queue;
            });
            return block;
        }

        public TransformBlock<PathToFile, BuilderCustomerData> ToCustomData()
        {
            var block = new TransformBlock<PathToFile, BuilderCustomerData>(pathToFile =>
            {
                var factory = new CustomerTextDataFactory();
                return new BuilderCustomerData { PathToFile = pathToFile, CustomerData = factory.LoadFromFile(pathToFile.File) };
            });
            return block;
        }

        public TransformBlock<BuilderCustomerData, BuilderCustomerData> Filter(int minimumSymbols)
        {
            var block = new TransformBlock<BuilderCustomerData, BuilderCustomerData>(data =>
            {
                var filter = new FilterTextData(minimumSymbols);
                bool enabled = filter.Run(data.CustomerData);
                return new BuilderCustomerData
                {
                    PathToFile = data.PathToFile,
                    Enabled = enabled,
                    CustomerData = data.CustomerData
                };
            });
            return block;
        }

        public TransformBlock<BuilderCustomerData, ItemReport> Processing()
        {
            var block = new TransformBlock<BuilderCustomerData, ItemReport>(data =>
            {
                var weight = new WeightTextData();
                int result = weight.Run(data.CustomerData);
                return new ItemReport { Enabled = true, FilePath = data.PathToFile.File, Weight = result };
            });
            return block;
        }
        public TransformBlock<BuilderCustomerData, ItemReport> ToIgnore()
        {
            var block = new TransformBlock<BuilderCustomerData, ItemReport>(data => new ItemReport { Enabled = false, FilePath = data.PathToFile.File });
            return block;
        }

        public BatchBlock<ItemReport> ToReportItem()
        {
            var block = new BatchBlock<ItemReport>(int.MaxValue);
            return block;
        }

        public TransformBlock<ItemReport[], ProcessingReport> ToReport()
        {
            var block = new TransformBlock<ItemReport[], ProcessingReport>(items =>
            {
                var processingReport = new ProcessingReport();
                foreach (ItemReport itemReport in items)
                {
                    processingReport.ReportItems.Add(itemReport);
                }
                return processingReport;
            });
            return block;
        }

        public ActionBlock<ProcessingReport> ReportToFile(PathToFile pathToReportFile)
        {
            var block = new ActionBlock<ProcessingReport>(report =>
            {
                var helper = new ProcessingReportHelper();
                helper.WriteTo(report, pathToReportFile.File);
            });
            return block;
        }
        public ActionBlock<T> ToConsole<T>()
        {
            var action = new ActionBlock<T>(item => Console.WriteLine("thread: {0}, item: {1}", Thread.CurrentThread.ManagedThreadId, item));
            return action;
        }

        public void ContinueWith(IDataflowBlock source, IDataflowBlock next)
        {
            source.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) next.Fault(t.Exception);
                else next.Complete();
            });
        }
    }

}