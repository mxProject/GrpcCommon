using System;
using System.Collections.Generic;
using System.Text;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Indicates that the method should be ignored in the reflection process.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RpcIgnoreAttribute : Attribute
    {
        /// <summary>
        /// Create a new instance.
        /// </summary>
        public RpcIgnoreAttribute()
        {
        }
    }

}
