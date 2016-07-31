namespace MicroServiceSamples.DataTypes
{
    public class Model : IModel
    {
        public readonly string ModelId;
        public Model(string modelId)
        {
            ModelId = modelId;
        }
    }
}