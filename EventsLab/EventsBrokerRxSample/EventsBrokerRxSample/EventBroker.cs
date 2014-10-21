using System;
using System.Collections.Generic;

namespace EventsBrokerRxSample
{
    public interface IEventBroker : IObservable<EventArgs>
    {
        void Unsubscribe(IObserver<EventArgs> subscriber);
        void Publish<T>(T args) where T : EventArgs;
    }

    //
    // multi-threaded not supported!
    // 
    public class EventBroker : IEventBroker
    {
        private readonly List<Subscription> m_subscribers = new List<Subscription>();
        public IDisposable Subscribe(IObserver<EventArgs> subscriber)
        {
            var subscription = new Subscription(this, subscriber);
            m_subscribers.Add(subscription);
            Console.WriteLine("Subscribe: {0}", subscriber.GetHashCode());
            return subscription;
        }
        public void Unsubscribe(IObserver<EventArgs> subscriber)
        {
            Console.WriteLine("Unsubscribe: {0}", subscriber.GetHashCode());
            m_subscribers.RemoveAll(s => s.Subscriber == subscriber);
        }
        public void Publish<T>(T args) where T : EventArgs
        {
            foreach (Subscription subscription in m_subscribers.ToArray())
            {
                subscription.Subscriber.OnNext(args);
            }
        }

        private class Subscription : IDisposable
        {
            private readonly IEventBroker m_broker;
            public readonly IObserver<EventArgs> Subscriber;
            public Subscription(IEventBroker broker, IObserver<EventArgs> subscriber)
            {
                m_broker = broker;
                Subscriber = subscriber;
            }
            #region IDisposable
            public void Dispose()
            {
                m_broker.Unsubscribe(Subscriber);
            }
            #endregion
        }
    }
}