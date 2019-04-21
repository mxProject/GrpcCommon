using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{
    [TestClass]
    public class TestAsyncUnaryCall
    {

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void BlockingUnaryCall()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            TestResponse response = UnitTest1.Client.BlockingUnaryCall(request);

            Assert.AreEqual(request.IntValue * -1, response.IntValue);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StructBlockingUnaryCall()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 2, DateTimeValue = DateTime.UtcNow };

            TestResponseStruct response = UnitTest1.Client.StructBlockingUnaryCall(request);

            Assert.AreEqual(request.IntValue * -1, response.IntValue);
            Assert.AreEqual(request.DateTimeValue.AddDays(1), response.DateTimeValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AsyncUnaryCall()
        {
            TestRequest request = new TestRequest { IntValue = 3 };

            TestResponse response = await UnitTest1.Client.AsyncUnaryCall(request).ConfigureAwait(false);

            Assert.AreEqual(request.IntValue * -1, response.IntValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task StructAsyncUnaryCall()
        {
            TestRequestStruct request = new TestRequestStruct { IntValue = 2, DateTimeValue = DateTime.UtcNow };

            TestResponseStruct response = await UnitTest1.Client.StructAsyncUnaryCall(request).ConfigureAwait(false);

            Assert.AreEqual(request.IntValue * -1, response.IntValue);
            Assert.AreEqual(request.DateTimeValue.AddDays(1), response.DateTimeValue);
        }

    }

}
