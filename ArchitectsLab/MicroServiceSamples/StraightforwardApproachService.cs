using MicroServiceSamples.DataTypes;
using MicroServiceSamples.Infra;

namespace MicroServiceSamples
{
    public class StraightforwardApproachService: IService
    {
        #region IService
        public string Execute(string modelId)
        {
            object r1 = new Aktion1().Execute(modelId);
            object r2 = new Aktion2().Execute(modelId);
            return string.Format("main({0}, {1})", r1, r2);
        }
        #endregion
    }

    public class Aktion1
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return string.Format("S1({0})", model.Parameters1);
        }
    }
    public class Aktion2
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            string parameters1 = model.Parameters1;

            object r2A = new Aktion2A().Execute(modelId);
            object r2B = new Aktion2B().Execute(modelId);
            return string.Format("S2({0}, {1}, {2})", parameters1, r2A, r2B);
        }
    }

    public class Aktion2A
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return string.Format("S2A({0}:{1})", modelId, model.Parameters1);
        }
    }
    public class Aktion2B
    {
        public object Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            return string.Format("S2B({0})", model.Parameters1);
        }
    }
}