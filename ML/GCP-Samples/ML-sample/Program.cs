using System;
using System.Threading;
using Google.Apis.Prediction.v1_6.Data;

namespace ML_sample
{
    class Program
    {
        const string AUTH_JSON_FILE = @"D:\@Temp\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json";
        const string MODEL_ID = "20000-test";
        //const string PROJECT_ID = "har-machine-learning-1333";
        const string PROJECT_NUMBER = "1024476357063";
        const string STRORAGE_DATA = "good-test-case/testGood2.csv";
        const int PROGRESS_WAITING_TIME = 1000;

        static void Main(string[] args)
        {
            PredictionFramework predictionFramework = new PredictionFramework();
            predictionFramework.CreatePredictionService(AUTH_JSON_FILE);
            ProjectModelId projectModelId = new ProjectModelId(PROJECT_NUMBER, MODEL_ID);
            string command = "status";

            Console.WriteLine("Project number: {0}, Model ID: {1}", projectModelId.PojectNumber, projectModelId.ModelId);
            Console.WriteLine("Command: {0}", command);
            
            switch (command)
            {
                case "status":
                    Insert2 trainingStatus = predictionFramework.GetModelStatus(projectModelId);
                    Console.WriteLine("Training status: {0}", trainingStatus.TrainingStatus);
                    Insert2.ModelInfoData stausModelInfoData = trainingStatus.ModelInfo;
                    if (stausModelInfoData != null)
                    {
                        Console.WriteLine("ModelType: {0}", stausModelInfoData.ModelType);
                        Console.WriteLine("NumberInstances: {0}", stausModelInfoData.NumberInstances);
                        Console.WriteLine("MeanSquaredError: {0}", stausModelInfoData.MeanSquaredError);
                    }
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
                                Insert2.ModelInfoData modelInfoData = getResponse.ModelInfo;
                                if (modelInfoData != null)
                                {
                                    Console.WriteLine("ModelType: {0}", modelInfoData.ModelType);
                                    Console.WriteLine("NumberInstances: {0}", modelInfoData.NumberInstances);
                                    Console.WriteLine("MeanSquaredError: {0}", modelInfoData.MeanSquaredError);
                                }
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
    }
}
