using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="IAsyncStreamWriter{T}"/>.
    /// </summary>
    public static class IAsyncStreamWriterExtensions
    {

        #region WritesAsync

        /// <summary>
        /// Writes the specified objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        public static async Task WritesAsync<T>(this IAsyncStreamWriter<T> streamWriter, IEnumerable<T> objects)
        {
            foreach (T request in objects)
            {
                await streamWriter.WriteAsync(request).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Writes the specified objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="objects">The objects.</param>
        /// <returns></returns>
        public static async Task WritesAsync<T>(this IAsyncStreamWriter<T> streamWriter, IEnumerable<Task<T>> objects)
        {
            foreach (Task<T> request in objects)
            {
                await streamWriter.WriteAsync(await request.ConfigureAwait(false)).ConfigureAwait(false);
            }
        }

        #endregion

        #region Convert

        /// <summary>
        /// Converts the object type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a object.</param>
        /// <returns>A stream writer to write a converted object.</returns>
        public static IAsyncStreamWriter<TSource> Convert<TSource, T>(this IAsyncStreamWriter<T> streamWriter, Func<TSource, T> requestConverter)
        {
            return new Internals.ConvertAsyncStreamWriter<TSource, T>(streamWriter, requestConverter);
        }

        /// <summary>
        /// Converts the request type.
        /// </summary>
        /// <typeparam name="TSource">The type before conversion.</typeparam>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamWriter"></param>
        /// <param name="requestConverter">Method to convert a object.</param>
        /// <returns>A stream writer to write a converted object.</returns>
        public static IAsyncStreamWriter<TSource> Convert<TSource, T>(this IAsyncStreamWriter<T> streamWriter, Func<TSource, Task<T>> requestConverter)
        {
            return new Internals.AsyncConvertAsyncStreamWriter<TSource, T>(streamWriter, requestConverter);
        }

        #endregion

    }

}
