using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Grpc.Core;

namespace mxProject.Helpers.GrpcCore.Extensions
{

    /// <summary>
    /// Server-side handler for unary call without type restrictions.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task<TResponse> StructUnaryServerMethod<TRequest, TResponse>(TRequest request, ServerCallContext context);

    /// <summary>
    /// Server-side handler for client streaming call without type restrictions.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="requestStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task<TResponse> StructClientStreamingServerMethod<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, ServerCallContext context);

    /// <summary>
    /// Server-side handler for server streaming call without type restrictions.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="request"></param>
    /// <param name="responseStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task StructServerStreamingServerMethod<TRequest, TResponse>(TRequest request, IServerStreamWriter<TResponse> responseStream, ServerCallContext context);

    /// <summary>
    /// Server-side handler for duplex streaming call without type restrictions.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    /// <param name="requestStream"></param>
    /// <param name="responseStream"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public delegate Task StructDuplexStreamingServerMethod<TRequest, TResponse>(IAsyncStreamReader<TRequest> requestStream, IServerStreamWriter<TResponse> responseStream, ServerCallContext context);

}
