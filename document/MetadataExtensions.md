# Extension methods

The following methods are implemented on mxProject.Helpers.GrpcCore.Extensions.MetadataExtensions class.

## Metadata

### AddStringValue

Sets the specified key and value. Nothing is done if it has already been set.

```c#
Metadata metadata = new Metadata();
metadata.AddStringValue("user", "user1");
metadata.AddStringValue("user", "user2");
Console.WriteLine(metadata.GetStringValueOrNull("user"));

--- console output ---
user1
```

### AddBinaryValue

Sets the specified key and value. Nothing is done if it has already been set.

```c#
Metadata metadata = new Metadata();
metadata.AddBinaryValue("user-bin", new byte[]{1,2});
metadata.AddBinaryValue("user-bin", new byte[]{1,2,3});
Console.WriteLine(metadata.GetBinaryValueOrNull("user-bin").Length);

--- console output ---
2
```

### SetStringValue

Sets the specified key and value.

```c#
Metadata metadata = new Metadata();
metadata.SetStringValue("user", "user1");
metadata.SetStringValue("user", "user2");
Console.WriteLine(metadata.GetStringValueOrNull("user"));

--- console output ---
user2
```

### SetBinaryValue

Sets the specified key and value.

```c#
Metadata metadata = new Metadata();
metadata.SetBinaryValue("user-bin", new byte[]{1,2});
metadata.SetBinaryValue("user-bin", new byte[]{1,2,3});
Console.WriteLine(metadata.GetBinaryValueOrNull("user-bin").Length);

--- console output ---
3
```

### ContainsKey

Gets a value indicating whether the specified key is included.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.ContainsKey("user"));
metadata.SetStringValue("user", "user1");
Console.WriteLine(metadata.ContainsKey("user"));

--- console output ---
false
true
```

### IndexOf

Gets the index associated with the specified key.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.IndexOf("user"));
metadata.SetStringValue("user", "user1");
Console.WriteLine(metadata.IndexOf("user"));

--- console output ---
-1
0
```

### TryGetStringValue

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
metadata.SetStringValue("user", "user1");
if (metadata.TryGetStringValue("user", out string value))
{
    Console.WriteLine(value);
}
--- console output ---
user1
```

### TryGetBinaryValue

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
metadata.SetBinaryValue("user-bin", new byte[]{1,2,3});
if (metadata.TryGetBinaryValue("user-bin", out byte[] value))
{
    Console.WriteLine(value.Length);
}
--- console output ---
3
```

### GetStringValueOrNull

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.GetStringValueOrNull("user"))
--- console output ---

```

### GetBinaryValueOrNull

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.GetBinaryValueOrNull("user-bin"))
--- console output ---

```

### GetStringValueOrDefault

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.GetStringValueOrDefault("user", "NULL"))
--- console output ---
NULL
```

### GetBinaryValueOrDefault

Gets the value associated with the specified key.

```c#
Metadata metadata = new Metadata();
Console.WriteLine(metadata.GetBinaryValueOrDefault("user-bin", new byte[]{}))
--- console output ---
byte[]
```

### ToStringDictionary

Creates a dictionary containing string values.

```c#
Metadata metadata = new Metadata();
metadata.SetStringValue("user", "user1");
metadata.SetStringValue("token", "12345");
IDictionary<string, string> dic = metadata.ToStringDictionary();
```

### ToBinaryDictionary

Creates a dictionary containing bytes values.

```c#
Metadata metadata = new Metadata();
metadata.SetBinaryValue("user-bin", new byte[]{1,2,3});
metadata.SetBinaryValue("token-bin", new byte[]{1,2,3,4,5});
IDictionary<string, byte[]> dic = metadata.ToBinaryDictionary();
```
