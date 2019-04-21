using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions.Internals
{

    /// <summary>
    /// 
    /// </summary>
    internal sealed class StructMethodCache
    {

        private static readonly Dictionary<string, Method<byte[], Byte[]>> s_BinaryMethods = new Dictionary<string, Method<byte[], Byte[]>>();
        private static readonly Marshaller<byte[]> s_BinaryMarshaller = new Marshaller<byte[]>(bytes => bytes, bytes => bytes);

        /// <summary>
        /// Get the method definition to send byte array and receive byte array.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        internal Method<byte[], Byte[]> GetMethod<TRequest, TResponse>(Method<TRequest, TResponse> method)
        {

            if (s_BinaryMethods.TryGetValue(method.FullName, out Method<byte[], Byte[]> binaryMethod))
            {
                return binaryMethod;
            }

            lock (s_BinaryMethods)
            {

                if (s_BinaryMethods.TryGetValue(method.FullName, out binaryMethod))
                {
                    return binaryMethod;
                }

                binaryMethod = new Method<byte[], byte[]>(
                    method.Type
                    , method.ServiceName
                    , method.Name
                    , s_BinaryMarshaller
                    , s_BinaryMarshaller
                    );

                s_BinaryMethods.Add(method.FullName, binaryMethod);

                return binaryMethod;

            }

        }

    }

}
