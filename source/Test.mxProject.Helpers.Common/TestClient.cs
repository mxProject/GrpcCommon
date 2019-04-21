using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{

    /// <summary>
    /// 
    /// </summary>
    public class TestClient : RpcClient
    {

        public TestClient(string server, int port) : base(server, port)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public TestResponse BlockingUnaryCall(TestRequest request)
        {
            return CallInvoker.BlockingUnaryCall(TestRpcMethods.GetResponse, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        public TestResponseStruct StructBlockingUnaryCall(TestRequestStruct request)
        {
            return CallInvoker.StructBlockingUnaryCall(TestRpcMethods.GetResponseStruct, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TestResponse> AsyncUnaryCall(TestRequest request)
        {
            return await CallInvoker.AsyncUnaryCall(TestRpcMethods.GetResponse, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<TestResponseStruct> StructAsyncUnaryCall(TestRequestStruct request)
        {
            return await CallInvoker.StructAsyncUnaryCall(TestRpcMethods.GetResponseStruct, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AsyncClientStreamingCall<TestRequest, TestResponse> ClientStreaming()
        {
            return CallInvoker.AsyncClientStreamingCall(TestRpcMethods.SendRequests, "", new CallOptions());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AsyncClientStreamingCall<TestRequestStruct, TestResponseStruct> StructClientStreaming()
        {
            return CallInvoker.StructAsyncClientStreamingCall(TestRpcMethods.SendRequestsStruct, "", new CallOptions());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AsyncServerStreamingCall<TestResponse> ServerStreaming(TestRequest request)
        {
            return CallInvoker.AsyncServerStreamingCall(TestRpcMethods.ReceiveResponses, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AsyncServerStreamingCall<TestResponseStruct> StructServerStreaming(TestRequestStruct request)
        {
            return CallInvoker.StructAsyncServerStreamingCall(TestRpcMethods.ReceiveResponsesStruct, "", new CallOptions(), request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AsyncDuplexStreamingCall<TestRequest, TestResponse> DuplexStreaming()
        {
            return CallInvoker.AsyncDuplexStreamingCall(TestRpcMethods.SendAndReceive, "", new CallOptions());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AsyncDuplexStreamingCall<TestRequestStruct, TestResponseStruct> StructDuplexStreaming()
        {
            return CallInvoker.StructAsyncDuplexStreamingCall(TestRpcMethods.SendAndReceiveStruct, "", new CallOptions());
        }

    }
}
