using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions.Internals
{

    /// <summary>
    /// Stream writer that writes converted objects.
    /// </summary>
    /// <typeparam name="TSource">The type before conversion.</typeparam>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    internal sealed class ConvertClientStreamWriter<TSource, TRequest> : IClientStreamWriter<TSource>
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="streamWriter">The stream writer.</param>
        /// <param name="converter">Conversion method.</param>
        internal ConvertClientStreamWriter(IClientStreamWriter<TRequest> streamWriter, Func<TSource, TRequest> converter)
        {
            m_StreamWriter = streamWriter;
            m_Converter = converter;
        }

        private readonly IClientStreamWriter<TRequest> m_StreamWriter;
        private readonly Func<TSource, TRequest> m_Converter;

        /// <summary>
        /// 
        /// </summary>
        public WriteOptions WriteOptions
        {
            get { return m_StreamWriter.WriteOptions; }
            set { m_StreamWriter.WriteOptions = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task CompleteAsync()
        {
            return m_StreamWriter.CompleteAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task WriteAsync(TSource message)
        {
            return m_StreamWriter.WriteAsync(m_Converter(message));
        }

    }

}
