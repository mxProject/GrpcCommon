using System;
using System.Collections.Generic;
using System.Text;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Extention methods of <see cref="RpcException"/>.
    /// </summary>
    public static class RpcExceptionExtension
    {

        /// <summary>
        /// Creates and returns a string representation of the current exception for debugging.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string ToDebugString(this RpcException exception)
        {
            StringBuilder sb = new StringBuilder(256);

            sb.AppendLine($"StatusCode = {exception.Status.StatusCode}");
            sb.AppendLine($"Detail = {exception.Status.Detail}");
            sb.AppendLine($"Message = {exception.Message}");

            if (exception.Trailers != null && exception.Trailers.Count > 0)
            {
                foreach (var trailer in exception.Trailers)
                {
                    sb.Append($"Trailers[{trailer.Key}] = ");
                    if (trailer.IsBinary)
                    {
                        if (trailer.ValueBytes == null)
                        {
                            sb.Append("null");
                        }
                        else
                        {
                            sb.Append($"bytes[{trailer.ValueBytes.Length}]");
                        }
                    }
                    else
                    {
                        if (trailer.ValueBytes == null)
                        {
                            sb.Append("null");
                        }
                        else
                        {
                            sb.Append($"'{trailer.Value}'");
                        }
                    }
                    sb.AppendLine("");
                }
            }

            return sb.ToString();
        }

    }

}
