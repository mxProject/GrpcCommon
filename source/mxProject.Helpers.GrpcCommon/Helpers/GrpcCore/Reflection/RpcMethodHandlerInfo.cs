using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Information of gRPC method.
    /// </summary>
    public sealed class RpcMethodHandlerInfo
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="methodType">The gRPC method type.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <param name="handler">The method handler.</param>
        /// <param name="ignore">A value whether <see cref="RpcIgnoreAttribute"/> is declared.</param>
        public RpcMethodHandlerInfo(MethodType methodType, Type requestType, Type responseType, MethodInfo handler, bool ignore)
        {
            MethodType = methodType;
            RequestType = requestType;
            ResponseType = responseType;
            Handler = handler;
            Ignore = ignore;
        }

        /// <summary>
        /// Gets the gRPC method type.
        /// </summary>
        public MethodType MethodType { get; }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        public Type RequestType { get; }

        /// <summary>
        /// Gets the type of the response.
        /// </summary>
        public Type ResponseType { get; }

        /// <summary>
        /// Gets the method handler.
        /// </summary>
        public MethodInfo Handler { get; }

        /// <summary>
        /// Gets whether <see cref="RpcIgnoreAttribute"/> is declared.
        /// </summary>
        public bool Ignore { get; }
    }

}
