using System;
using System.Linq;
using System.Collections.Generic;

namespace EventsBrokerRxSample
{
    #region Parameter implementation
    public class ParameterChangedEventArgs : EventArgs
    {
        public Parameter Parameter;
        public ParameterChangedEventArgs(Parameter parameter)
        {
            Parameter = parameter;
        }
    }
    public class ParameterSetToZeroEventArgs : EventArgs
    {
        public Parameter Parameter;
        public ParameterSetToZeroEventArgs(Parameter parameter)
        {
            Parameter = parameter;
        }
    }
    public class Parameter
    {
        public string Name { get; set; }
        private double m_value;
        public double Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                m_broker.Publish(new ParameterChangedEventArgs(this));
                if (Math.Abs(m_value) < 0.001)
                    m_broker.Publish(new ParameterSetToZeroEventArgs(this));
            }
        }
        private readonly IEventBroker m_broker;
        public Parameter(IEventBroker broker)
        {
            m_broker = broker;
        }
    }
    #endregion

    public class ParameterManager: IDisposable
    {
        private readonly List<IDisposable> subscriptionList = new List<IDisposable>();
        public ParameterManager(IEventBroker eventBroker)
        {
            IDisposable subscription = eventBroker
                .OfType<ParameterChangedEventArgs>()
                .Skip(2)
                .Take(3)
                .Subscribe(args => Console.WriteLine("Parameter '{0}' changed. New value = {1}.", args.Parameter.Name, args.Parameter.Value));
            subscriptionList.Add(subscription);

            subscription = eventBroker
                .OfType<ParameterSetToZeroEventArgs>()
                .Subscribe(args => Console.WriteLine("Parameter '{0}' is equal 0.", args.Parameter.Name));
            subscriptionList.Add(subscription);
        }

        #region IDisposable
        public void Dispose()
        {
            foreach (IDisposable subscription in subscriptionList)
            {
                subscription.Dispose();
            }
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            IEventBroker broker = new EventBroker();
            using (var manager = new ParameterManager(broker))
            {
                var parameter1 = new Parameter(broker) {Name = "CD", Value = 10};
                var parameter2 = new Parameter(broker) {Name = "HEIGHT", Value = 100};

                parameter1.Value = 12;
                parameter1.Value = 13;
                parameter1.Value = 14;
                parameter1.Value = 15;
                parameter1.Value = 15;

                parameter2.Value = 0;
            }
        }
    }
}
