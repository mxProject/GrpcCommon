# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.AsyncDuplexStreamingCallExtenstions class.

## AsyncDuplexStreamingCall&lt;TRequest, TResponse&gt;

### WriteAllAndReadAsync

Writes the specified requests and reads the responses.

```c#
IEnumerable<TRequest> requests;
using (var call = client.ExecuteDuplexStreaming())
{
    IList<TResponse> responses = await call.WriteAllAndReadAsync(requests);
}
```

### WriteAllAndFillAsync

Writes the specified requests and puts the responses in the collection.

```c#
IEnumerable<TRequest> requests;
List<TResponse> responses = new List<TResponse>();
using (var call = client.ExecuteDuplexStreaming())
{
    await call.WriteAllAndFillAsync(requests, responses);
}
```

### WriteAllAndForEachAsync

Writes the specified requests and executes the specified method on the readed responses.

```c#
IEnumerable<TRequest> requests;
Action<TResponse> onResponse;
using (var call = client.ExecuteDuplexStreaming())
{
    await call.WriteAllAndForEachAsync(requests, onResponse);
}
```
```c#
IEnumerable<TRequest> requests;
Func<TResponse, Task> onResponseAsync;
using (var call = client.ExecuteDuplexStreaming())
{
    await call.WriteAllAndForEachAsync(requests, onResponseAsync);
}
```

### ConvertRequest

Converts the request type.

```c#
IEnumerable<TSource> requestSources;
Func<TSource, TRequest> requestConverter;
// convert to AsyncDuplexStreamingCall<TSource, TResponse>.
using (var call = client.ExecuteDuplexStreaming().ConvertRequest(requestConverter))
{
    IList<TResponse> responses = await call.WriteAllAndReadAsync(requestSources);
}
```

### ConvertResponse

Converts the response type.

```c#
IEnumerable<TRequest> requests;
Func<TResponse, TResult> responseConverter;
// convert to AsyncDuplexStreamingCall<TRequest, TResult>.
using (var call = client.ExecuteDuplexStreaming().ConvertResponse(responseConverter))
{
    IList<TResult> results = await call.WriteAllAndReadAsync(requests);
}
```

### ConvertRequestResponse

Converts the request/response type.

```c#
IEnumerable<TSource> requestSources;
Func<TSource, TRequest> requestConverter;
Func<TResponse, TResult> responseConverter;
// convert to AsyncDuplexStreamingCall<TSource, TResult>.
using (var call = client.ExecuteDuplexStreaming().ConvertRequestResponse(requestConverter, responseConverter))
{
    IList<TResult> results = await call.WriteAllAndReadAsync(requestSources);
}
```
