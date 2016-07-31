using System.Collections.Generic;
using MicroServiceSamples.DataTypes;
using MicroServiceSamples.Infra;

namespace MicroServiceSamples
{
    public class MicroServicesApproachService : IService
    {
        #region IService
        public string Execute(string modelId)
        {
            object r1 = new Aktion1().Execute(modelId);
            object r2 = new Aktion2().Execute(modelId);
            return string.Format("main({0}, {1})", r1, r2);
        }
        #endregion
        public class Aktion1
        {
            public object Execute(string modelId)
            {
                return string.Format("S1({0})", MicroServices.GetModelParametersService.GetParameters(modelId));
            }
        }
        public class Aktion2
        {
            public object Execute(string modelId)
            {
                object r2A = new Aktion2A().Execute(modelId);
                object r2B = new Aktion2B().Execute(modelId);
                return string.Format("S2({0}, {1}, {2})", modelId, r2A, r2B);
            }
        }
        public class Aktion2A
        {
            public object Execute(string modelId)
            {
                return string.Format("S2A({0}:{1})", modelId, MicroServices.GetModelParametersService.GetParameters(modelId));
            }
        }
        public class Aktion2B
        {
            public object Execute(string modelId)
            {
                return string.Format("S2B({0})", MicroServices.GetModelParametersService.GetParameters(modelId));
            }
        }
    }

    public class MicroServices
    {
        public static LoadModelServiceImpl LoadModelService = new LoadModelServiceImpl();
        public static GetModelParametersServiceImpl GetModelParametersService = new GetModelParametersServiceImpl();

        public class LoadModelServiceImpl
        {
            readonly Dictionary<string, IModel> m_modelCache = new Dictionary<string, IModel>();
            public IModel LoadModel(string modelId)
            {
                if (m_modelCache.ContainsKey(modelId))
                    return m_modelCache[modelId];
                IModel model = Helper.LoadModel(modelId);
                m_modelCache.Add(modelId, model);
                return model;                
            }
        }
        public class GetModelParametersServiceImpl
        {
            readonly Dictionary<string, string> m_parameters1Cache = new Dictionary<string, string>(); 
            public string GetParameters(string modelId)
            {
                if (m_parameters1Cache.ContainsKey(modelId))
                    return m_parameters1Cache[modelId];

                IModel model = LoadModelService.LoadModel(modelId);
                string parameters1 = model.Parameters1;
                m_parameters1Cache.Add(modelId, parameters1);
                return parameters1;
            }
        }
    }
}