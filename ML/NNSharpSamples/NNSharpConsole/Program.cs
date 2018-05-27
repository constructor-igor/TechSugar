using NNSharp.DataTypes;
using NNSharp.IO;
using NNSharp.Models;

namespace NNSharpConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string kerasModelFile = "";
            Sample1(kerasModelFile);
        }

        public static void Sample1(string filePath)
        {
            // Read the previously created json.
            var reader = new ReaderKerasModel(filePath);
            SequentialModel model = reader.GetSequentialExecutor();

            // Then create the data to run the executer on.
            // batch: should be set in the Keras model.
            int height = 0;
            int width = 0;
            int channel = 0;
            int batch = 0;
            Data2D input = new Data2D(height, width, channel, batch);

            // Calculate the network's output.
            IData output = model.ExecuteNetwork(input);
        }
    }
}
