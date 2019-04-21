using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{
    [TestClass]
    public class UnitTest1
    {

        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            Server = new TestServer("localhost", 50051);
            Client = new TestClient("localhost", 50051);

            Server.Start();
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            Client.ShutdownAsync().ContinueWith(t => Server.ShutdownAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        internal static TestClient Client { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        internal static TestServer Server { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static TestRequest GetRequest(int value)
        {
            return new TestRequest { IntValue = value, DateTimeValue = DateTime.UtcNow };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static TestRequestStruct GetRequestStruct(int value)
        {
            return new TestRequestStruct { IntValue = value, DateTimeValue = DateTime.UtcNow };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static IEnumerable<TestRequest> GetRequests(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new TestRequest { IntValue = i + 1, DateTimeValue = DateTime.UtcNow };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static IEnumerable<Task<TestRequest>> GetRequestsAsync(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return Task.FromResult(new TestRequest { IntValue = i + 1, DateTimeValue = DateTime.UtcNow });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static IEnumerable<TestRequestStruct> GetRequestStructs(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return new TestRequestStruct { IntValue = i + 1, DateTimeValue = DateTime.UtcNow };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static IEnumerable<Task<TestRequestStruct>> GetRequestStructsAsync(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                yield return Task.FromResult(new TestRequestStruct { IntValue = i + 1, DateTimeValue = DateTime.UtcNow });
            }
        }

    }

}
