using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="IAsyncStreamReader{T}"/>.
    /// </summary>
    public static class IAsyncStreamReaderExtensions
    {

        #region ReadAllAsync

        /// <summary>
        /// Reads all objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamReader"></param>
        /// <returns>The responses.</returns>
        public static async Task<IList<T>> ReadAllAsync<T>(this IAsyncStreamReader<T> streamReader)
        {
            List<T> responses = new List<T>();

            while (await streamReader.MoveNext().ConfigureAwait(false))
            {
                responses.Add(streamReader.Current);
            }

            return responses;
        }

        /// <summary>
        /// Reads all objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamReader"></param>
        /// <param name="buffer">The collection that stores readed objects.</param>
        /// <remarks></remarks>
        public static async Task FillAllAsync<T>(this IAsyncStreamReader<T> streamReader, IList<T> buffer)
        {
            while (await streamReader.MoveNext().ConfigureAwait(false))
            {
                buffer.Add(streamReader.Current);
            }
        }

        #endregion

        #region ForEachAsync

        /// <summary>
        /// Executes the specified method on the readed objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamReader"></param>
        /// <param name="action">The method.</param>
        /// <returns></returns>
        public static async Task ForEachAsync<T>(this IAsyncStreamReader<T> streamReader, Action<T> action)
        {
            while (await streamReader.MoveNext().ConfigureAwait(false))
            {
                action(streamReader.Current);
            }
        }

        /// <summary>
        /// Executes the specified method on the readed objects.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="streamReader"></param>
        /// <param name="asyncAction">The method.</param>
        /// <returns></returns>
        public static async Task ForEachAsync<T>(this IAsyncStreamReader<T> streamReader, Func<T, Task> asyncAction)
        {
            while (await streamReader.MoveNext().ConfigureAwait(false))
            {
                await asyncAction(streamReader.Current).ConfigureAwait(false);
            }
        }

        #endregion

        #region Convert

        /// <summary>
        /// Converts the readed object type.
        /// </summary>
        /// <typeparam name="T">The type of the readed object.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="streamReader"></param>
        /// <param name="converter">Method to convert the object.</param>
        /// <returns>A stream reader that returns the converted object.</returns>
        public static IAsyncStreamReader<TResult> Convert<T, TResult>(this IAsyncStreamReader<T> streamReader, Func<T, TResult> converter)
        {
            return new Internals.ConvertAsyncStreamReader<T, TResult>(streamReader, converter);
        }

        /// <summary>
        /// Converts the readed object type.
        /// </summary>
        /// <typeparam name="T">The type of the readed object.</typeparam>
        /// <typeparam name="TResult">The type after conversion.</typeparam>
        /// <param name="streamReader"></param>
        /// <param name="converter">Method to convert the object.</param>
        /// <returns>A stream reader that returns the converted object.</returns>
        public static IAsyncStreamReader<TResult> Convert<T, TResult>(this IAsyncStreamReader<T> streamReader, Func<T, Task<TResult>> converter)
        {
            return new Internals.AsyncConvertAsyncStreamReader<T, TResult>(streamReader, converter);
        }

        #endregion

    }

}
