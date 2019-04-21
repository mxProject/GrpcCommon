using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="AsyncClientStreamingCall{TRequest, TResponse}"/>.
    /// </summary>
    public static class AsyncDuplexStreamingCallExtensions
    {

        #region WriteAllAndReadAsync

        /// <summary>
        /// Writes the specified requests and reads the responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns>The responses.</returns>
        public static async Task<IList<TResponse>> WriteAllAndReadAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task<IList<TResponse>> receive = call.ResponseStream.ReadAllAsync();

            await Task.WhenAll(send, receive).ConfigureAwait(false);

            return await receive;

        }

        /// <summary>
        /// Writes the specified requests and reads the responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns>The responses.</returns>
        public static async Task<IList<TResponse>> WriteAllAndReadAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task<IList<TResponse>> receive = call.ResponseStream.ReadAllAsync();

            await Task.WhenAll(send, receive).ConfigureAwait(false);

            return await receive;

        }

        /// <summary>
        /// Writes the specified requests and reads the responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="buffer">The collection that stores readed responses.</param>
        /// <returns></returns>
        public static Task WriteAllAndFillAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests, IList<TResponse> buffer)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.FillAllAsync(buffer);

            return Task.WhenAll(send, receive);

        }

        /// <summary>
        /// Writes the specified requests and reads the responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="buffer">The collection that stores readed responses.</param>
        /// <returns></returns>
        public static Task WriteAllAndFillAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests, IList<TResponse> buffer)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.FillAllAsync(buffer);

            return Task.WhenAll(send, receive);

        }

        #endregion

        #region WriteAllAndForEachAsync

        /// <summary>
        /// Writes the specified requests and executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="onResponse">The method.</param>
        /// <returns></returns>
        public static Task WriteAllAndForEachAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests, Action<TResponse> onResponse)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.ForEachAsync(onResponse);

            return Task.WhenAll(send, receive);

        }

        /// <summary>
        /// Writes the specified requests and executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="onResponse">The method.</param>
        /// <returns></returns>
        public static Task WriteAllAndForEachAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests, Action<TResponse> onResponse)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.ForEachAsync(onResponse);

            return Task.WhenAll(send, receive);

        }

        /// <summary>
        /// Writes the specified requests and executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="onResponse">The method.</param>
        /// <returns></returns>
        public static Task WriteAllAndForEachAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests, Func<TResponse, Task> onResponse)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.ForEachAsync(onResponse);

            return Task.WhenAll(send, receive);

        }

        /// <summary>
        /// Writes the specified requests and executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <param name="onResponse">The method.</param>
        /// <returns></returns>
        public static Task WriteAllAndForEachAsync<TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests, Func<TResponse, Task> onResponse)
        {

            Task send = call.RequestStream.WriteRequestsAndCompleteAsync(requests);
            Task receive = call.ResponseStream.ForEachAsync(onResponse);

            return Task.WhenAll(send, receive);

        }

        #endregion

        #region ConvertRequest

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResponse> ConvertRequest<TSource, TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResponse>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResponse> ConvertRequest<TSource, TRequest, TResponse>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResponse>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

        #region ConvertResponse

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TRequest, TResult> ConvertResponse<TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TResponse, TResult> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TRequest, TResult>(
                call.RequestStream
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TRequest, TResult> ConvertResponse<TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TResponse, Task<TResult>> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TRequest, TResult>(
                call.RequestStream
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

        #region ConvertRequestResponse

        /// <summary>
        /// Converts the request and reponse type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter, Func<TResponse, TResult> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the request and reponse type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter, Func<TResponse, TResult> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the request and reponse type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter, Func<TResponse, Task<TResult>> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the request and reponse type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request and response type converted.</returns>
        public static AsyncDuplexStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncDuplexStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter, Func<TResponse, Task<TResult>> responseConverter)
        {
            return new AsyncDuplexStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

    }

}
