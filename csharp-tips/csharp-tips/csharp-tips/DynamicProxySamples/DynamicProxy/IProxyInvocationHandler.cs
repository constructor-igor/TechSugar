using System;
using System.Reflection;

namespace DynamicProxy
{
	/// <summary>
    /// Interface that a user defined proxy handler needs to implement.  This interface 
    /// defines one method that gets invoked by the generated proxy.  
	/// </summary>
	public interface IProxyInvocationHandler
	{
        /// <param name="proxy">The instance of the proxy</param>
        /// <param name="method">The method info that can be used to invoke the actual method on the object implementation</param>
        /// <param name="parameters">Parameters to pass to the method</param>
        /// <returns>Object</returns>
        Object Invoke( Object proxy, MethodInfo method, Object[] parameters );
	}
}
