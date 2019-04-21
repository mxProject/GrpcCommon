# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.ServerServiceDefinitionExtensions class.

## ServerServiceDefinition

### AddStructMethod

#### Unary

Adds a Unary method definition.
It is possible to specify a value type for request / response type.

```c#
static Method<int, long> method;
ServerServiceDefinition.Builder builder;
Service service = new Service();
builder = builder.AddStructMethod(method, service.Unary);

internal class Service
{
    public Task<long> Unary(int request, ServerCallContext context);
}
```

#### ServerStreaming

Adds a ServerStreaming method definition.
It is possible to specify a value type for request / response type.

```c#
static Method<int, long> method;
ServerServiceDefinition.Builder builder;
Service service = new Service();
builder = builder.AddStructMethod(method, service.ServerStreaming);

internal class Service
{
    public Task ServerStreaming(int request, IServerStreamWriter<long> responseWriter, ServerCallContext context);
}
```

#### ClientStreaming

Adds a ClientStreaming method definition.
It is possible to specify a value type for request / response type.

```c#
static Method<int, long> method;
ServerServiceDefinition.Builder builder;
Service service = new Service();
builder = builder.AddStructMethod(method, service.ClientStreaming);

internal class Service
{
    public Task<long> ClientStreaming(IAsyncStreamReader<int> requestReader, ServerCallContext context);
}
```

#### DuplexStreaming

Adds a DuplexStreaming method definition.
It is possible to specify a value type for request / response type.

```c#
static Method<int, long> method;
ServerServiceDefinition.Builder builder;
Service service = new Service();
builder = builder.AddStructMethod(method, service.DuplexStreaming);

internal class Service
{
    public Task DuplexStreaming(IAsyncStreamReader<int> requestReader, IServerStreamWriter<long> responseWriter, ServerCallContext context);
}
```

## delegates

Server handlers without type restrictions.

```c#
public delegate Task<TResponse> StructUnaryServerMethod<TRequest, TResponse>(
    TRequest request
    , ServerCallContext context
);

public delegate Task<TResponse> StructClientStreamingServerMethod<TRequest, TResponse>(
    IAsyncStreamReader<TRequest> requestStream
    , ServerCallContext context
);

public delegate Task StructServerStreamingServerMethod<TRequest, TResponse>(
    TRequest request, IServerStreamWriter<TResponse> responseStream
    , ServerCallContext context
);

public delegate Task StructDuplexStreamingServerMethod<TRequest, TResponse>(
    IAsyncStreamReader<TRequest> requestStream
    , IServerStreamWriter<TResponse> responseStream
    , ServerCallContext context
);
```

