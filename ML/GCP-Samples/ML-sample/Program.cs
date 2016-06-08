using System;
using System.Threading;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;
using Google.Apis.Services;

namespace ML_sample
{
    class Program
    {
        const string PROJECT_ID = "har-machine-learning-1333";
        const string PROJECT_NUMBER = "1024476357063";
        const string STRORAGE_DATA = "good-test-case/testGood2.csv";
        const int PROGRESS_WAITING_TIME = 1000;

        static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"D:\@Temp\GCP_ML_Feasibility\HAR Machine Learning-41ae33f89eac.json");
            var credentials = Google.Apis.Auth.OAuth2.GoogleCredential.GetApplicationDefaultAsync().Result;
            if (credentials.IsCreateScopedRequired)
            {
                credentials = credentials.CreateScoped(PredictionService.Scope.DevstorageFullControl, PredictionService.Scope.Prediction);
            }
            var serviceInitializer = new BaseClientService.Initializer()
            {
                ApplicationName = "Prediction Sample",
                HttpClientInitializer = credentials
            };

            PredictionService predictionService = new PredictionService(serviceInitializer);

            var deleteRequest = predictionService.Trainedmodels.Delete(PROJECT_NUMBER, PROJECT_ID);
            string deleteResponse = deleteRequest.Execute();

            Insert insertBody = new Insert
            {
                StorageDataLocation = STRORAGE_DATA, /*StoragePMMLLocation = STRORAGE_DATA,*/ Id = PROJECT_ID, ModelType = "REGRESSION"            
            };
            TrainedmodelsResource.InsertRequest insertRequest = predictionService.Trainedmodels.Insert(insertBody, PROJECT_NUMBER);
            Insert2 insertResponse = insertRequest.Execute();            
            Console.WriteLine("Inserted the training data for the model.");
            Console.WriteLine("Training status: " + insertResponse.TrainingStatus);

            // Wait until the training is complete
            bool trainingRunning = true;
            while (trainingRunning)
            {
                Console.WriteLine("Getting a new training progress status...");
                var getRequest = predictionService.Trainedmodels.Get(PROJECT_NUMBER, PROJECT_ID);
                var getResponse = getRequest.Execute();
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

            //predictionService.Trainedmodels.Predict(body, PROJECT_ID, Analyze.ModelDescriptionData)
        }
    }
}
