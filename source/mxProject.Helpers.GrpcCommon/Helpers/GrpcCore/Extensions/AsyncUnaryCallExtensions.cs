using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="AsyncUnaryCall{TResponse}"/>.
    /// </summary>
    public static class AsyncUnaryCallExtensions
    {

        #region ConvertResponse

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncUnaryCall<TResult> ConvertResponse<TResponse, TResult>(this AsyncUnaryCall<TResponse> call, Func<TResponse, TResult> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, TResult> converter)
            {
                return converter(await response.ConfigureAwait(false));
            }
            
            return new AsyncUnaryCall<TResult>(
                convert(call.ResponseAsync, responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        /// <summary>
        /// Converts the reponse type.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="call"></param>
        /// <param name="responseConverter">Method to convert a response.</param>
        /// <returns>A streaming call with response type converted.</returns>
        public static AsyncUnaryCall<TResult> ConvertResponse<TResponse, TResult>(this AsyncUnaryCall<TResponse> call, Func<TResponse, Task<TResult>> responseConverter)
        {
            async Task<TResult> convert(Task<TResponse> response, Func<TResponse, Task<TResult>> converter)
            {
                return await converter(await response.ConfigureAwait(false)).ConfigureAwait(false);
            }

            return new AsyncUnaryCall<TResult>(
                convert(call.ResponseAsync, responseConverter)
                , call.ResponseHeadersAsync
                , call.GetStatus
                , call.GetTrailers
                , call.Dispose
                );
        }

        #endregion

    }

}
