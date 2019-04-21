using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="IServerStreamWriter{TResponse}"/>.
    /// </summary>
    public static class IServerStreamWriterExtensions
    {

        #region WritesAsync

        /// <summary>
        /// Writes the specified objects.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="responses">The responses.</param>
        /// <returns></returns>
        public static async Task WritesAsync<TResponse>(this IServerStreamWriter<TResponse> streamWriter, IEnumerable<TResponse> responses)
        {
            foreach (TResponse request in responses)
            {
                await streamWriter.WriteAsync(request).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes the specified objects.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="responses">The responses.</param>
        /// <returns></returns>
        public static async Task WritesAsync<TResponse>(this IServerStreamWriter<TResponse> streamWriter, IEnumerable<Task<TResponse>> responses)
        {
            foreach (Task<TResponse> request in responses)
            {
                await streamWriter.WriteAsync(await request.ConfigureAwait(false)).ConfigureAwait(false);
            }
        }

        #endregion

        #region ConvertResponse

        /// <summary>
        /// Converts the object type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a response.</param>
        /// <returns>A stream writer to write a source object.</returns>
        public static IServerStreamWriter<TSource> ConvertResponse<TSource, TResponse>(this IServerStreamWriter<TResponse> streamWriter, Func<TSource, TResponse> requestConverter)
        {
            return new Internals.ConvertServerStreamWriter<TSource, TResponse>(streamWriter, requestConverter);
        }

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a response.</param>
        /// <returns>A stream writer to write a source object.</returns>
        public static IServerStreamWriter<TSource> ConvertResponse<TSource, TResponse>(this IServerStreamWriter<TResponse> streamWriter, Func<TSource, Task<TResponse>> requestConverter)
        {
            return new Internals.AsyncConvertAsyncServerWriter<TSource, TResponse>(streamWriter, requestConverter);
        }

        #endregion

    }

}
