using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{
    [TestClass]
    public class UnitTest_HeavyLoop
    {

        int ClientRepeatCount = 10000;
        int ServerRepeatCount = 100;

        [TestMethod]
        public void BlockingUnaryCall()
        {
            for (int i = 0; i < ClientRepeatCount; ++i)
            {
                TestRequest request = UnitTest1.GetRequest(1);
                TestResponse response = UnitTest1.Client.BlockingUnaryCall(request);
            }
        }

        [TestMethod]
        public void StructBlockingUnaryCall()
        {
            for (int i = 0; i < ClientRepeatCount; ++i)
            {
                TestRequestStruct request = UnitTest1.GetRequestStruct(1);
                TestResponseStruct response = UnitTest1.Client.StructBlockingUnaryCall(request);
            }
        }

        [TestMethod]
        public async Task AsyncUnaryCall()
        {
            for (int i = 0; i < ClientRepeatCount; ++i)
            {
                TestRequest request = UnitTest1.GetRequest(1);
                TestResponse response = await UnitTest1.Client.AsyncUnaryCall(request).ConfigureAwait(false);
            }
        }

        [TestMethod]
        public async Task StructAsyncUnaryCall()
        {
            for (int i = 0; i < ClientRepeatCount; ++i)
            {
                TestRequestStruct request = UnitTest1.GetRequestStruct(1);
                TestResponseStruct response = await UnitTest1.Client.StructAsyncUnaryCall(request).ConfigureAwait(false);
            }
        }

        [TestMethod]
        public async Task ClientStreaming()
        {
            using (var call = UnitTest1.Client.ClientStreaming())
            {
                for (int i = 0; i < ClientRepeatCount; ++i)
                {
                    await call.RequestStream.WriteAsync(UnitTest1.GetRequest(1)).ConfigureAwait(false);
                }
                await call.RequestStream.CompleteAsync().ConfigureAwait(false);
                TestResponse response = await call.ResponseAsync.ConfigureAwait(false);
            }
        }

        [TestMethod]
        public async Task StructClientStreaming()
        {
            using (var call = UnitTest1.Client.StructClientStreaming())
            {
                for (int i = 0; i < ClientRepeatCount; ++i)
                {
                    await call.RequestStream.WriteAsync(UnitTest1.GetRequestStruct(1)).ConfigureAwait(false);
                }
                await call.RequestStream.CompleteAsync().ConfigureAwait(false);
                TestResponseStruct response = await call.ResponseAsync.ConfigureAwait(false);
            }
        }

        [TestMethod]
        public async Task ServerStreaming()
        {
            using (var call = UnitTest1.Client.ServerStreaming(UnitTest1.GetRequest(ServerRepeatCount)))
            {
                while (await call.ResponseStream.MoveNext(System.Threading.CancellationToken.None).ConfigureAwait(false))
                {
                    TestResponse response = call.ResponseStream.Current;
                }
            }
        }

        [TestMethod]
        public async Task StructServerStreaming()
        {
            using (var call = UnitTest1.Client.StructServerStreaming(UnitTest1.GetRequestStruct(ServerRepeatCount)))
            {
                while (await call.ResponseStream.MoveNext(System.Threading.CancellationToken.None).ConfigureAwait(false))
                {
                    TestResponseStruct response = call.ResponseStream.Current;
                }
            }
        }

        [TestMethod]
        public async Task DuplexStreaming()
        {
            void onResponse(TestResponse response)
            {
                int value = response.IntValue;
            };

            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequests(ServerRepeatCount), (Action<TestResponse>)onResponse);
            }
        }

        [TestMethod]
        public async Task StructDuplexStreaming()
        {
            void onResponse(TestResponseStruct response)
            {
                int value = response.IntValue;
            };

            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequestStructs(ServerRepeatCount), (Action<TestResponseStruct>)onResponse);
            }
        }

    }
}
