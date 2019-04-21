# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.CallOptionsExtensions class.

## CallOptions

### AddHeader

Adds the specified header. Nothing is done if it has already been set.

```c#
CallOptions options = new CallOptions();
options = options.AddHeader("user", "user1");
options = options.AddHeader("user", "user2");
Console.WriteLine(options.GetHeaderStringValue("user"));

--- console output ---
user1
```

### SetHeader

Sets the specified header.

```c#
CallOptions options = new CallOptions();
options = options.SetHeader("user", "user1");
options = options.SetHeader("user", "user2");
Console.WriteLine(options.GetHeaderStringValue("user"));

--- console output ---
user2
```

### ContainsHeader

Gets a value indicating whether the specified key is included.

```c#
CallOptions options = new CallOptions();
options = options.SetHeader("user", "user1");
Console.WriteLine(options.ContainsHeader("user"));

--- console output ---
true
```

### GetHeader

Gets the value associated with the specified key.

```c#
CallOptions options = new CallOptions();
options = options.SetHeader("user", "user1");
options = options.SetHeader("token-bin", new byte[]{1,2,3});
Console.WriteLine(options.GetHeaderStringValue("user"));
Console.WriteLine(options.GetHeaderBinaryValue("token-bin").Length);

--- console output ---
user1
3
```
