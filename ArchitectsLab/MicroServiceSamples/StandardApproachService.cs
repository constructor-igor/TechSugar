using MicroServiceSamples.DataTypes;
using MicroServiceSamples.Infra;

namespace MicroServiceSamples
{
    public class StandardApproachService : IService
    {
        #region IService
        public string Execute(string modelId)
        {
            IModel model = Helper.LoadModel(modelId);
            object r1 = new Aktion1().Execute(model);
            object r2 = new Aktion2().Execute(model);
            return string.Format("main({0}, {1})", r1, r2);
        }
        #endregion
        public class Aktion1
        {
            public object Execute(IModel model)
            {
                return string.Format("S1({0})", model.Parameters1);
            }
        }
        public class Aktion2
        {
            public object Execute(IModel model)
            {
                object r2A = new Aktion2A().Execute(model);
                object r2B = new Aktion2B().Execute(model);
                return string.Format("S2({0}, {1})", r2A, r2B);
            }
        }
        public class Aktion2A
        {
            public object Execute(IModel model)
            {
                return string.Format("S2A({0}:{1})", model.ModelId, model.Parameters1);
            }
        }
        public class Aktion2B
        {
            public object Execute(IModel model)
            {
                return string.Format("S2B({0})", model.Parameters1);
            }
        }
    }
}