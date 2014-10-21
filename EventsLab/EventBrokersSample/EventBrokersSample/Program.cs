using System;
using System.Collections.Generic;

namespace EventBrokersSample
{
    class Program
    {
        public interface IPlugin
        {
            bool Valid(string eventName);
            Action<string, object, EventArgs> Handler { get; set; }
        }

        public class PluginLog : IPlugin
        {
            public bool Valid(string eventName)
            {
                return true;
            }
            public Action<string, object, EventArgs> Handler { get; set; }
        }

        public interface IBroker
        {
            void Publish(string name, object sender, EventArgs args);
            void Subscribe<T>(string name, Action<object, EventArgs> handler);
        }
        public class Broker: IBroker
        {
            private readonly MultiValueDictionary<string, Action<object, EventArgs>> subscriptions = new MultiValueDictionary<string, Action<object, EventArgs>>();
            public List<IPlugin> PluginList = new List<IPlugin>();
            public void Publish(string name, object sender, EventArgs args)
            {
                foreach (IPlugin plugin in PluginList)
                {
                    if (plugin.Valid(name))
                        plugin.Handler(name, sender, args);
                }

                foreach (var handler in subscriptions[name])
                {                    
                    handler(sender, args);
                }
            }

            public void Subscribe<T>(string name, Action<object, EventArgs> handler)
            {
                subscriptions.Add(name, handler);
            }
        }

        public class Parameter
        {
            private double m_value;
            public string Name { get; set; }
            public double Value 
            { 
                get { return m_value; }
                set
                {
                    m_value = value; 
                    m_broker.Publish("parameterChanged", this, new EventArgs());
                }
            }

            private IBroker m_broker;

            public Parameter(IBroker broker)
            {
                m_broker = broker;
            }
            public Parameter Clone()
            {
                return (Parameter) this.MemberwiseClone();
            }

            public override string ToString()
            {
                return String.Format("Name = {0}, Value = {1}", Name, Value);
            }
        }

        public class ParametersManager
        {
            private IBroker m_broker;

            public ParametersManager(IBroker broker)
            {
                m_broker = broker;
                m_broker.Subscribe<EventArgs>("parameterChanged", (sender, args) =>
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("parameter '{0}' changed", (sender as Parameter).Name);
                    Console.ResetColor();
                });
            }
        }

        static void Main()
        {
            var broker = new Broker();
            IPlugin plugin = new PluginLog();
            plugin.Handler = (eventName, sender, args) => Console.WriteLine("[plugin]: event = {0}, sender = ({1}), args = ({2})", eventName, sender, args);
            broker.PluginList.Add(plugin);

            var parameter = new Parameter(broker) {Name = "CD"};
            var manager = new ParametersManager(broker);

            parameter.Value = 10;

            Parameter parameter2 = parameter.Clone();

            parameter2.Name = "HEIGHT";
            parameter2.Value = 5;
        }
    }
}
