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
    public class TestServer : RpcServer
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="port"></param>
        public TestServer(string server, int port) : base(server, port)
        {
            TestService testService = new TestService();

            //AddMethod(TestRpcMethods.GetResponse, testService.GetResponse);
            //AddMethod(TestRpcMethods.SendRequests, testService.SendRequests);
            //AddMethod(TestRpcMethods.ReceiveResponses, testService.ReceiveResponses);
            //AddMethod(TestRpcMethods.SendAndReceive, testService.SendAndReceive);

            //AddStructMethod(TestRpcMethods.GetResponseStruct, testService.GetResponseStruct);
            //AddStructMethod(TestRpcMethods.SendRequestsStruct, testService.SendRequestsStruct);
            //AddStructMethod(TestRpcMethods.ReceiveResponsesStruct, testService.ReceiveResponsesStruct);
            //AddStructMethod(TestRpcMethods.SendAndReceiveStruct, testService.SendAndReceiveStruct);

            base.AddMethods(testService);

        }

    }
}
