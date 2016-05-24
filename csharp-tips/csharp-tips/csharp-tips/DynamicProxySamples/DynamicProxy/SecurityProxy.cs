using System;

namespace DynamicProxy
{
	/// <summary>
    /// Test proxy invocation handler which is used to check a methods security
    /// before invoking the method
    /// </summary>
	public class SecurityProxy : IProxyInvocationHandler
	{
        Object obj = null;

        ///<summary>
        /// Class constructor
        ///</summary>
        ///<param name="obj">Instance of object to be proxied</param>
        private SecurityProxy( Object obj ) {
            this.obj = obj;
        }

        ///<summary>
        /// Factory method to create a new proxy instance.
        ///</summary>
        ///<param name="obj">Instance of object to be proxied</param>
        public static Object NewInstance( Object obj ) {
            return ProxyFactory.GetInstance().Create( 
                new SecurityProxy( obj ), obj.GetType() );
        }

        ///<summary>
        /// IProxyInvocationHandler method that gets called from within the proxy
        /// instance. 
        ///</summary>
        ///<param name="proxy">Instance of proxy</param>
        ///<param name="method">Method instance 
        public Object Invoke(Object proxy, System.Reflection.MethodInfo method, Object[] parameters) {

            Object retVal = null;
            string userRole = "role";
  
            // if the user has permission to invoke the method, the method
            // is invoked, otherwise an exception is thrown indicating they
            // do not have permission
            if ( SecurityManager.IsMethodInRole( userRole, method.Name ) ) {
                // The actual method is invoked
                retVal = method.Invoke( obj, parameters );
            } else {
                throw new Exception( "Invalid permission to invoke " + method.Name ); 
            }

            return retVal;
        }
    }
}
