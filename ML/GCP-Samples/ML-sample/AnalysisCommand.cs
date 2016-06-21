using System;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;

namespace ML_sample
{
    public class AnalysisCommand : ICommand
    {
        private readonly PredictionFramework m_predictionFramework;

        public AnalysisCommand(PredictionFramework predictionFramework, ProjectModelId projectModelId)
        {
            m_predictionFramework = predictionFramework;
            ProjectModelId = projectModelId;
            Command = "Analysis";
        }

        public ProjectModelId ProjectModelId { get; private set; }
        public string Command { get; private set; }
        public void Run()
        {
            TrainedmodelsResource.AnalyzeRequest analyzeRequest = m_predictionFramework.PredictionService.Trainedmodels.Analyze(ProjectModelId.ProjectNumber, ProjectModelId.ModelId);
            Analyze response = analyzeRequest.Execute();

            Console.WriteLine("analysis response:");
            foreach (Analyze.DataDescriptionData.FeaturesData feature in response.DataDescription.Features)
            {
                Console.WriteLine("text='{0}', count={1}, mean={2}, variance={3}", feature.Text, feature.Numeric.Count, feature.Numeric.Mean, feature.Numeric.Variance);
            }
        }
    }
}