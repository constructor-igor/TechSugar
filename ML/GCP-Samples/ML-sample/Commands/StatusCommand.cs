using System;
using Google.Apis.Prediction.v1_6.Data;
using ML_sample.Interfaces;

namespace ML_sample.Commands
{
    public class StatusCommand : ICommand
    {
        private readonly PredictionFramework m_predictionFramework;

        public StatusCommand(PredictionFramework predictionFramework, ProjectModelId projectModelId)
        {
            m_predictionFramework = predictionFramework;
            ProjectModelId = projectModelId;
            Command = "status";
        }

        #region ICommand
        public ProjectModelId ProjectModelId { get; private set; }
        public string Command { get; private set; }
        public void Run()
        {
            Insert2 trainingStatus = m_predictionFramework.GetModelStatus(ProjectModelId);
            Console.WriteLine("Training status: {0}", trainingStatus.TrainingStatus);
            ToConsole(trainingStatus.ModelInfo);
        }
        #endregion
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