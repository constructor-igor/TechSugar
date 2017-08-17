using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace OperationsFactory
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\nServer:");
            Server server = new Server();
            server.RunOperation1(10);
            server.RunOperation2(10);
            server.RunOperation3(10, "10");
            server.RunOperation1(10);

            Console.WriteLine("\nServerUnity:");
            ServerUnity serverUnity = new ServerUnity();
            serverUnity.CreateOperation("1", 21).Execute();
            serverUnity.CreateOperation("2", 22).Execute();
            serverUnity.CreateOperation("3", 23, "23").Execute();

            Console.WriteLine("\nServerFactory:");
            ServerFactory serverFactory = new ServerFactory();
            serverFactory.CreateOperation("1", 21).Execute();
            serverFactory.CreateOperation("2", 22).Execute();
            serverFactory.CreateOperation("3", 23, "23").Execute();
        }
    }

    public class ServerUnity
    {
        private readonly UnityContainer m_container;

        public ServerUnity()
        {
            m_container = new UnityContainer();
            m_container.RegisterType<IOperation, Operation1>("1");
            m_container.RegisterType<IOperation, Operation2>("2");
            m_container.RegisterType<IOperation, Operation3>("3");
        }

        public IOperation CreateOperation(string name, params object[] parameters)
        {
            IOperation operation = m_container.Resolve<IOperation>(name, new OrderedParametersOverride(parameters));
            return operation;
        }
    }

    public class ServerFactory
    {
        private readonly Dictionary<string, Func<object[], IOperation>> m_container = new Dictionary<string, Func<object[], IOperation>>();
        public ServerFactory()
        {            
            m_container.Add("1", objects => new Operation1((int)objects[0]));
            m_container.Add("2", objects => new Operation2((int)objects[0]));
            m_container.Add("3", objects => new Operation3((int)objects[0], (string)objects[1]));
        }
        public IOperation CreateOperation(string name, params object[] parameters)
        {
            return m_container[name](parameters);
            switch (name)
            {
                case "1":
                    return new Operation1((int)parameters[0]);
                case "2":
                    return new Operation2((int)parameters[0]);
                case "3":
                    return new Operation3((int)parameters[0], (string)parameters[1]);
                default: 
                    throw new ArgumentException();
            }
        }
    }

    public class Server
    {
        public void RunOperation1(int data)
        {
            new Operation1(data).Execute();
        }
        public void RunOperation2(int data)
        {
            new Operation2(data).Execute();
        }
        public void RunOperation3(int dataInt, string dataString)
        {
            new Operation3(dataInt, dataString).Execute();
        }
    }

    public interface IOperation
    {
        void Execute();
    }
    public class Operation1: IOperation
    {
        private readonly int m_data;

        public Operation1(int data)
        {
            m_data = data;
            Console.WriteLine("[operation1] ctor");
        }
        #region Implementation of IOperation
        public void Execute()
        {
            Console.WriteLine($"[operation1] Execute({m_data})");
        }
        #endregion
    }
    public class Operation2 : IOperation
    {
        private readonly int m_data;

        public Operation2(int data)
        {
            m_data = data;
            Console.WriteLine("[operation2] ctor");
        }
        #region Implementation of IOperation
        public void Execute()
        {
            Console.WriteLine($"[operation2] Execute({m_data})");
        }
        #endregion
    }
    public class Operation3 : IOperation
    {
        private readonly int m_dataInt;
        private readonly string m_dataString;

        public Operation3(int dataInt, string dataString)
        {
            m_dataInt = dataInt;
            m_dataString = dataString;
            Console.WriteLine("[operation3] ctor");
        }
        #region Implementation of IOperation
        public void Execute()
        {
            Console.WriteLine($"[operation3] Execute({m_dataInt}, {m_dataString})");
        }
        #endregion
    }

    public class OrderedParametersOverride : ResolverOverride
    {
        private readonly Queue<InjectionParameterValue> parameterValues;

        public OrderedParametersOverride(IEnumerable<object> parameterValues)
        {
            this.parameterValues = new Queue<InjectionParameterValue>();
            foreach (var parameterValue in parameterValues)
            {
                this.parameterValues.Enqueue(InjectionParameterValue.ToParameter(parameterValue));
            }
        }

        public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
        {
            if (parameterValues.Count < 1)
                return null;

            var value = this.parameterValues.Dequeue();
            return value.GetResolverPolicy(dependencyType);
        }
    }
}
