namespace Plugin.Command.Interfaces
{
    public interface ICommandPlugin
    {
        string Name { get; }
        double Run(double X, double Y);
        void Action();
    }
}
