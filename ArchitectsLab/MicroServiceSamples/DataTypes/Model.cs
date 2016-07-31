using System;

namespace MicroServiceSamples.DataTypes
{
    public class Model : IModel
    {
        public Model(string modelId)
        {
            ModelId = modelId;
        }

        #region IModel
        public string ModelId { get; private set; }

        public string Parameters1
        {
            get
            {
                Console.WriteLine("Model.Parameters1()");
                return "parameters1";
            }
        }
        #endregion
    }
}