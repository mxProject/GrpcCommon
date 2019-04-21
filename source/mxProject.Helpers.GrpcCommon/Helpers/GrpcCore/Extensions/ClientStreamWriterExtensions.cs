using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="IClientStreamWriter{TRequest}"/>.
    /// </summary>
    public static class ClientStreamWriterExtensions
    {

        #region WritesAsync

        /// <summary>
        /// Writes the specified requests.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static async Task WriteRequestsAsync<TRequest>(this IClientStreamWriter<TRequest> streamWriter, IEnumerable<TRequest> requests)
        {
            foreach (TRequest request in requests)
            {
                await streamWriter.WriteAsync(request).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes the specified requests.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static async Task WriteRequestsAsync<TRequest>(this IClientStreamWriter<TRequest> streamWriter, IEnumerable<Task<TRequest>> requests)
        {
            foreach (Task<TRequest> request in requests)
            {
                await streamWriter.WriteAsync(await request.ConfigureAwait(false)).ConfigureAwait(false);
            }
        }

        #endregion

        #region WriteAndCompleteAsync

        /// <summary>
        /// Writes the specified requests and send a completion.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static async Task WriteRequestsAndCompleteAsync<TRequest>(this IClientStreamWriter<TRequest> streamWriter, IEnumerable<TRequest> requests)
        {
            foreach (TRequest request in requests)
            {
                await streamWriter.WriteAsync(request).ConfigureAwait(false);
            }
            await streamWriter.CompleteAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes the specified requests and send a completion.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public static async Task WriteRequestsAndCompleteAsync<TRequest>(this IClientStreamWriter<TRequest> streamWriter, IEnumerable<Task<TRequest>> requests)
        {
            foreach (Task<TRequest> request in requests)
            {
                await streamWriter.WriteAsync(await request.ConfigureAwait(false)).ConfigureAwait(false);
            }
            await streamWriter.CompleteAsync().ConfigureAwait(false);
        }

        #endregion

        #region ConvertRequest

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <returns>A stream writer to write a converted request.</returns>
        public static IClientStreamWriter<TSource> ConvertRequest<TSource, TRequest>(this IClientStreamWriter<TRequest> streamWriter, Func<TSource, TRequest> requestConverter)
        {
            return new Internals.ConvertClientStreamWriter<TSource, TRequest>(streamWriter, requestConverter);
        }

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a request.</param>
        /// <returns>A stream writer to write a converted request.</returns>
        public static IClientStreamWriter<TSource> ConvertRequest<TSource, TRequest>(this IClientStreamWriter<TRequest> streamWriter, Func<TSource, Task<TRequest>> requestConverter)
        {
            return new Internals.AsyncConvertClientStreamWriter<TSource, TRequest>(streamWriter, requestConverter);
        }

        #endregion

    }

}
