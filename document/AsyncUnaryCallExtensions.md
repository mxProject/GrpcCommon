# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.AsyncUnaryCallExtensions class.

## AsyncUnaryCall&lt;TResponse&gt;

### ConvertResponse

Converts the response type.

```c#
TRequest request;
Func<TResponse, TResult> responseConverter;
// convert to AsyncUnaryCall<TResult>.
using (var call = client.ExecuteUnaryAsync(request).ConvertResponse(responseConverter))
{
    TResult result = await call.ResponseAsync;
}
```
