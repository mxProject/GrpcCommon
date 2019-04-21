using System;
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
    public class TestAsyncClientStreamingCall
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteRequestsAsync()
        {
            using (var call = UnitTest1.Client.ClientStreaming())
            {
                await call.WriteRequestsAsync(UnitTest1.GetRequests(3)).ConfigureAwait(false);
                await call.WriteRequestsAsync(UnitTest1.GetRequestsAsync(3)).ConfigureAwait(false);
                await call.WriteRequestsAndCompleteAsync(UnitTest1.GetRequests(3)).ConfigureAwait(false);
                
                TestResponse response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(18, response.IntValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task WriteRequestStructsAsync()
        {
            using (var call = UnitTest1.Client.StructClientStreaming())
            {
                await call.WriteRequestsAsync(UnitTest1.GetRequestStructs(3)).ConfigureAwait(false);
                await call.WriteRequestsAsync(UnitTest1.GetRequestStructsAsync(3)).ConfigureAwait(false);
                await call.WriteRequestsAndCompleteAsync(UnitTest1.GetRequestStructs(3)).ConfigureAwait(false);

                TestResponseStruct response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(18, response.IntValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertRequest()
        {
            TestRequest convert(int value)
            {
                return new TestRequest { IntValue = value, DateTimeValue = DateTime.UtcNow };
            }

            using (var call = UnitTest1.Client.ClientStreaming().ConvertRequest((Func<int, TestRequest>)convert))
            {
                await call.WriteRequestsAndCompleteAsync(new int[] { 1, 2, 3 }).ConfigureAwait(false);

                TestResponse response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(6, response.IntValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertRequest()
        {
            TestRequestStruct convert(int value)
            {
                return new TestRequestStruct { IntValue = value, DateTimeValue = DateTime.UtcNow };
            }

            using (var call = UnitTest1.Client.StructClientStreaming().ConvertRequest((Func<int, TestRequestStruct>)convert))
            {
                await call.WriteRequestsAndCompleteAsync(new int[] { 1, 2, 3 }).ConfigureAwait(false);

                TestResponseStruct response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(6, response.IntValue);
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

            using (var call = UnitTest1.Client.ClientStreaming().ConvertResponse((Func<TestResponse, int>)convert))
            {
                await call.WriteRequestsAndCompleteAsync(UnitTest1.GetRequests(3)).ConfigureAwait(false);

                int response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(6, response);
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

            using (var call = UnitTest1.Client.StructClientStreaming().ConvertResponse((Func<TestResponseStruct, int>)convert))
            {
                await call.WriteRequestsAndCompleteAsync(UnitTest1.GetRequestStructs(3)).ConfigureAwait(false);

                int response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(6, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ConvertRequestResponse()
        {
            TestRequest requestConvert(int value)
            {
                return new TestRequest { IntValue = value, DateTimeValue = DateTime.UtcNow };
            }

            int responseConvert(TestResponse response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.ClientStreaming().ConvertRequestResponse((Func<int, TestRequest>)requestConvert, (Func<TestResponse, int>)responseConvert))
            {
                await call.WriteRequestsAndCompleteAsync(new int[] { 2, 3 }).ConfigureAwait(false);

                int response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(5, response);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructConvertRequestResponse()
        {
            TestRequestStruct requestConvert(int value)
            {
                return new TestRequestStruct { IntValue = value, DateTimeValue = DateTime.UtcNow };
            }

            int responseConvert(TestResponseStruct response)
            {
                return response.IntValue;
            }

            using (var call = UnitTest1.Client.StructClientStreaming().ConvertRequestResponse((Func<int, TestRequestStruct>)requestConvert, (Func<TestResponseStruct, int>)responseConvert))
            {
                await call.WriteRequestsAndCompleteAsync(new int[] { 2, 3 }).ConfigureAwait(false);

                int response = await call.ResponseAsync.ConfigureAwait(false);

                Assert.AreEqual(5, response);
            }
        }

    }
}
