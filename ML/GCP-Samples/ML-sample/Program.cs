using System;
using System.Collections.Generic;
using System.Threading;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;

namespace ML_sample
{
    class Program
    {
        const string AUTH_JSON_FILE = @"D:\@Temp\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json";
        const string MODEL_ID = "20000-test";   // user-defined unique name for model
        //const string PROJECT_ID = "har-machine-learning-1333";
        const string PROJECT_NUMBER = "1024476357063";
        const string STRORAGE_DATA = "good-test-case/testGood2.csv";
        const int PROGRESS_WAITING_TIME = 1000;

        static void Main(string[] args)
        {
            PredictionFramework predictionFramework = new PredictionFramework();
            predictionFramework.CreatePredictionService(AUTH_JSON_FILE);
            ProjectModelId projectModelId = new ProjectModelId(PROJECT_NUMBER, MODEL_ID);
            string command = "predict";

            Console.WriteLine("Project number: {0}, Model ID: {1}", projectModelId.PojectNumber, projectModelId.ModelId);
            Console.WriteLine("Command: {0}", command);
            
            switch (command)
            {
                case "status":
                    Insert2 trainingStatus = predictionFramework.GetModelStatus(projectModelId);
                    Console.WriteLine("Training status: {0}", trainingStatus.TrainingStatus);
                    ToConsole(trainingStatus.ModelInfo);
                    break;
                case "analyze":
                    TrainedmodelsResource.AnalyzeRequest analyzeRequest = predictionFramework.PredictionService.Trainedmodels.Analyze(projectModelId.PojectNumber, projectModelId.ModelId);
                    Analyze analyzeResponse = analyzeRequest.Execute();
                    break;                    
                case "predict":
                    IList<object> list = new List<object>();
                    //list.Add("270.1945417,402.4902572,1341.938939,1532.260464,372.4266548,81.66415048");
                    list.Add("270.1945417");
                    list.Add("402.4902572");
                    list.Add("1341.938939");
                    list.Add("1532.260464");
                    list.Add("372.4266548");
                    list.Add("81.66415048");
                    Input predictBody = new Input {InputValue = new Input.InputData {CsvInstance = list}};

                    TrainedmodelsResource.PredictRequest predictRequest = predictionFramework.PredictionService.Trainedmodels.Predict(predictBody, projectModelId.PojectNumber, projectModelId.ModelId);
                    Output predictResponse = predictRequest.Execute();
                    Console.WriteLine("predict output value: {0}", predictResponse.OutputValue);
                    break;
                case "training":
                    predictionFramework.DeleteTrainedModel(projectModelId);
                    Insert2 insertResponse = predictionFramework.TrainRegressionModel(projectModelId, STRORAGE_DATA);
                    Console.WriteLine("Inserted the training data for the model.");
                    // Wait until the training is complete
                    bool trainingRunning = true;
                    while (trainingRunning)
                    {
                        Console.WriteLine("Getting a new training progress status...");
                        var getResponse = predictionFramework.GetModelStatus(projectModelId);
                        Console.WriteLine("Got a new training progress status: {0}", getResponse.TrainingStatus);

                        switch (getResponse.TrainingStatus)
                        {
                            case "RUNNING":
                                Console.WriteLine("The model training is still in progress, let us wait for {0} ms.", PROGRESS_WAITING_TIME);
                                Thread.Sleep(PROGRESS_WAITING_TIME);
                                break;
                            case "DONE":
                                Console.WriteLine("The model has been trained successfully.");
                                ToConsole(getResponse.ModelInfo);
                                trainingRunning = false;
                                break;
                            case "ERROR: TRAINING JOB NOT FOUND":
                                throw new Exception("the training job was not found.");
                            case "ERROR: TOO FEW INSTANCES IN DATASET":
                                throw new Exception("there are too few instances in the dataset.");
                            default:
                                throw new ArgumentException("Unknown status (error): " + getResponse.TrainingStatus);
                        }
                    }
                    break;
            }
            //predictionService.Trainedmodels.Predict(body, PROJECT_ID, Analyze.ModelDescriptionData)
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
