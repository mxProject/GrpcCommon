using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grpc.Core;

using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestAsyncServerStreamingCall
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ReadAllAsync()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            using (var call = UnitTest1.Client.ServerStreaming(request))
            {
                IList<TestResponse> responses = await call.ReadAllAsync().ConfigureAwait(false);

                Assert.AreEqual(3, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructReadAllAsync()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 3, DateTimeValue = DateTime.UtcNow };

            using (var call = UnitTest1.Client.StructServerStreaming(request))
            {
                IList<TestResponseStruct> responses = await call.ReadAllAsync().ConfigureAwait(false);

                Assert.AreEqual(3, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task FillAllAsync()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            using (var call = UnitTest1.Client.ServerStreaming(request))
            {
                List<TestResponse> responses = new List<TestResponse>();

                await call.FillAllAsync(responses).ConfigureAwait(false);

                Assert.AreEqual(3, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructFillAllAsync()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 3, DateTimeValue = DateTime.UtcNow };

            using (var call = UnitTest1.Client.StructServerStreaming(request))
            {
                List<TestResponseStruct> responses = new List<TestResponseStruct>();

                await call.FillAllAsync(responses).ConfigureAwait(false);

                Assert.AreEqual(3, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ForEachAsync()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            int count = 0;

            void onResponse(TestResponse response)
            {
                Console.WriteLine(response.IntValue);
                ++count;
            }

            using (var call = UnitTest1.Client.ServerStreaming(request))
            {
                await call.ForEachAsync((Action<TestResponse>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructForEachAsync()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 3, DateTimeValue = DateTime.UtcNow };

            int count = 0;

            void onResponse(TestResponseStruct response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
                ++count;
            }

            using (var call = UnitTest1.Client.StructServerStreaming(request))
            {
                await call.ForEachAsync((Action<TestResponseStruct>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ForEachAsync_Async()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            int count = 0;

            Task onResponse(TestResponse response)
            {
                Console.WriteLine(response.IntValue);
                ++count;
                return Task.CompletedTask;
            }

            using (var call = UnitTest1.Client.ServerStreaming(request))
            {
                await call.ForEachAsync((Func<TestResponse, Task>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructForEachAsync_Async()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 3, DateTimeValue = DateTime.UtcNow };

            int count = 0;

            Task onResponse(TestResponseStruct response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
                ++count;
                return Task.CompletedTask;
            }

            using (var call = UnitTest1.Client.StructServerStreaming(request))
            {
                await call.ForEachAsync((Func<TestResponseStruct, Task>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertResponse()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            int count = 0;

            void onResponse(string response)
            {
                Console.WriteLine(response);
                ++count;
            }

            using (var call = UnitTest1.Client.ServerStreaming(request).ConvertResponse(res => $"IntValue:{res.IntValue} DateTime:{res.DateTimeValue}"))
            {
                await call.ForEachAsync((Action<string>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertResponse()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 3, DateTimeValue = DateTime.UtcNow };

            int count = 0;

            void onResponse(string response)
            {
                Console.WriteLine(response);
                ++count;
            }

            using (var call = UnitTest1.Client.StructServerStreaming(request).ConvertResponse(res => $"IntValue:{res.IntValue} DateTime:{res.DateTimeValue}"))
            {
                await call.ForEachAsync((Action<string>)onResponse).ConfigureAwait(false);

                Assert.AreEqual(3, count);
            }
        }

    }
}
