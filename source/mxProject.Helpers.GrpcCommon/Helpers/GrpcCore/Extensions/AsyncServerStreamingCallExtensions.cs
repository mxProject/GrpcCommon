using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="AsyncServerStreamingCall{TResponse}"/>.
    /// </summary>
    public static class AsyncServerStreamingCallExtensions
    {

        #region ReadAllAsync

        /// <summary>
        /// Reads all responses.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <returns>The responses </returns>
        public static Task<IList<TResponse>> ReadAllAsync<TResponse>(this AsyncServerStreamingCall<TResponse> call)
        {
            return call.ResponseStream.ReadAllAsync();
        }

        /// <summary>
        /// Reads all responses.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="buffer">The collection that stores readed responses.</param>
        /// <remarks></remarks>
        public static Task FillAllAsync<TResponse>(this AsyncServerStreamingCall<TResponse> call, IList<TResponse> buffer)
        {
            return call.ResponseStream.FillAllAsync(buffer);
        }

        #endregion

        #region ForEachAsync

        /// <summary>
        /// Executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="action">The method.</param>
        /// <returns></returns>
        public static Task ForEachAsync<TResponse>(this AsyncServerStreamingCall<TResponse> call, Action<TResponse> action)
        {
            return call.ResponseStream.ForEachAsync(action);
        }

        /// <summary>
        /// Executes the specified method on the readed responses.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="call"></param>
        /// <param name="asyncAction">The method.</param>
        /// <returns></returns>
        public static Task ForEachAsync<TResponse>(this AsyncServerStreamingCall<TResponse> call, Func<TResponse, Task> asyncAction)
        {
            return call.ResponseStream.ForEachAsync(asyncAction);
        }

        #endregion

        #region ConvertResponse

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncServerStreamingCall<TResult> ConvertResponse<TResponse, TResult>(this AsyncServerStreamingCall<TResponse> call, Func<TResponse, TResult> responseConverter)
        {
            return new AsyncServerStreamingCall<TResult>(
                call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncServerStreamingCall<TResult> ConvertResponse<TResponse, TResult>(this AsyncServerStreamingCall<TResponse> call, Func<TResponse, Task<TResult>> responseConverter)
        {
            return new AsyncServerStreamingCall<TResult>(
                call.ResponseStream.Convert(responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

    }

}
