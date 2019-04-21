using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace Test.mxProject.Helpers.Common
{
    /// <summary>
    /// 
    /// </summary>
    internal static class TestRpcMethods
    {

        static TestRpcMethods()
        {

            Marshaller<TestRequest> requestMarshaller = MessagePackMarshaller.Current.GetMarshaller<TestRequest>();
            Marshaller<TestResponse> responseMarshaller = MessagePackMarshaller.Current.GetMarshaller<TestResponse>();

            Marshaller<TestRequestStruct> requestStructMarshaller = MessagePackMarshaller.Current.GetMarshaller<TestRequestStruct>();
            Marshaller<TestResponseStruct> responseStructMarshaller = MessagePackMarshaller.Current.GetMarshaller<TestResponseStruct>();

            GetResponse = new Method<TestRequest, TestResponse>(MethodType.Unary, "TestService", "GetResponse", requestMarshaller, responseMarshaller);
            SendRequests = new Method<TestRequest, TestResponse>(MethodType.ClientStreaming, "TestService", "SendRequests", requestMarshaller, responseMarshaller);
            ReceiveResponses = new Method<TestRequest, TestResponse>(MethodType.ServerStreaming, "TestService", "ReceiveResponses", requestMarshaller, responseMarshaller);
            SendAndReceive = new Method<TestRequest, TestResponse>(MethodType.DuplexStreaming, "TestService", "SendAndReceive", requestMarshaller, responseMarshaller);

            GetResponseStruct = new Method<TestRequestStruct, TestResponseStruct>(MethodType.Unary, "TestService", "GetResponseStruct", requestStructMarshaller, responseStructMarshaller);
            SendRequestsStruct = new Method<TestRequestStruct, TestResponseStruct>(MethodType.ClientStreaming, "TestService", "SendRequestsStruct", requestStructMarshaller, responseStructMarshaller);
            ReceiveResponsesStruct = new Method<TestRequestStruct, TestResponseStruct>(MethodType.ServerStreaming, "TestService", "ReceiveResponsesStruct", requestStructMarshaller, responseStructMarshaller);
            SendAndReceiveStruct = new Method<TestRequestStruct, TestResponseStruct>(MethodType.DuplexStreaming, "TestService", "SendAndReceiveStruct", requestStructMarshaller, responseStructMarshaller);
        }

        internal static Method<TestRequest, TestResponse> GetResponse { get; }
        internal static Method<TestRequest, TestResponse> SendRequests { get; }
        internal static Method<TestRequest, TestResponse> ReceiveResponses { get; }
        internal static Method<TestRequest, TestResponse> SendAndReceive { get; }

        internal static Method<TestRequestStruct, TestResponseStruct> GetResponseStruct { get; }
        internal static Method<TestRequestStruct, TestResponseStruct> SendRequestsStruct { get; }
        internal static Method<TestRequestStruct, TestResponseStruct> ReceiveResponsesStruct { get; }
        internal static Method<TestRequestStruct, TestResponseStruct> SendAndReceiveStruct { get; }
    }
}
