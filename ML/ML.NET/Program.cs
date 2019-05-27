using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

/*
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.ml.standardtrainerscatalog.sdca?view=ml-dotnet
    https://docs.microsoft.com/en-us/dotnet/machine-learning/how-to-guides/load-data-ml-net
 */

namespace ML.NET
{

    public class FormulaData{
        [ColumnName("Features")]
        [VectorType(1)]
        public float[] X;
        [ColumnName("Label")]
        public float Y;
        public FormulaData(double x, double y){
            X = new float[]{Convert.ToSingle(x)};
            Y = Convert.ToSingle(y);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World 'Microsoft.ML'!");

            List<FormulaData> pointsValues = Enumerable
                .Range(-1, 100)
                .Select(value => {return new FormulaData(value, value*2-1);})
                .ToList();

            // Create MLContext
            var mlContext = new MLContext(1);

            // Load Data
            IDataView data = mlContext.Data.LoadFromEnumerable<FormulaData>(pointsValues);

            DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            IDataView trainData = dataSplit.TrainSet;
            IDataView testData = dataSplit.TestSet;

            // Define trainer options.
            var options = new SdcaRegressionTrainer.Options
            {
                LabelColumnName = "Label", //nameof(FormulaData.Y),
                FeatureColumnName = "Features", //nameof(FormulaData.X),
                // Make the convergence tolerance tighter. It effectively leads to more training iterations.
                ConvergenceTolerance = 0.02f,
                // Increase the maximum number of passes over training data. Similar to ConvergenceTolerance,
                // this value specifics the hard iteration limit on the training algorithm.
                MaximumNumberOfIterations = 30,
                // Increase learning rate for bias.
                BiasLearningRate = 0.1f            
            };

            // Define StochasticDualCoodrinateAscent regression algorithm estimator
            var sdcaEstimator = mlContext.Regression.Trainers.Sdca(options);

            // Build machine learning model
            var trainedModel = sdcaEstimator.Fit(trainData);

            // Use trained model to make inferences on test data
            IDataView testDataPredictions = trainedModel.Transform(testData);

            // Extract model metrics and get RSquared
            RegressionMetrics trainedModelMetrics = mlContext.Regression.Evaluate(testDataPredictions);
            double rSquared = trainedModelMetrics.RSquared;

            Console.WriteLine($"rSquared: {rSquared}");
        }
    }
}
