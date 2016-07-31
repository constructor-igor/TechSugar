namespace MicroServiceSamples.DataTypes
{
    public class Model : IModel
    {
        public Model(string modelId)
        {
            ModelId = modelId;
            Parameters1 = "parameters1";
        }

        #region IModel
        public string ModelId { get; private set; }
        public string Parameters1 { get; private set; }
        #endregion
    }
}