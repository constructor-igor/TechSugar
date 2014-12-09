using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Hadoop.MapReduce;
using Microsoft.Hadoop.WebClient.WebHCatClient;

//
//
//  References:
//  - http://blogs.msdn.com/b/data_otaku/archive/2013/09/07/hadoop-for-net-developers-implementing-a-simple-mapreduce-job.aspx

namespace HadoopMapReduceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //establish job configuration
            var myConfig = new HadoopJobConfiguration {InputPath = "/demo/simple/in", OutputFolder = "/demo/simple/out"};

            //connect to cluster
            var myUri = new Uri("http://localhost");
            const string userName = "hadoop";
            string passWord = null;
            IHadoop myCluster = Hadoop.Connect(myUri, userName, passWord);

            //execute mapreduce job
            MapReduceResult jobResult = myCluster.MapReduceJob.Execute<MySimpleMapper, MySimpleReducer>(myConfig);

            //write job result to console
            int exitCode = jobResult.Info.ExitCode;

            string exitStatus = exitCode == 0 ? "Success" : "Failure";
            exitStatus = exitCode + " (" + exitStatus + ")";

            Console.WriteLine();
            Console.Write("Exit Code = " + exitStatus);
            Console.Read();
        }
    }

    public class MySimpleMapper : MapperBase
    {
        public override void Map(string inputLine, MapperContext context)
        {
            //interpret the incoming line as an integer value
            int value = int.Parse(inputLine);
            //determine whether value is even or odd
            string key = (value % 2 == 0) ? "even" : "odd";
            //output key assignment with value
            context.EmitKeyValue(key, value.ToString(CultureInfo.InvariantCulture));
        }
    }

    public class MySimpleReducer : ReducerCombinerBase
    {
        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            //initialize counters
            int myCount = 0;
            int mySum = 0;

            //count and sum incoming values
            foreach (string value in values)
            {
                mySum += int.Parse(value);
                myCount++;
            }

            //output results
            context.EmitKeyValue(key, myCount + "\t" + mySum);
        }
    }
}
