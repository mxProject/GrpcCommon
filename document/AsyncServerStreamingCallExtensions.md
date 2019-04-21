# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.AsyncServerStreamingCallExtenstions class.

## AsyncServerStreamingCall&lt;TResponse&gt;

### ReadAllAsync

Reads all responses.

```c#
TRequest request;
using (var call = client.ExecuteServerStreaming(request))
{
    IList<TResponse> responses = await call.ReadAllAsync();
}
```

### FillAllAsync

Puts all responses in the collection.

```c#
TRequest request;
List<TResponse> responses = new List<TResponse>();
using (var call = client.ExecuteServerStreaming(request))
{
    await call.FillAllAsync(responses);
}
```

### ForEachAsync

Executes the specified method on the readed responses.

```c#
TRequest request;
Action<TResponse> action;
using (var call = client.ExecuteServerStreaming(request))
{
    await call.ForEachAsync(action);
}
```

```c#
TRequest request;
Func<TResponse, Task> asyncAction;
using (var call = client.ExecuteServerStreaming(request))
{
    await call.ForEachAsync(asyncAction);
}
```

### ConvertResponse

Converts the response type.

```c#
TRequest request;
Func<TResponse, TResult> responseConverter;
// convert to AsyncServerStreamingCall<TResult>.
using (var call = client.ExecuteServerStreaming(request).ConvertResponse(responseConverter))
{
    IList<TResult> results = await call.ReadAllAsync();
}
```
