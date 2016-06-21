using System;
using System.Threading;
using Google.Apis.Prediction.v1_6.Data;
using ML_sample.Interfaces;

namespace ML_sample.Commands
{
    public class TrainingCommand : ICommand
    {
        const int PROGRESS_WAITING_TIME = 1000;
        private readonly PredictionFramework m_predictionFramework;
        private readonly string m_storageData;
        public ProjectModelId ProjectModelId { get; private set; }
        public string Command { get; private set; }

        public TrainingCommand(PredictionFramework predictionFramework, ProjectModelId projectModelId, string storageData)
        {
            m_predictionFramework = predictionFramework;
            m_storageData = storageData;
            ProjectModelId = projectModelId;
            Command = "training";
        }
        public void Run()
        {
            Console.WriteLine("Model '{0}' deleted with response '{1}'.", ProjectModelId.ModelId, m_predictionFramework.DeleteTrainedModel(ProjectModelId));
            Insert2 insertResponse = m_predictionFramework.TrainRegressionModel(ProjectModelId, m_storageData);
            Console.WriteLine("Inserted the training data for the model.");

            // Wait until the training is complete
            bool trainingRunning = true;
            while (trainingRunning)
            {
                Console.WriteLine("Getting a new training progress status...");
                var getResponse = m_predictionFramework.GetModelStatus(ProjectModelId);
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