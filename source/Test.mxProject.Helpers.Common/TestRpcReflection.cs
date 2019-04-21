using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Grpc.Core;
using mxProject.Helpers.GrpcCore.Reflection;
using mxProject.Helpers.GrpcCore.Extensions;

namespace Test.mxProject.Helpers.Common
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestRpcReflection
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EnumerateServiceMethods()
        {
            foreach (RpcMethodHandlerInfo methodHandler in RpcReflection.EnumerateServiceMethods(typeof(TestService), false))
            {
                Console.WriteLine($"[{methodHandler.MethodType}] {methodHandler.Handler.Name}<{methodHandler.RequestType.Name}, {methodHandler.ResponseType.Name}>");
            }
        }

    }
}
