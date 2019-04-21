using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Test.mxProject.Helpers.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class RpcClient
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public RpcClient(string server, int port)
        {
            m_Channel = new Channel(server, port, ChannelCredentials.Insecure);
            CallInvoker = new DefaultCallInvoker(m_Channel);
        }

        private readonly Channel m_Channel;

        /// <summary>
        /// 
        /// </summary>
        public CallInvoker CallInvoker { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interceptor"></param>
        /// <returns></returns>
        public CallInvoker Intercept(Interceptor interceptor)
        {
            CallInvoker = CallInvoker.Intercept(interceptor);
            return CallInvoker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task ShutdownAsync()
        {
            return m_Channel.ShutdownAsync();
        }

    }

}
