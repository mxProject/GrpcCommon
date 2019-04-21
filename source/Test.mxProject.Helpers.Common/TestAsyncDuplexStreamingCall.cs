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
    public class TestAsyncDuplexStreamingCall
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndReadAsync()
        {
            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                IList<TestResponse> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequests(3)).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndReadAsync()
        {
            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                IList<TestResponseStruct> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequestStructs(3)).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndReadAsync_Async()
        {
            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                IList<TestResponse> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequestsAsync(3)).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndReadAsync_Async()
        {
            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                IList<TestResponseStruct> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequestStructsAsync(3)).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndFillAsync()
        {
            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                List<TestResponse> responses = new List<TestResponse>();
                await call.WriteAllAndFillAsync(UnitTest1.GetRequests(3), responses).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndFillAsync()
        {
            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                List<TestResponseStruct> responses = new List<TestResponseStruct>();
                await call.WriteAllAndFillAsync(UnitTest1.GetRequestStructs(3), responses).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndFillAsync_Async()
        {
            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                List<TestResponse> responses = new List<TestResponse>();
                await call.WriteAllAndFillAsync(UnitTest1.GetRequestsAsync(3), responses).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndFillAsync_Async()
        {
            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                List<TestResponseStruct> responses = new List<TestResponseStruct>();
                await call.WriteAllAndFillAsync(UnitTest1.GetRequestStructsAsync(3), responses).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndForEachAsync()
        {
            void onReponse(TestResponse response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
            }

            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequests(4), (Action<TestResponse>)onReponse).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndForEachAsync()
        {
            void onReponse(TestResponseStruct response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
            }

            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequestStructs(4), (Action<TestResponseStruct>)onReponse).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteAllAndForEachAsync_Async()
        {
            Task onReponse(TestResponse response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
                return Task.CompletedTask;
            }

            using (var call = UnitTest1.Client.DuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequests(4), onReponse).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructWriteAllAndForEachAsync_Async()
        {
            Task onReponse(TestResponseStruct response)
            {
                Console.WriteLine($"{response.IntValue}, {response.DateTimeValue}");
                return Task.CompletedTask;
            }

            using (var call = UnitTest1.Client.StructDuplexStreaming())
            {
                await call.WriteAllAndForEachAsync(UnitTest1.GetRequestStructs(4), onReponse).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertRequest()
        {
            TestRequest convert(string s)
            {
                return new TestRequest { IntValue = int.Parse(s), DateTimeValue = DateTime.UtcNow };
            }

            using (var call = UnitTest1.Client.DuplexStreaming().ConvertRequest((Func<string, TestRequest>)convert))
            {
                IList<TestResponse> responses = await call.WriteAllAndReadAsync(new string[] { "1", "2", "3" }).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertRequest()
        {
            TestRequestStruct convert(string s)
            {
                return new TestRequestStruct { IntValue = int.Parse(s), DateTimeValue = DateTime.UtcNow };
            }

            using (var call = UnitTest1.Client.StructDuplexStreaming().ConvertRequest((Func<string, TestRequestStruct>)convert))
            {
                IList<TestResponseStruct> responses = await call.WriteAllAndReadAsync(new string[] { "1", "2", "3" }).ConfigureAwait(false);

                Assert.AreEqual(6, responses.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertResponse()
        {
            int convert(TestResponse response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.DuplexStreaming().ConvertResponse((Func<TestResponse, int>)convert))
            {
                IList<int> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequests(3)).ConfigureAwait(false);

                int summary = 0;

                foreach (int value in responses)
                {
                    summary += value;
                }

                Assert.AreEqual(10, summary);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertResponse()
        {
            int convert(TestResponseStruct response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.StructDuplexStreaming().ConvertResponse((Func<TestResponseStruct, int>)convert))
            {
                IList<int> responses = await call.WriteAllAndReadAsync(UnitTest1.GetRequestStructs(3)).ConfigureAwait(false);

                int summary = 0;

                foreach (int value in responses)
                {
                    summary += value;
                }

                Assert.AreEqual(10, summary);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertRequestResponse()
        {
            TestRequest requestConvert(string s)
            {
                return new TestRequest { IntValue = int.Parse(s), DateTimeValue = DateTime.UtcNow };
            }

            int responseConvert(TestResponse response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.DuplexStreaming().ConvertRequestResponse((Func<string, TestRequest>)requestConvert, (Func<TestResponse, int>)responseConvert))
            {
                IList<int> responses = await call.WriteAllAndReadAsync(new string[] { "5", "3" }).ConfigureAwait(false);

                int summary = 0;

                foreach (int value in responses)
                {
                    summary += value;
                }

                Assert.AreEqual(21, summary);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertRequestResponse()
        {
            TestRequestStruct requestConvert(string s)
            {
                return new TestRequestStruct { IntValue = int.Parse(s), DateTimeValue = DateTime.UtcNow };
            }

            int responseConvert(TestResponseStruct response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.StructDuplexStreaming().ConvertRequestResponse((Func<string, TestRequestStruct>)requestConvert, (Func<TestResponseStruct, int>)responseConvert))
            {
                IList<int> responses = await call.WriteAllAndReadAsync(new string[] { "5", "3" }).ConfigureAwait(false);

                int summary = 0;

                foreach (int value in responses)
                {
                    summary += value;
                }

                Assert.AreEqual(21, summary);
            }
        }

    }
}
