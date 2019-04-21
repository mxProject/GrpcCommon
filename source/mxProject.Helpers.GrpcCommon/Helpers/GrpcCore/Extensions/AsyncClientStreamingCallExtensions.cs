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
    public static class AsyncClientStreamingCallExtensions
    {

        #region WriteRequestsAsync

        /// <summary>
        /// Writes the specified requests.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static Task WriteRequestsAsync<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests)
        {
            return call.RequestStream.WriteRequestsAsync(requests);
        }

        /// <summary>
        /// Writes the specified requests.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static Task WriteRequestsAsync<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests)
        {
            return call.RequestStream.WriteRequestsAsync(requests);
        }

        #endregion

        #region WriteRequestsAndCompleteAsync

        /// <summary>
        /// Write the specified requests and send a completion.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns>The response.</returns>
        public static async Task<TResponse> WriteRequestsAndCompleteAsync<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, IEnumerable<TRequest> requests)
        {

            await call.RequestStream.WriteRequestsAsync(requests).ConfigureAwait(false);

            await call.RequestStream.CompleteAsync().ConfigureAwait(false);

            return await call.ResponseAsync.ConfigureAwait(false);

        }

        /// <summary>
        /// Write the specified requests and send a completion.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="requests">The requests.</param>
        /// <returns>The response.</returns>
        public static async Task<TResponse> WriteRequestsAndCompleteAsync<TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, IEnumerable<Task<TRequest>> requests)
        {

            await call.RequestStream.WriteRequestsAsync(requests).ConfigureAwait(false);

            await call.RequestStream.CompleteAsync().ConfigureAwait(false);

            return await call.ResponseAsync.ConfigureAwait(false);

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
        public static AsyncClientStreamingCall<TSource, TResponse> ConvertRequest<TSource, TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter)
        {
            return new AsyncClientStreamingCall<TSource, TResponse>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseAsync
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
        public static AsyncClientStreamingCall<TSource, TResponse> ConvertRequest<TSource, TRequest, TResponse>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter)
        {
            return new AsyncClientStreamingCall<TSource, TResponse>(
                call.RequestStream.ConvertRequest(requestConverter)
                , call.ResponseAsync
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
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncClientStreamingCall<TRequest, TResult> ConvertResponse<TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TResponse, TResult> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, TResult> converter)
            {
                return converter(await response.ConfigureAwait(false));
            }

            return new AsyncClientStreamingCall<TRequest, TResult>(
                call.RequestStream
                , convert(call.ResponseAsync, responseConverter)
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
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncClientStreamingCall<TRequest, TResult> ConvertResponse<TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TResponse, Task<TResult>> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, Task<TResult>> converter)
            {
                return await converter(await response.ConfigureAwait(false)).ConfigureAwait(false);
            }

            return new AsyncClientStreamingCall<TRequest, TResult>(
                call.RequestStream
                , convert(call.ResponseAsync, responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

        #region ConvertRequestResponse

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncClientStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter, Func<TResponse, TResult> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, TResult> converter)
            {
                return converter(await response.ConfigureAwait(false));
            }

            return new AsyncClientStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , convert(call.ResponseAsync, responseConverter)
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
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncClientStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter, Func<TResponse, TResult> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, TResult> converter)
            {
                return converter(await response.ConfigureAwait(false));
            }

            return new AsyncClientStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , convert(call.ResponseAsync, responseConverter)
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
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncClientStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, TRequest> requestConverter, Func<TResponse, Task<TResult>> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, Task<TResult>> converter)
            {
                return await converter(await response.ConfigureAwait(false)).ConfigureAwait(false);
            }

            return new AsyncClientStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , convert(call.ResponseAsync, responseConverter)
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
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with request type converted.</returns>
        public static AsyncClientStreamingCall<TSource, TResult> ConvertRequestResponse<TSource, TRequest, TResponse, TResult>(this AsyncClientStreamingCall<TRequest, TResponse> call, Func<TSource, Task<TRequest>> requestConverter, Func<TResponse, Task<TResult>> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, Task<TResult>> converter)
            {
                return await converter(await response.ConfigureAwait(false)).ConfigureAwait(false);
            }

            return new AsyncClientStreamingCall<TSource, TResult>(
                call.RequestStream.ConvertRequest(requestConverter)
                , convert(call.ResponseAsync, responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

    }

}
