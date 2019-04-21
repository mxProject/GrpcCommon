# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.CallInvokerExtensions class.

## CallInvoker

### StructBlockingUnaryCall

Invokes BlockingUnaryCall without type restrictions.
It is possible to specify a value type for request / response type.

```c#
public class Client
{
    private CallInvoker CallInvoker;

    private static Method<int, long> method;
    public long BlockingUnaryCall(int request, CallOptions options)
    {
        return CallInvoker.StructBlockingUnaryCall(method, "", options, request);
    }
}
```

### StructAsyncUnaryCall

Invokes AsyncUnaryCall without type restrictions.
It is possible to specify a value type for request / response type.

```c#
public class Client
{
    private CallInvoker CallInvoker;

    private static Method<int, long> method;
    public AsyncUnaryCall<long> AsyncUnaryCall(int request, CallOptions options)
    {
        return CallInvoker.StructAsyncUnaryCall(method, "", options, request);
    }
}
```

### StructAsyncServerStreamingCall

Invokes AsyncServerStreamingCall without type restrictions.
It is possible to specify a value type for request / response type.

```c#
public class Client
{
    private CallInvoker CallInvoker;

    private static Method<int, long> method;
    public AsyncServerStreamingCall<long> AsyncServerStreamingCall(int request, CallOptions options)
    {
        return CallInvoker.StructAsyncServerStreamingCall(method, "", options, request);
    }
}
```

### StructAsyncClientStreamingCall

Invokes AsyncClientStreamingCall without type restrictions.
It is possible to specify a value type for request / response type.

```c#
public class Client
{
    private CallInvoker CallInvoker;

    private static Method<int, long> method;
    public AsyncClientStreamingCall<int, long> AsyncClientStreamingCall(CallOptions options)
    {
        return CallInvoker.StructAsyncClientStreamingCall(method, "", options);
    }
}
```

### StructAsyncDuplexStreamingCall

Invokes AsyncDuplexStreamingCall without type restrictions.
It is possible to specify a value type for request / response type.

```c#
public class Client
{
    private CallInvoker CallInvoker;

    private static Method<int, long> method;
    public AsyncDuplexStreamingCall<int, long> AsyncDuplexStreamingCall(CallOptions options)
    {
        return CallInvoker.StructAsyncDuplexStreamingCall(method, "", options);
    }
}
```
