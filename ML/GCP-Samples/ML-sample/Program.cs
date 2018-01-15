using System;
using ML_sample.Commands;
using ML_sample.Interfaces;

namespace ML_sample
{
    class Program
    {
////                const string AUTH_JSON_FILE = @"D:\@Temp\ML_Feasibility\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json";
//                const string AUTH_JSON_FILE = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\google\HAR Machine Learning-2a2ea6fd74e3.json";
//                private const string PROJECT_NUMBER = "1024476357063";
//                private const string modelId = "sin-360-sanity";
//                private const string storageData = "sanity-test-cases/sin-360-training-data.csv";
//                private const string predictFile = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\sin-360-training-data.csv";

        const string AUTH_JSON_FILE = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\google\HAR Machine Learning-2a2ea6fd74e3.json";
        private const string PROJECT_NUMBER = "1024476357063";
        private const string modelId = "data1x1p7";
        private const string storageData = "sanity-test-cases/data_1_gcp_training_70.csv";
        private const string predictFile = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\data_1_gcp_predict_30.csv";


        //        //const string AUTH_JSON_FILE = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\google\data1p1x7-b93dbc2c7b4b.json";
        //        private const string AUTH_JSON_FILE = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\google\HAR Machine Learning-2a2ea6fd74e3.json";
        //        private const string PROJECT_NUMBER = "1024476357063";
        //        private const string modelId = "data1x1p7";
        //        private const string storageData = "sanity-test-cases/data_1_gcp.csv";
        ////        private const string predictFile = @"D:\@Temp\@Issues\2018-01-10_Igor_ML\data\data_1_gcp_predict.csv";

        static void Main()
        {
            PredictionFramework predictionFramework = new PredictionFramework();
            predictionFramework.CreatePredictionService(AUTH_JSON_FILE);
            ProjectModelId projectModelId = new ProjectModelId(PROJECT_NUMBER, modelId);
            //ICommand command = new TrainingCommand(predictionFramework, projectModelId, storageData);
            //ICommand command = new StatusCommand(predictionFramework, projectModelId);
            //ICommand command = new AnalysisCommand(predictionFramework, projectModelId);
            ICommand command = new PredictCommand(predictionFramework, projectModelId, predictFile);

            Console.WriteLine("Project number: {0}, Model ID: {1}", projectModelId.ProjectNumber, projectModelId.ModelId);
            Console.WriteLine("Command: {0}", command.Command);

            command.Run();
        }
    }
}
