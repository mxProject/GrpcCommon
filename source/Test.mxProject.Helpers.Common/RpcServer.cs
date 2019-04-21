using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

using mxProject.Helpers.GrpcCore.Extensions;
using mxProject.Helpers.GrpcCore.Reflection;

namespace Test.mxProject.Helpers.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class RpcServer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public RpcServer(string server, int port)
        {
            m_Server = new Server();
            m_Server.Ports.Add(new ServerPort(server, port, ServerCredentials.Insecure));
            m_Service = ServerServiceDefinition.CreateBuilder();
        }

        private readonly Server m_Server;
        private ServerServiceDefinition.Builder m_Service;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, UnaryServerMethod<TRequest, TResponse> handler)
            where TRequest : class
            where TResponse : class
        {
            m_Service = m_Service.AddMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, ClientStreamingServerMethod<TRequest, TResponse> handler)
            where TRequest : class
            where TResponse : class
        {
            m_Service = m_Service.AddMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, ServerStreamingServerMethod<TRequest, TResponse> handler)
            where TRequest : class
            where TResponse : class
        {
            m_Service = m_Service.AddMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, DuplexStreamingServerMethod<TRequest, TResponse> handler)
            where TRequest : class
            where TResponse : class
        {
            m_Service = m_Service.AddMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddStructMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, StructUnaryServerMethod<TRequest, TResponse> handler)
        {
            m_Service = m_Service.AddStructMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddStructMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, StructClientStreamingServerMethod<TRequest, TResponse> handler)
        {
            m_Service = m_Service.AddStructMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddStructMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, StructServerStreamingServerMethod<TRequest, TResponse> handler)
        {
            m_Service = m_Service.AddStructMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="handler"></param>
        protected void AddStructMethod<TRequest, TResponse>(Method<TRequest, TResponse> method, StructDuplexStreamingServerMethod<TRequest, TResponse> handler)
        {
            m_Service = m_Service.AddStructMethod(method, handler);
        }

        /// <summary>
        /// 
        /// </summary>
        protected void AddMethods(object serviceInstance)
        {

            string serviceName = "TestService";

            TestService service = new TestService();

            foreach (RpcMethodHandlerInfo methodHandler in RpcReflection.EnumerateServiceMethods(typeof(TestService), false))
            {
                m_Service = RpcReflection.AddMethod(m_Service, serviceName, methodHandler, MessagePackMarshaller.Current, service);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public void Start(params Interceptor[] interceptors)
        {
            ServerServiceDefinition definition = m_Service.Build();

            if (interceptors != null)
            {
                definition = definition.Intercept(interceptors);
            }

            m_Server.Services.Add(definition);
            m_Server.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task ShutdownAsync()
        {
            return m_Server.ShutdownAsync();
        }

    }
}
