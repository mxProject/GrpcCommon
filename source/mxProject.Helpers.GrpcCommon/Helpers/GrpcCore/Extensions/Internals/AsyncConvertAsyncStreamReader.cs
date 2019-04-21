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
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <typeparam name="TResult">The type after conversion.</typeparam>
    internal sealed class AsyncConvertAsyncStreamReader<TResponse, TResult> : IAsyncStreamReader<TResult>
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="streamReader">The stream reader.</param>
        /// <param name="converter">Conversion method.</param>
        internal AsyncConvertAsyncStreamReader(IAsyncStreamReader<TResponse> streamReader, Func<TResponse, Task<TResult>> converter)
        {
            m_StreamReader = streamReader;
            m_Converter = converter;
        }

        private readonly IAsyncStreamReader<TResponse> m_StreamReader;
        private readonly Func<TResponse, Task<TResult>> m_Converter;
        private TResult m_Current;
        private bool m_Eof;

        /// <summary>
        /// 
        /// </summary>
        public TResult Current
        {
            get {
                if (m_Eof) { throw new Exception(); }
                return m_Current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            m_StreamReader.Dispose();
            (m_Current as IDisposable)?.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            if (await m_StreamReader.MoveNext(cancellationToken).ConfigureAwait(false))
            {
                m_Current = await m_Converter(m_StreamReader.Current).ConfigureAwait(false);
                return true;
            }
            else
            {
                m_Current = default;
                m_Eof = true;
                return false;
            }
        }

    }

}
