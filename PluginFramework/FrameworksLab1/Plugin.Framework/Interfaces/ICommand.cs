namespace Plugin.Framework.Interfaces
{
    public interface ICommand
    {
        ICommandDescriptor Descriptor { get; }
        IDataEntity Run(ICommandContext commandContext);
    }
}
