using MicroServiceSamples.DataTypes;

namespace MicroServiceSamples
{
    public class StandardApproachService: IService
    {
        public string Execute(string modelId)
        {
            return modelId;
        }
    }
}