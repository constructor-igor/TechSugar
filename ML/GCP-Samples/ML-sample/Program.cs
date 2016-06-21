using System;
using System.Collections.Generic;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;
using ML_sample.Commands;
using ML_sample.Interfaces;

namespace ML_sample
{
    class Program
    {
        const string AUTH_JSON_FILE = @"D:\@Temp\ML_Feasibility\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json";
        const string MODEL_ID = "20000-test";   // user-defined unique name for model
        const string PROJECT_NUMBER = "1024476357063";
        const string STORAGE_DATA = "good-test-case/testGood2.csv";
        const int PROGRESS_WAITING_TIME = 1000;

        static void Main(string[] args)
        {
            PredictionFramework predictionFramework = new PredictionFramework();
            predictionFramework.CreatePredictionService(AUTH_JSON_FILE);
            ProjectModelId projectModelId = new ProjectModelId(PROJECT_NUMBER, "sin-360-sanity");
            //ICommand command = new TrainingCommand(predictionFramework, projectModelId, "sanity-test-cases/sin-360-training-data.csv");
            //ICommand command = new StatusCommand(predictionFramework, projectModelId);
            ICommand command = new AnalysisCommand(predictionFramework, projectModelId);

            Console.WriteLine("Project number: {0}, Model ID: {1}", projectModelId.ProjectNumber, projectModelId.ModelId);
            Console.WriteLine("Command: {0}", command.Command);

            command.Run();
            
//            switch (command)
//            {
//                case "predict":
//                    //IList<object> list = "270.1945417,402.4902572,1341.938939,1532.260464,372.4266548,81.66415048".Split(',');
//                    IList<object> list = "288.9336446,399.7982794,1053.802145,1645.373715,499.709547,77.98642255".Split(',');
//                    Input predictBody = new Input {InputValue = new Input.InputData {CsvInstance = list}};
//
//                    TrainedmodelsResource.PredictRequest predictRequest = predictionFramework.PredictionService.Trainedmodels.Predict(predictBody, projectModelId.ProjectNumber, projectModelId.ModelId);
//                    Output predictResponse = predictRequest.Execute();
//                    Console.WriteLine("predict output value: {0}", predictResponse.OutputValue);
//                    break;
//            }
//            //predictionService.Trainedmodels.Predict(body, PROJECT_ID, Analyze.ModelDescriptionData)
        }

        private static void ToConsole(Insert2.ModelInfoData modelInfoData)
        {
            if (modelInfoData != null)
            {
                Console.WriteLine("ModelType: {0}", modelInfoData.ModelType);
                Console.WriteLine("NumberInstances: {0}", modelInfoData.NumberInstances);
                Console.WriteLine("MeanSquaredError: {0}", modelInfoData.MeanSquaredError);
            }
        }
    }
}
