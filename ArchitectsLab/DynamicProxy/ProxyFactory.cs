namespace DynamicProxy
{
    internal static class BinderExtensions
    {
        public static IList<Type> GetTypeArguments(this InvokeMemberBinder binder)
        {
            return Impromptu.InvokeGet(binder, "Microsoft.CSharp.RuntimeBinder.ICSharpInvokeOrInvokeMemberBinder.TypeArguments") as IList<Type>;
        }
    }
}