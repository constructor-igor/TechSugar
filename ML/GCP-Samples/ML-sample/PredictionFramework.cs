using System;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;
using Google.Apis.Services;

namespace ML_sample
{
    public class ProjectModelId
    {
        public readonly string PojectNumber;
        public readonly string ModelId;

        public ProjectModelId(string pojectNumber, string modelId)
        {
            PojectNumber = pojectNumber;
            ModelId = modelId;
        }
    }
    public class PredictionFramework
    {
        public PredictionService PredictionService; 
        public void CreatePredictionService(string authJsonFile)
        {
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", authJsonFile);
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

            PredictionService = new PredictionService(serviceInitializer);
        }

        public string DeleteTrainedModel(ProjectModelId projectModelId)
        {
            var deleteRequest = PredictionService.Trainedmodels.Delete(projectModelId.PojectNumber, projectModelId.ModelId);
            string deleteResponse = deleteRequest.Execute();
            return deleteResponse;
        }

        public Insert2 TrainRegressionModel(ProjectModelId projectModelId, string csvDataPathInStorage)
        {
            Insert insertBody = new Insert
            {
                StorageDataLocation = csvDataPathInStorage,
                Id = projectModelId.ModelId,
                ModelType = "REGRESSION"
            };
            TrainedmodelsResource.InsertRequest insertRequest = PredictionService.Trainedmodels.Insert(insertBody, projectModelId.PojectNumber);
            Insert2 insertResponse = insertRequest.Execute();
            return insertResponse;
        }

        public Insert2 GetModelStatus(ProjectModelId projectModelId)
        {
            var getRequest = PredictionService.Trainedmodels.Get(projectModelId.PojectNumber, projectModelId.ModelId);
            Insert2 response = getRequest.Execute();
            return response;
        }
    }
}