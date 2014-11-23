using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using Customer.DataProcessing;
using Customer.TextData;

namespace MainConsole
{
    class Program
    {
        static void Main()
        {
            //ProcessingByTPL_StraightForwardImplementation();
            //ProcessingByTPL_BlocksImplementation_v1();
            ProcessingByTPL_BlocksImplementation_v2();
        }

        static public void ProcessingByTPL_StraightForwardImplementation()
        {
            const string pathToFiles = @"..\..\..\..\DataFiles";
            string[] files = Directory.GetFiles(pathToFiles, "*.txt", SearchOption.AllDirectories);

            var loadDataFromFileBlock = new TransformBlock<string[], List<CustomerTextData>>(fileItems =>
            {
                var factory = new CustomerTextDataFactory();
                return new List<CustomerTextData>(Array.ConvertAll(fileItems, factory.LoadFromFile));
            });
            var filterBlock = new TransformBlock<List<CustomerTextData>, List<CustomerTextData>>(textDataList =>
            {
                var filter = new FilterTextData(5);
                return textDataList.Where(filter.Run).ToList();
            });
            var toListBlock = new TransformManyBlock<List<CustomerTextData>, CustomerTextData>(textDataList =>
            {
                var queue = new ConcurrentQueue<CustomerTextData>();
                textDataList.ForEach(queue.Enqueue);
                return queue;
            });
            var action = new ActionBlock<CustomerTextData>(textData =>
            {
                var weight = new WeightTextData();
                int result = weight.Run(textData);
                Trace.WriteLine(result);
                Console.WriteLine(result);
            });

            loadDataFromFileBlock.LinkTo(filterBlock);
            filterBlock.LinkTo(toListBlock);
            toListBlock.LinkTo(action);

            loadDataFromFileBlock.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) ((IDataflowBlock)filterBlock).Fault(t.Exception);
                else filterBlock.Complete();
            });
            filterBlock.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) ((IDataflowBlock)toListBlock).Fault(t.Exception);
                else toListBlock.Complete();
            });
            toListBlock.Completion.ContinueWith(t =>
            {
                if (t.IsFaulted) ((IDataflowBlock)action).Fault(t.Exception);
                else action.Complete();
            });

            loadDataFromFileBlock.Post(files);
            loadDataFromFileBlock.Complete();
            action.Completion.Wait();
        }

        static public void ProcessingByTPL_BlocksImplementation_v1()
        {
            const string pathToFiles = @"..\..\..\..\DataFiles";

            var factory = new FlowBuilderFactory_v1();

            var getFilesBlock = factory.GetFiles("*.txt");
            var loadDataBlock = factory.LoadDataList();
            var filterBlock = factory.Filter(5);
            var toListBlock = factory.ToList<CustomerTextData>();
            var actionBlock = factory.Process();

            getFilesBlock.LinkTo(loadDataBlock);
            loadDataBlock.LinkTo(filterBlock);
            filterBlock.LinkTo(toListBlock);
            toListBlock.LinkTo(actionBlock);

            factory.SequentialContinueWith();

            getFilesBlock.Post(pathToFiles);
            getFilesBlock.Complete();
            actionBlock.Completion.Wait();
        }

        static public void ProcessingByTPL_BlocksImplementation_v2()
        {
            const string pathToFiles = @"..\..\..\..\DataFiles";
            const string pathToReport = @"..\..\..\..\DataFiles\report.report";

            var factory = new FlowBuilderFactory_v2();

            var inputGenerator = factory.InputFilesGeneration("*.txt");
            var toCustomData = factory.ToCustomData();
            var filter = factory.Filter(5);
            var processing = factory.Processing();
            var toIgnore = factory.ToIgnore();
            var batchBlock = factory.ToReportItem();
            var toReport = factory.ToReport();
            var reportToFile = factory.ReportToFile(new FlowBuilderFactory_v2.PathToFile { Path = pathToReport });
            var toConsole = factory.ToConsole<object>();

            inputGenerator.LinkTo(toCustomData);
            toCustomData.LinkTo(filter);
            filter.LinkTo(processing, data=>data.Enabled);
            filter.LinkTo(toIgnore, data=>!data.Enabled);
            processing.LinkTo(batchBlock);
            toIgnore.LinkTo(batchBlock);
            batchBlock.LinkTo(toReport);
            toReport.LinkTo(reportToFile);
            //toReport.LinkTo(toConsole);

            factory.ContinueWith(inputGenerator, toCustomData);
            factory.ContinueWith(toCustomData, filter);
            factory.ContinueWith(filter, processing);
            factory.ContinueWith(filter, toIgnore);
            factory.ContinueWith(processing, batchBlock);
            factory.ContinueWith(toIgnore, batchBlock);
            factory.ContinueWith(batchBlock, toReport);
            factory.ContinueWith(toReport, reportToFile);
            //factory.ContinueWith(toReport, toConsole);

            inputGenerator.Post(new FlowBuilderFactory_v2.PathToFolder {Folder = pathToFiles});
            inputGenerator.Complete();
            reportToFile.Completion.Wait();
        }

        public class FlowBuilderFactory_v2
        {
            public class PathToFolder
            {
                public string Folder { get; set; }
            }
            public class PathToFile
            {
                public string Path { get; set; }
            }

            public class BuilderCustomerData
            {
                public PathToFile PathToFile { get; set; }
                public bool Enabled { get; set; }
                public CustomerTextData CustomerData { get; set; }
            }

            public TransformManyBlock<PathToFolder, PathToFile> InputFilesGeneration(string searchPattern)
            {
                var block = new TransformManyBlock<PathToFolder, PathToFile>(pathToFolder =>
                {
                    string[] files = Directory.GetFiles(pathToFolder.Folder, searchPattern, SearchOption.AllDirectories);
                    var queue = new ConcurrentQueue<PathToFile>();
                    Array.ForEach(files, file => queue.Enqueue(new PathToFile {Path=file}));
                    return queue;
                });
                return block;
            }

            public TransformBlock<PathToFile, BuilderCustomerData> ToCustomData()
            {
                var block = new TransformBlock<PathToFile, BuilderCustomerData>(pathToFile =>
                {
                    var factory = new CustomerTextDataFactory();
                    return new BuilderCustomerData {PathToFile = pathToFile, CustomerData = factory.LoadFromFile(pathToFile.Path)};
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
                    return new ItemReport {Enabled = true, FilePath = data.PathToFile.Path, Weight = result};
                });
                return block;
            }
            public TransformBlock<BuilderCustomerData, ItemReport> ToIgnore()
            {
                var block = new TransformBlock<BuilderCustomerData, ItemReport>(data => new ItemReport { Enabled = false, FilePath = data.PathToFile.Path});
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
                    helper.WriteTo(report, pathToReportFile.Path);
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
}
