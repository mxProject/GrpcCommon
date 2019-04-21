using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions.Internals
{

    /// <summary>
    /// Stream reader that returns converted objects.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <typeparam name="TResult">The type after conversion.</typeparam>
    internal sealed class ConvertAsyncStreamReader<T, TResult> : IAsyncStreamReader<TResult>
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        /// <param name="converter">Conversion method.</param>
        internal ConvertAsyncStreamReader(IAsyncStreamReader<T> streamReader, Func<T, TResult> converter)
        {
            m_StreamReader = streamReader;
            m_Converter = converter;
        }

        private readonly IAsyncStreamReader<T> m_StreamReader;
        private readonly Func<T, TResult> m_Converter;

        /// <summary>
        /// 
        /// </summary>
        public TResult Current
        {
            get { return m_Converter(m_StreamReader.Current); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            m_StreamReader.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return m_StreamReader.MoveNext(cancellationToken);
        }

    }

}
