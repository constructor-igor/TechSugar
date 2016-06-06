using System;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;
using Google.Apis.Services;

namespace ML_sample
{
    class Program
    {
        const string PROJECT_ID = "har-machine-learning-1333";
        const string PROJECT_NUMBER = "1024476357063";
        const string STRORAGE_DATA = "good-test-case/testGood2.txt";

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

            Insert trainInsert = new Insert { StorageDataLocation = STRORAGE_DATA, StoragePMMLLocation = STRORAGE_DATA, Id = PROJECT_ID };
            TrainedmodelsResource.InsertRequest insertRequest = predictionService.Trainedmodels.Insert(trainInsert, PROJECT_NUMBER);
            Insert2 insertResponse = insertRequest.Execute();

            //predictionService.Trainedmodels.Predict(body, PROJECT_ID, Analyze.ModelDescriptionData)
        }
    }
}
