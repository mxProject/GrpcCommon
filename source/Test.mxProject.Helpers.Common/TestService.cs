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
    public sealed class TestService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<TestResponseStruct> GetResponseStruct(TestRequestStruct request, ServerCallContext context)
        {
            return Task.FromResult(new TestResponseStruct { IntValue = request.IntValue * -1, DateTimeValue = request.DateTimeValue.AddDays(1) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<TestResponse> GetResponse(TestRequest request, ServerCallContext context)
        {
            return Task.FromResult(new TestResponse { IntValue = request.IntValue * -1, DateTimeValue = request.DateTimeValue.AddDays(1) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestReader"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<TestResponseStruct> SendRequestsStruct(IAsyncStreamReader<TestRequestStruct> requestReader, ServerCallContext context)
        {
            int summary = 0;
            DateTime? dateTime = null;

            while (await requestReader.MoveNext(context.CancellationToken).ConfigureAwait(false))
            {
                summary += requestReader.Current.IntValue;
                if (!dateTime.HasValue) { dateTime = requestReader.Current.DateTimeValue; }
            }

            return new TestResponseStruct { IntValue = summary, DateTimeValue = dateTime.GetValueOrDefault() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestReader"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<TestResponse> SendRequests(IAsyncStreamReader<TestRequest> requestReader, ServerCallContext context)
        {
            int summary = 0;
            DateTime? dateTime = null;

            while (await requestReader.MoveNext(context.CancellationToken).ConfigureAwait(false))
            {
                summary += requestReader.Current.IntValue;
                if (!dateTime.HasValue) { dateTime = requestReader.Current.DateTimeValue; }
            }

            return new TestResponse { IntValue = summary, DateTimeValue = dateTime.GetValueOrDefault() };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveResponsesStruct(TestRequestStruct request, IServerStreamWriter<TestResponseStruct> responseWriter, ServerCallContext context)
        {
            for (int i = 0; i < request.IntValue; ++i)
            {
                TestResponseStruct response = new TestResponseStruct() { IntValue = i + 1, DateTimeValue = request.DateTimeValue.AddDays(1) };
                await responseWriter.WriteAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ReceiveResponses(TestRequest request, IServerStreamWriter<TestResponse> responseWriter, ServerCallContext context)
        {
            for (int i = 0; i < request.IntValue; ++i)
            {
                TestResponse response = new TestResponse() { IntValue = i + 1, DateTimeValue = request.DateTimeValue.AddDays(1) };
                await responseWriter.WriteAsync(response).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestReader"></param>
        /// <param name="responseWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SendAndReceiveStruct(IAsyncStreamReader<TestRequestStruct> requestReader, IServerStreamWriter<TestResponseStruct> responseWriter, ServerCallContext context)
        {
            while (await requestReader.MoveNext(context.CancellationToken).ConfigureAwait(false))
            {
                for (int i = 0; i < requestReader.Current.IntValue; ++i)
                {
                    TestResponseStruct response = new TestResponseStruct() { IntValue = i + 1, DateTimeValue = requestReader.Current.DateTimeValue.AddDays(1) };
                    await responseWriter.WriteAsync(response).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestReader"></param>
        /// <param name="responseWriter"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SendAndReceive(IAsyncStreamReader<TestRequest> requestReader, IServerStreamWriter<TestResponse> responseWriter, ServerCallContext context)
        {
            while (await requestReader.MoveNext(context.CancellationToken).ConfigureAwait(false))
            {
                for (int i = 0; i < requestReader.Current.IntValue; ++i)
                {
                    TestResponse response = new TestResponse() { IntValue = i + 1, DateTimeValue = requestReader.Current.DateTimeValue.AddDays(1) };
                    await responseWriter.WriteAsync(response).ConfigureAwait(false);
                }
            }
        }

    }
}
