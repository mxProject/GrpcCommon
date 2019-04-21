# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.AsyncClientStreamingCallExtensions class.

## AsyncClientStreamingCall&lt;TRequest, TResponse&gt;

### WriteRequestsAsync

Writes the specified requests.

```c#
IEnumerable<TRequest> requests1;
IEnumerable<TRequest> requests2;
using (var call = client.ExecuteClientStreaming())
{
    await call.WriteRequestsAsync(requests1);
    await call.WriteRequestsAsync(requests2);
    call.RequestStream.CompleteAsync();
    TResponse response = await call.ResponseAsync;
}
```

### WriteRequestsAndCompleteAsync

Writes the specified requests and sends a completion.

```c#
IEnumerable<TRequest> requests;
using (var call = client.ExecuteClientStreaming())
{
    TResponse response = await call.WriteRequestsAndCompleteAsync(requests);
}
```

### ConvertRequest

Converts the request type.

```c#
IEnumerable<TSource> requestSources;
Func<TSource, TRequest> requestConverter;
// convert to AsyncClientStreamingCall<TSource, TResponse>.
using (var call = client.ExecuteClientStreaming().ConvertRequest(requestConverter))
{
    TResponse response = await call.WriteRequestsAndCompleteAsync(requestSources);
}
```

### ConvertResponse

Converts the response type.

```c#
IEnumerable<TRequest> requests;
Func<TResponse, TResult> responseConverter;
// convert to AsyncClientStreamingCall<TRequest, TResult>.
using (var call = client.ExecuteClientStreaming().ConvertResponse(responseConverter))
{
    TResult result = await call.WriteRequestsAndCompleteAsync(requests);
}
```

### ConvertRequestResponse

Converts the request/response type.

```c#
IEnumerable<TSource> requestSources;
Func<TSource, TRequest> requestConverter;
Func<TResponse, TResult> responseConverter;
// convert to AsyncClientStreamingCall<TSource, TResult>.
using (var call = client.ExecuteClientStreaming().ConvertRequestResponse(requestConverter, responseConverter))
{
    TResult result = await call.WriteRequestsAndCompleteAsync(requestSources);
}
```
