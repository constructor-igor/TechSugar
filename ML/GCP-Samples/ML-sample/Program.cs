using System;
using ML_sample.Commands;
using ML_sample.Interfaces;

namespace ML_sample
{
    class Program
    {
        const string AUTH_JSON_FILE = @"D:\@Temp\ML_Feasibility\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json";
        const string PROJECT_NUMBER = "1024476357063";

        static void Main()
        {
            PredictionFramework predictionFramework = new PredictionFramework();
            predictionFramework.CreatePredictionService(AUTH_JSON_FILE);
            ProjectModelId projectModelId = new ProjectModelId(PROJECT_NUMBER, "sin-360-sanity");
            //ICommand command = new TrainingCommand(predictionFramework, projectModelId, "sanity-test-cases/sin-360-training-data.csv");
            //ICommand command = new StatusCommand(predictionFramework, projectModelId);
            //ICommand command = new AnalysisCommand(predictionFramework, projectModelId);
            ICommand command = new PredictCommand(predictionFramework, projectModelId, @"D:\@Temp\ML_Feasibility\Tech.Sugar-ML-in-Cloud\GCPDemo\predict-data.csv");

            Console.WriteLine("Project number: {0}, Model ID: {1}", projectModelId.ProjectNumber, projectModelId.ModelId);
            Console.WriteLine("Command: {0}", command.Command);

            command.Run();
        }
    }
}
