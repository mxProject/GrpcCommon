using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Extensions;

namespace mxProject.Helpers.GrpcCore.Reflection
{

    /// <summary>
    /// Reflection operation related to gRPC.
    /// </summary>
    public static class RpcReflection
    {

        #region enumerates service methods

        /// <summary>
        /// Enumerates the RPC methods implemented in the specified service type.
        /// </summary>
        /// <param name="serviceImplType">The type of the service.</param>
        /// <param name="containsIgnoreMethod">A value whether to include the method declared with <see cref="RpcIgnoreAttribute"/>.</param>
        /// <returns></returns>
        public static IEnumerable<RpcMethodHandlerInfo> EnumerateServiceMethods(Type serviceImplType, bool containsIgnoreMethod)
        {

            foreach (MethodInfo method in serviceImplType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {

                bool ignore = IsIgnore(method);

                if (!containsIgnoreMethod && ignore) { continue; }

                if (!TryGetServiceMethodInfo(method, out MethodType methodType, out Type requestType, out Type responseType)) { continue; }

                yield return new RpcMethodHandlerInfo(methodType, requestType, responseType, method, ignore);

            }

        }

        /// <summary>
        /// Gets whether to ignore the specified method.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <returns>true if ignore; otherwise false.</returns>
        private static bool IsIgnore(MethodInfo methodImpl)
        {
            RpcIgnoreAttribute attr = methodImpl.GetCustomAttribute<RpcIgnoreAttribute>(false);
            return (attr != null);
        }

        /// <summary>
        /// If the specified method is an RPC service method, gets the RPC method information.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <param name="methodType">The gRPC method type.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <returns>true if it is an RPC service method; otherwise false.</returns>
        private static bool TryGetServiceMethodInfo(MethodInfo methodImpl, out MethodType methodType, out Type requestType, out Type responseType)
        {

            if (methodImpl.IsPublic && !methodImpl.IsStatic)
            {

                if (IsMatchUnaryMethodHandler(methodImpl, out requestType, out responseType))
                {
                    methodType = MethodType.Unary;
                    return true;
                }
                else if (IsMatchClientStreamingMethodHandler(methodImpl, out requestType, out responseType))
                {
                    methodType = MethodType.ClientStreaming;
                    return true;
                }
                else if (IsMatchServerStreamingMethodHandler(methodImpl, out requestType, out responseType))
                {
                    methodType = MethodType.ServerStreaming;
                    return true;
                }
                else if (IsMatchDuplexStreamingMethodHandler(methodImpl, out requestType, out responseType))
                {
                    methodType = MethodType.DuplexStreaming;
                    return true;
                }

            }

            methodType = default;
            responseType = null;
            requestType = null;

            return false;

        }

        /// <summary>
        /// Gets whether the specified method is an unary service method.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <returns>true if it is an unary service method; otherwise false.</returns>
        private static bool IsMatchUnaryMethodHandler(MethodInfo methodImpl, out Type requestType, out Type responseType)
        {

            //Task<TResponse> Method(TRequest request, ServerCallContext context)

            requestType = null;
            responseType = null;

            if (!methodImpl.ReturnType.IsGenericType) { return false; }
            if (methodImpl.ReturnType.GetGenericTypeDefinition() != typeof(Task<>)) { return false; }

            ParameterInfo[] parameters = methodImpl.GetParameters();

            if (parameters.Length != 2) { return false; }

            if (IsServiceRequestStreamType(parameters[0].ParameterType)) { return false; }
            if (parameters[1].ParameterType != typeof(ServerCallContext)) { return false; }

            requestType = parameters[0].ParameterType;
            responseType = methodImpl.ReturnType.GetGenericArguments()[0];

            return true;

        }

        /// <summary>
        /// Gets whether the specified method is a server streaming service method.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <returns>true if it is a server streaming service method; otherwise false.</returns>
        private static bool IsMatchServerStreamingMethodHandler(MethodInfo methodImpl, out Type requestType, out Type responseType)
        {

            //Task Method(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context)

            requestType = null;
            responseType = null;

            if (methodImpl.ReturnType != typeof(Task)) { return false; }

            ParameterInfo[] parameters = methodImpl.GetParameters();

            if (parameters.Length != 3) { return false; }

            if (IsServiceRequestStreamType(parameters[0].ParameterType)) { return false; }
            if (!IsServiceResponseStreamType(parameters[1].ParameterType)) { return false; }
            if (parameters[2].ParameterType != typeof(ServerCallContext)) { return false; }

            requestType = parameters[0].ParameterType;
            responseType = parameters[1].ParameterType.GetGenericArguments()[0];

            return true;

        }

        /// <summary>
        /// Gets whether the specified method is a client streaming service method.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <returns>true if it is a client streaming service method; otherwise false.</returns>
        private static bool IsMatchClientStreamingMethodHandler(MethodInfo methodImpl, out Type requestType, out Type responseType)
        {

            //Task<TResponse> Method(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context)

            requestType = null;
            responseType = null;

            if (!methodImpl.ReturnType.IsGenericType) { return false; }
            if (methodImpl.ReturnType.GetGenericTypeDefinition() != typeof(Task<>)) { return false; }

            ParameterInfo[] parameters = methodImpl.GetParameters();

            if (parameters.Length != 2) { return false; }

            if (!IsServiceRequestStreamType(parameters[0].ParameterType)) { return false; }
            if (parameters[1].ParameterType != typeof(ServerCallContext)) { return false; }

            requestType = parameters[0].ParameterType.GetGenericArguments()[0];
            responseType = methodImpl.ReturnType.GetGenericArguments()[0];

            return true;

        }

        /// <summary>
        /// Gets whether the specified method is a duplex streaming service method.
        /// </summary>
        /// <param name="methodImpl">The method.</param>
        /// <param name="requestType">The type of the request.</param>
        /// <param name="responseType">The type of the response.</param>
        /// <returns>true if it is a duplex streaming service method; otherwise false.</returns>
        private static bool IsMatchDuplexStreamingMethodHandler(MethodInfo methodImpl, out Type requestType, out Type responseType)
        {

            //Task Method(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context)

            requestType = null;
            responseType = null;

            if (methodImpl.ReturnType != typeof(Task)) { return false; }

            ParameterInfo[] parameters = methodImpl.GetParameters();

            if (parameters.Length != 3) { return false; }

            if (!IsServiceRequestStreamType(parameters[0].ParameterType)) { return false; }
            if (!IsServiceResponseStreamType(parameters[1].ParameterType)) { return false; }
            if (parameters[2].ParameterType != typeof(ServerCallContext)) { return false; }

            requestType = parameters[0].ParameterType.GetGenericArguments()[0];
            responseType = parameters[1].ParameterType.GetGenericArguments()[0];

            return true;

        }

        /// <summary>
        /// Gets whether the specified type represents a request stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsServiceRequestStreamType(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IAsyncStreamReader<>));
        }

        /// <summary>
        /// Gets whether the specified type represents a response stream.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsServiceResponseStreamType(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IServerStreamWriter<>));
        }

        #endregion

        #region generates service method

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="handler">The method information.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <returns>The method description.</returns>
        public static IMethod CreateMethod(string serviceName, RpcMethodHandlerInfo handler, IRpcMarshallerFactory marshallerFactory)
        {

            MethodInfo m = typeof(RpcReflection).GetMethod("CreateMethodCore", BindingFlags.Static | BindingFlags.NonPublic);

            return (IMethod)m.MakeGenericMethod(new Type[] { handler.RequestType, handler.ResponseType }).Invoke(null, new object[] { serviceName, handler, marshallerFactory });

        }

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="handler">The method information.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <returns>The method description.</returns>
        private static Method<TRequest, TResponse> CreateMethodCore<TRequest, TResponse>(string serviceName, RpcMethodHandlerInfo handler, IRpcMarshallerFactory marshallerFactory)
        {
            return new Method<TRequest, TResponse>(handler.MethodType, serviceName, handler.Handler.Name, marshallerFactory.GetMarshaller<TRequest>(), marshallerFactory.GetMarshaller<TResponse>());
        }

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="methodType">The RPC method type.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <returns>The method description.</returns>
        private static Method<TRequest, TResponse> CreateMethod<TRequest, TResponse>(string serviceName, string methodName, MethodType methodType, IRpcMarshallerFactory marshallerFactory)
        {
            return new Method<TRequest, TResponse>(methodType, serviceName, methodName, marshallerFactory.GetMarshaller<TRequest>(), marshallerFactory.GetMarshaller<TResponse>());
        }

        #endregion

        #region generates service method

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <param name="builder">The service builder.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="handler">The method information.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <param name="serviceInstance">The service instance.</param>
        /// <returns>The method description.</returns>
        public static ServerServiceDefinition.Builder AddMethod(ServerServiceDefinition.Builder builder, string serviceName, RpcMethodHandlerInfo handler, IRpcMarshallerFactory marshallerFactory, object serviceInstance)
        {
            if (handler.RequestType.IsClass && handler.ResponseType.IsClass)
            {
                MethodInfo m = typeof(RpcReflection).GetMethod("AddMethodCore", BindingFlags.Static | BindingFlags.NonPublic);
                return (ServerServiceDefinition.Builder)m.MakeGenericMethod(new Type[] { handler.RequestType, handler.ResponseType }).Invoke(null, new object[] { builder, serviceName, handler, marshallerFactory, serviceInstance });
            }
            else
            {
                MethodInfo m = typeof(RpcReflection).GetMethod("AddStructMethodCore", BindingFlags.Static | BindingFlags.NonPublic);
                return (ServerServiceDefinition.Builder)m.MakeGenericMethod(new Type[] { handler.RequestType, handler.ResponseType }).Invoke(null, new object[] { builder, serviceName, handler, marshallerFactory, serviceInstance });
            }
        }

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder">The service builder.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="handler">The method information.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <param name="serviceInstance">The service instance.</param>
        /// <returns>The method description.</returns>
        private static ServerServiceDefinition.Builder AddMethodCore<TRequest, TResponse>(ServerServiceDefinition.Builder builder, string serviceName, RpcMethodHandlerInfo handler, IRpcMarshallerFactory marshallerFactory, object serviceInstance)
            where TRequest : class
            where TResponse : class
        {
            Method<TRequest, TResponse> method = new Method<TRequest, TResponse>(handler.MethodType, serviceName, handler.Handler.Name, marshallerFactory.GetMarshaller<TRequest>(), marshallerFactory.GetMarshaller<TResponse>());

            switch (handler.MethodType)
            {
                case MethodType.Unary:
                    return builder.AddMethod(method, (UnaryServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(UnaryServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.ClientStreaming:
                    return builder.AddMethod(method, (ClientStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(ClientStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.ServerStreaming:
                    return builder.AddMethod(method, (ServerStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(ServerStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.DuplexStreaming:
                    return builder.AddMethod(method, (DuplexStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(DuplexStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                default:
                    return builder;
            }

        }

        /// <summary>
        /// Creates the RPC method description.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="builder">The service builder.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="handler">The method information.</param>
        /// <param name="marshallerFactory">The factory of the marshaller.</param>
        /// <param name="serviceInstance">The service instance.</param>
        /// <returns>The method description.</returns>
        private static ServerServiceDefinition.Builder AddStructMethodCore<TRequest, TResponse>(ServerServiceDefinition.Builder builder, string serviceName, RpcMethodHandlerInfo handler, IRpcMarshallerFactory marshallerFactory, object serviceInstance)
        {
            Method<TRequest, TResponse> method = new Method<TRequest, TResponse>(handler.MethodType, serviceName, handler.Handler.Name, marshallerFactory.GetMarshaller<TRequest>(), marshallerFactory.GetMarshaller<TResponse>());

            switch (handler.MethodType)
            {
                case MethodType.Unary:
                    return builder.AddStructMethod(method, (StructUnaryServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(StructUnaryServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.ClientStreaming:
                    return builder.AddStructMethod(method, (StructClientStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(StructClientStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.ServerStreaming:
                    return builder.AddStructMethod(method, (StructServerStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(StructServerStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                case MethodType.DuplexStreaming:
                    return builder.AddStructMethod(method, (StructDuplexStreamingServerMethod<TRequest, TResponse>)handler.Handler.CreateDelegate(typeof(StructDuplexStreamingServerMethod<TRequest, TResponse>), serviceInstance));

                default:
                    return builder;
            }

        }

        #endregion

    }

}
