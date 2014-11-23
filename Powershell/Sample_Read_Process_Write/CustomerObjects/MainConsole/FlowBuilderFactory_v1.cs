using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Customer.DataProcessing;
using Customer.TextData;

namespace MainConsole
{
    public class FlowBuilderFactory_v1
    {
        readonly List<IDataflowBlock> flow = new List<IDataflowBlock>();

        public TransformBlock<string, List<string>> GetFiles(string searchPattern)
        {
            var loadDataFromFileBlock = new TransformBlock<string, List<string>>(pathToFiles =>
            {
                string[] files = Directory.GetFiles(pathToFiles, searchPattern, SearchOption.AllDirectories);
                return new List<string>(files);
            });
            flow.Add(loadDataFromFileBlock);
            return loadDataFromFileBlock;
        }

        public TransformBlock<List<string>, List<CustomerTextData>> LoadDataList()
        {
            var loadDataFromFileBlock = new TransformBlock<List<string>, List<CustomerTextData>>(fileItems =>
            {
                var factory = new CustomerTextDataFactory();
                return new List<CustomerTextData>(fileItems.ConvertAll(factory.LoadFromFile));
            });
            flow.Add(loadDataFromFileBlock);
            return loadDataFromFileBlock;
        }

        public TransformBlock<List<CustomerTextData>, List<CustomerTextData>> Filter(int minimumSymbols)
        {
            var filterBlock = new TransformBlock<List<CustomerTextData>, List<CustomerTextData>>(textDataList =>
            {
                var filter = new FilterTextData(minimumSymbols);
                return textDataList.Where(filter.Run).ToList();
            });
            flow.Add(filterBlock);
            return filterBlock;
        }

        public TransformManyBlock<List<T>, T> ToList<T>()
        {
            var toListBlock = new TransformManyBlock<List<T>, T>(textDataList =>
            {
                var queue = new ConcurrentQueue<T>();
                textDataList.ForEach(queue.Enqueue);
                return queue;
            });
            flow.Add(toListBlock);
            return toListBlock;
        }

        public ActionBlock<CustomerTextData>  Process()
        {
            var action = new ActionBlock<CustomerTextData>(textData =>
            {
                var weight = new WeightTextData();
                int result = weight.Run(textData);
                Trace.WriteLine(result);
                Console.WriteLine(result);
            });
            flow.Add(action);
            return action;
        }

        public void SequentialContinueWith()
        {
            for (int i = 0; i < flow.Count - 1; i++)
            {
                IDataflowBlock nextBlock = flow[i + 1];
                flow[i].Completion.ContinueWith(t =>
                {
                    if (t.IsFaulted) nextBlock.Fault(t.Exception);
                    else nextBlock.Complete();
                });
            }
        }
    }
}