using ML_sample.Interfaces;

namespace ML_sample.Commands
{
    public class PredictCommand : ICommand
    {
        private readonly PredictionFramework m_predictionFramework;

        public PredictCommand(PredictionFramework predictionFramework, ProjectModelId projectModelId)
        {
            m_predictionFramework = predictionFramework;
            ProjectModelId = projectModelId;
            Command = "predict";
        }

        #region Command
        public ProjectModelId ProjectModelId { get; private set; }
        public string Command { get; private set; }
        public void Run()
        {
            //                    IList<object> list = "288.9336446,399.7982794,1053.802145,1645.373715,499.709547,77.98642255".Split(',');
            //                    Input predictBody = new Input {InputValue = new Input.InputData {CsvInstance = list}};
            //
            //                    TrainedmodelsResource.PredictRequest predictRequest = predictionFramework.PredictionService.Trainedmodels.Predict(predictBody, projectModelId.ProjectNumber, projectModelId.ModelId);
            //                    Output predictResponse = predictRequest.Execute();
            //                    Console.WriteLine("predict output value: {0}", predictResponse.OutputValue);
        }
        #endregion
    }
}