using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Google.Apis.Prediction.v1_6;
using Google.Apis.Prediction.v1_6.Data;
using ML_sample.Interfaces;

namespace ML_sample.Commands
{
    public class Point
    {
        public double X;
        public double Y;
    }
    public class PredictCommand : ICommand
    {
        private readonly PredictionFramework m_predictionFramework;
        private readonly string m_predictValuesFile;

        public PredictCommand(PredictionFramework predictionFramework, ProjectModelId projectModelId, string predictValuesFile)
        {
            m_predictionFramework = predictionFramework;
            m_predictValuesFile = predictValuesFile;
            ProjectModelId = projectModelId;
            Command = "predict";
        }

        #region Command
        public ProjectModelId ProjectModelId { get; private set; }
        public string Command { get; private set; }
        public void Run()
        {
//            List<string> predictValues = File
//                .ReadAllLines(m_predictValuesFile)
//                .Select(line => line.Split(',')[1]).ToList();
//            IList<object> list = predictValues.Cast<object>().Take(30).ToArray();
            //IList<object> list = "288.9336446,399.7982794,1053.802145,1645.373715,499.709547,77.98642255".Split(',');
            List<double> originalY = new List<double>();

            Random random = new Random(12345);
            IEnumerable<Point> evaluatedRandomPoints = Enumerable
                .Range(1, 10)
                .Select(index =>
                {
                    double x = ConvertToRadians(360*random.NextDouble());
                    double y = Math.Sin(x);
                    Point point = new Point {X = x, Y = y};
                    return point;
                });

            IEnumerable<Point> evaluatedFilePoints = File
                .ReadAllLines(m_predictValuesFile)
                .Select(line =>
                {
                    string[] xyItems = line.Split(',');
                    double x = Double.Parse(xyItems[1]);
                    double y = Double.Parse(xyItems[0]);
                    Point point = new Point {X = x, Y = y};
                    return point;
                })
                .Skip(100)
                .Take(10);

            List<double> evaluated = evaluatedRandomPoints
                .Select(value =>
                {
                    originalY.Add(value.Y);

                    IList<object> list = new object[] {value.X.ToString(CultureInfo.InvariantCulture)};
                    Input predictBody = new Input {InputValue = new Input.InputData {CsvInstance = list}};
                    TrainedmodelsResource.PredictRequest predictRequest = m_predictionFramework.PredictionService.Trainedmodels.Predict(predictBody, ProjectModelId.ProjectNumber, ProjectModelId.ModelId);
                    Output predictResponse = predictRequest.Execute();
                    string responseValue = predictResponse.OutputValue;

                    Console.WriteLine("X: {0}, Y: {1}, response: {2}", value.X, value.Y, responseValue);

                    return responseValue;
                })
                .Select(Double.Parse)
                .ToList();

            double error = originalY
                .Select((valueY, index) => Math.Pow(valueY - evaluated[index], 2))
                .Sum();
            error = Math.Sqrt(error);
            Console.WriteLine("error: {0}", error);

//            Input predictBody = new Input { InputValue = new Input.InputData { CsvInstance = list } };
//
//            TrainedmodelsResource.PredictRequest predictRequest = m_predictionFramework.PredictionService.Trainedmodels.Predict(predictBody, ProjectModelId.ProjectNumber, ProjectModelId.ModelId);
//            Output predictResponse = predictRequest.Execute();
//            Console.WriteLine("predict output value: {0}", predictResponse.OutputValue);
        }
        #endregion
        public double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}