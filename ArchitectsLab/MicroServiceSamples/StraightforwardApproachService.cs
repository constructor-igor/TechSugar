using MicroServiceSamples.DataTypes;
using MicroServiceSamples.Infra;

namespace MicroServiceSamples
{
    public class StraightforwardApproachService: IService
    {
        #region IService
        public void Execute(string modelId)
        {
            object r1 = new Aktion1().Execute(modelId);
            object r2 = new Aktion2().Execute(modelId);
        }
        #endregion
    }

    public class Aktion1
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return new object();
        }
    }
    public class Aktion2
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            object r2A = new Aktion2A().Execute(modelId);
            object r2B = new Aktion2A().Execute(modelId);
            return new object();
        }
    }

    public class Aktion2A
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return new object();
        }
    }
    public class Aktion2B
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return new object();
        }
    }
}