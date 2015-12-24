namespace DesignPatterns.Observer
{
    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify(object realSubject);
    }
}