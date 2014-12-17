using VS2015News;
// <copyright file="MobileFactory.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;

namespace VS2015News
{
    /// <summary>A factory for VS2015News.Mobile instances</summary>
    public static partial class MobileFactory
    {
        /// <summary>A factory for VS2015News.Mobile instances</summary>
        [PexFactoryMethod(typeof(Mobile))]
        public static Mobile Create()
        {
            Mobile mobile = new Mobile();            
            return mobile;

            // TODO: Edit factory method of Mobile
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
