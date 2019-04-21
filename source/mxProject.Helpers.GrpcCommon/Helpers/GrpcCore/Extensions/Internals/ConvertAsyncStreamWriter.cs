﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions.Internals
{

    /// <summary>
    /// Stream writer to write converted objects.
    /// </summary>
    /// <typeparam name="TSource">The type before conversion.</typeparam>
    /// <typeparam name="T">The type of the object.</typeparam>
    internal sealed class ConvertAsyncStreamWriter<TSource, T> : IAsyncStreamWriter<TSource>
    {

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="streamWriter">The stream writer.</param>
        /// <param name="converter">Conversion method.</param>
        internal ConvertAsyncStreamWriter(IAsyncStreamWriter<T> streamWriter, Func<TSource, T> converter)
        {
            m_StreamWriter = streamWriter;
            m_Converter = converter;
        }

        private readonly IAsyncStreamWriter<T> m_StreamWriter;
        private readonly Func<TSource, T> m_Converter;

        /// <summary>
        /// 
        /// </summary>
        public WriteOptions WriteOptions
        {
            get => m_StreamWriter.WriteOptions;
            set => m_StreamWriter.WriteOptions = value;
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
