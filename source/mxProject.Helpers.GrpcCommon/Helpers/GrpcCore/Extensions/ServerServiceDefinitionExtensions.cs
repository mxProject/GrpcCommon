using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Extensions.Internals;
using mxProject.Helpers.GrpcCore.Reflection;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="ServerServiceDefinition"/>.
    /// </summary>
    public static class ServerServiceDefinitionExtensions
    {

        private static readonly StructMethodCache s_StructMethodCache = new StructMethodCache();

        #region AddStructMethod

        /// <summary>
        /// Adds a definition for a single request - single response method without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder"></param>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ServerServiceDefinition.Builder AddStructMethod<TRequest, TResponse>(this ServerServiceDefinition.Builder builder, Method<TRequest, TResponse> method, StructUnaryServerMethod<TRequest, TResponse> handler)
        {
            async Task<byte[]> func(byte[] request, ServerCallContext context)
            {
                TResponse response = await handler(method.RequestMarshaller.Deserializer(request), context).ConfigureAwait(false);
                return method.ResponseMarshaller.Serializer(response);
            };
            
            return builder.AddMethod(s_StructMethodCache.GetMethod(method), func);
        }

        /// <summary>
        /// Adds a definition for a client streaming method without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder"></param>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ServerServiceDefinition.Builder AddStructMethod<TRequest, TResponse>(this ServerServiceDefinition.Builder builder, Method<TRequest, TResponse> method, StructClientStreamingServerMethod<TRequest, TResponse> handler)
        {
            async Task<byte[]> func(IAsyncStreamReader<byte[]> requestStream, ServerCallContext context)
            {
                TResponse response = await handler(
                    requestStream.Convert(method.RequestMarshaller.Deserializer)
                    , context
                    ).ConfigureAwait(false);

                return method.ResponseMarshaller.Serializer(response);
            };

            return builder.AddMethod(s_StructMethodCache.GetMethod(method), func);
        }

        /// <summary>
        /// Adds a definition for a server streaming method without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder"></param>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ServerServiceDefinition.Builder AddStructMethod<TRequest, TResponse>(this ServerServiceDefinition.Builder builder, Method<TRequest, TResponse> method, StructServerStreamingServerMethod<TRequest, TResponse> handler)
        {
            Task func(byte[] request, IServerStreamWriter<byte[]> responseStream, ServerCallContext context)
            {
                return handler(
                    method.RequestMarshaller.Deserializer(request)
                    , responseStream.ConvertResponse(method.ResponseMarshaller.Serializer)
                    , context
                    );
            };

            return builder.AddMethod(s_StructMethodCache.GetMethod(method), func);
        }

        /// <summary>
        /// Adds a definition for a duplex streaming method without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder"></param>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ServerServiceDefinition.Builder AddStructMethod<TRequest, TResponse>(this ServerServiceDefinition.Builder builder, Method<TRequest, TResponse> method, StructDuplexStreamingServerMethod<TRequest, TResponse> handler)
        {
            Task func(IAsyncStreamReader<byte[]> requestStream, IServerStreamWriter<byte[]> responseStream, ServerCallContext context)
            {
                return handler(
                    requestStream.Convert(bytes => method.RequestMarshaller.Deserializer(bytes))
                    , responseStream.ConvertResponse(method.ResponseMarshaller.Serializer)
                    , context
                    );
            };

            return builder.AddMethod(s_StructMethodCache.GetMethod(method), func);
        }

        #endregion

    }
}
