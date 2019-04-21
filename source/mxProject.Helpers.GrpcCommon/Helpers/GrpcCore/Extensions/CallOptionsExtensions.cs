using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extension methods of <see cref="CallOptions"/>.
    /// </summary>
    public static class CallOptionsExtensions
    {

        #region AddHeader

        /// <summary>
        /// Adds the specified header. Nothing is done if it has already been set.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The haeder key representing a token string.</param>
        /// <param name="value">The string value.</param>
        /// <returns>A new callOptions.</returns>
        public static CallOptions AddHeader(this CallOptions callOptions, string headerKey, string value)
        {

            if (ContainsHeader(callOptions, headerKey)) { return callOptions; }

            Metadata headers = callOptions.Headers ?? new Metadata();

            headers.Add(headerKey, value ?? "");

            return new CallOptions(headers, callOptions.Deadline, callOptions.CancellationToken, callOptions.WriteOptions, callOptions.PropagationToken, callOptions.Credentials);

        }

        /// <summary>
        /// Adds the specified header. Nothing is done if it has already been set.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The haeder key representing a token string.</param>
        /// <param name="value">The binary value.</param>
        /// <returns>A new callOptions.</returns>
        public static CallOptions AddHeader(this CallOptions callOptions, string headerKey, byte[] value)
        {

            if (ContainsHeader(callOptions, headerKey)) { return callOptions; }

            Metadata headers = callOptions.Headers ?? new Metadata();

            headers.Add(headerKey, value ?? new byte[] { });

            return new CallOptions(headers, callOptions.Deadline, callOptions.CancellationToken, callOptions.WriteOptions, callOptions.PropagationToken, callOptions.Credentials);

        }

        #endregion

        #region SetHeader

        /// <summary>
        /// Sets the specified header.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The haeder key representing a token string.</param>
        /// <param name="value">The string value.</param>
        /// <returns>A new callOptions.</returns>
        public static CallOptions SetHeader(this CallOptions callOptions, string headerKey, string value)
        {

            Metadata headers = callOptions.Headers ?? new Metadata();

            headers.SetStringValue(headerKey, value);

            return new CallOptions(headers, callOptions.Deadline, callOptions.CancellationToken, callOptions.WriteOptions, callOptions.PropagationToken, callOptions.Credentials);

        }

        /// <summary>
        /// Sets the specified header.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The haeder key representing a token string.</param>
        /// <param name="value">The binary value.</param>
        /// <returns>A new callOptions.</returns>
        public static CallOptions SetHeader(this CallOptions callOptions, string headerKey, byte[] value)
        {

            Metadata headers = callOptions.Headers ?? new Metadata();

            headers.SetBinaryValue(headerKey, value);

            return new CallOptions(headers, callOptions.Deadline, callOptions.CancellationToken, callOptions.WriteOptions, callOptions.PropagationToken, callOptions.Credentials);

        }

        #endregion

        #region ContainsHeader

        /// <summary>
        /// Gets a value indicating whether the specified key is included. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The haeder key.</param>
        /// <returns>true if the key is contained; otherwise false.</returns>
        public static bool ContainsHeader(this CallOptions callOptions, string headerKey)
        {
            if (callOptions.Headers == null) { return false; }
            return callOptions.Headers.ContainsKey(headerKey);
        }

        #endregion

        #region GetHeader

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The key.</param>
        /// <returns>The value. If the key is not found, returns null.</returns>
        public static string GetHeaderStringValue(this CallOptions callOptions, string headerKey)
        {
            if (callOptions.Headers == null) { return null; }
            return callOptions.Headers.GetStringValueOrNull(headerKey);
        }

        /// <summary>
        /// Gets the value associated with the specified key. This method uses the brute force algorithm.
        /// </summary>
        /// <param name="callOptions"></param>
        /// <param name="headerKey">The key.</param>
        /// <returns>The value. If the key is not found, returns null.</returns>
        public static byte[] GetHeaderBinaryValue(this CallOptions callOptions, string headerKey)
        {
            if (callOptions.Headers == null) { return null; }
            return callOptions.Headers.GetBinaryValueOrNull(headerKey);
        }

        #endregion

    }

}
