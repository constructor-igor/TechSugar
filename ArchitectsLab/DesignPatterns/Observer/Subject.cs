using System.Collections.Generic;

namespace DesignPatterns.Observer
{
    public abstract class Subject
    {
        private readonly List<IObserver> m_observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            m_observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            m_observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in m_observers)
            {
                observer.Update(this);
            }
        }
    }
}