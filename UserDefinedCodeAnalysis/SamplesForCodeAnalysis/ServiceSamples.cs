using System.Collections.Generic;

namespace SamplesForCodeAnalysis
{
    public interface IBaseService
    {
        
    }

    public class UserService1 : IBaseService
    {        
    }

    public class UserService2 : IBaseService
    {        
    }

    public class ServiceManager
    {
        public IDictionary<string, IBaseService> AllServices = new Dictionary<string, IBaseService>();
        public void RegisterAllServices()
        {
            AllServices.Add("service1", new UserService1());
        }
    }
}