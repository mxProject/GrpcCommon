using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grpc.Core;

using mxProject.Helpers.GrpcCore.Extensions;
using Grpc.Core.Internal;

namespace Test.mxProject.Helpers.Common
{
    [TestClass]
    public class TestCallOptions
    {

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddHeader()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                CallOptions original = CreateCallOptions(tokenSource.Token);
                CallOptions callOptions = original;

                callOptions = callOptions.AddHeader("a", "0");
                callOptions = callOptions.AddHeader("b", "1");

                Assert.AreEqual(true, callOptions.ContainsHeader("a"));
                Assert.AreEqual(true, callOptions.ContainsHeader("b"));
                Assert.AreEqual("1", callOptions.Headers.GetStringValueOrNull("a"));

                Assert.AreEqual(original.WriteOptions, callOptions.WriteOptions);
                Assert.AreEqual(original.CancellationToken, callOptions.CancellationToken);
                Assert.AreEqual(original.PropagationToken, callOptions.PropagationToken);
                Assert.AreEqual(original.Credentials, callOptions.Credentials);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SetHeader()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                CallOptions original = CreateCallOptions(tokenSource.Token);
                CallOptions callOptions = original;

                callOptions = callOptions.SetHeader("a", "0");
                callOptions = callOptions.SetHeader("b", "1");

                Assert.AreEqual(true, callOptions.ContainsHeader("a"));
                Assert.AreEqual(true, callOptions.ContainsHeader("b"));
                Assert.AreEqual("0", callOptions.Headers.GetStringValueOrNull("a"));

                Assert.AreEqual(original.WriteOptions, callOptions.WriteOptions);
                Assert.AreEqual(original.CancellationToken, callOptions.CancellationToken);
                Assert.AreEqual(original.PropagationToken, callOptions.PropagationToken);
                Assert.AreEqual(original.Credentials, callOptions.Credentials);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetHeaderStringValue()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                CallOptions callOptions = CreateCallOptions(tokenSource.Token);

                Assert.AreEqual("1", callOptions.GetHeaderStringValue("a"));
                Assert.IsNull(callOptions.GetHeaderStringValue("b"));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetHeaderBinaryValue()
        {
            using (CancellationTokenSource tokenSource = new CancellationTokenSource())
            {
                CallOptions callOptions = CreateCallOptions(tokenSource.Token);

                Assert.AreEqual(1, callOptions.GetHeaderBinaryValue("a" + Metadata.BinaryHeaderSuffix).Length);
                Assert.IsNull(callOptions.GetHeaderBinaryValue("b" + Metadata.BinaryHeaderSuffix));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        private CallOptions CreateCallOptions(CancellationToken cancellation)
        {

            Metadata header = new Metadata();
            header.AddStringValue("a", "1");
            header.AddBinaryValue("a" + Metadata.BinaryHeaderSuffix, new byte[] { 1 });

            WriteOptions writeOptions = new WriteOptions(WriteFlags.BufferHint | WriteFlags.NoCompress);

            ContextPropagationToken propagationToken = null;

            CallCredentials credentials = CallCredentials.FromInterceptor(new AsyncAuthInterceptor((context, meta) => Task.CompletedTask));

            return new CallOptions(header, DateTime.Now, cancellation, writeOptions, propagationToken, credentials);

        }

    }
}
