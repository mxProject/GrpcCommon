using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;

using mxProject.Helpers.GrpcCore.Extensions.Internals;

namespace mxProject.Helpers.GrpcCore.Extensions
{
    /// <summary>
    /// Extension methods of <see cref="CallInvoker"/>.
    /// </summary>
    public static class CallInvokerExtensions
    {

        private static readonly StructMethodCache s_StructMethodCache = new StructMethodCache();

        /// <summary>
        /// Invokes BlockingUnaryCall without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="callInvoker"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static TResponse StructBlockingUnaryCall<TRequest, TResponse>(this CallInvoker callInvoker, Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            byte[] bytes = callInvoker.BlockingUnaryCall(s_StructMethodCache.GetMethod(method), host, options, method.RequestMarshaller.Serializer(request));
            return method.ResponseMarshaller.Deserializer(bytes);
        }

        /// <summary>
        /// Invokes AsyncUnaryCall without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="callInvoker"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static AsyncUnaryCall<TResponse> StructAsyncUnaryCall<TRequest, TResponse>(this CallInvoker callInvoker, Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            AsyncUnaryCall<byte[]> call = callInvoker.AsyncUnaryCall(s_StructMethodCache.GetMethod(method), host, options, method.RequestMarshaller.Serializer(request));
            return call.ConvertResponse(method.ResponseMarshaller.Deserializer);
        }

        /// <summary>
        /// Invokes AsyncClientStreamingCall without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="callInvoker"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AsyncClientStreamingCall<TRequest, TResponse> StructAsyncClientStreamingCall<TRequest, TResponse>(this CallInvoker callInvoker, Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            AsyncClientStreamingCall<byte[], byte[]> call = callInvoker.AsyncClientStreamingCall(s_StructMethodCache.GetMethod(method), host, options);
            return call.ConvertRequestResponse(method.RequestMarshaller.Serializer, method.ResponseMarshaller.Deserializer);
        }

        /// <summary>
        /// Invokes AsyncServerStreamingCall without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="callInvoker"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static AsyncServerStreamingCall<TResponse> StructAsyncServerStreamingCall<TRequest, TResponse>(this CallInvoker callInvoker, Method<TRequest, TResponse> method, string host, CallOptions options, TRequest request)
        {
            AsyncServerStreamingCall<byte[]> call = callInvoker.AsyncServerStreamingCall(s_StructMethodCache.GetMethod(method), host, options, method.RequestMarshaller.Serializer(request));
            return call.ConvertResponse(method.ResponseMarshaller.Deserializer);
        }

        /// <summary>
        /// Invokes AsyncDuplexStreamingCall without type restrictions.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="callInvoker"></param>
        /// <param name="method"></param>
        /// <param name="host"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AsyncDuplexStreamingCall<TRequest, TResponse> StructAsyncDuplexStreamingCall<TRequest, TResponse>(this CallInvoker callInvoker, Method<TRequest, TResponse> method, string host, CallOptions options)
        {
            AsyncDuplexStreamingCall<byte[], byte[]> call = callInvoker.AsyncDuplexStreamingCall(s_StructMethodCache.GetMethod(method), host, options);
            return call.ConvertRequestResponse(method.RequestMarshaller.Serializer, method.ResponseMarshaller.Deserializer);
        }

    }
}
