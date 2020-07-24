# OpaqueId

Yet another opaque id generation that can be used as trace id or reference code for your application. OpaqueId generation ensures no duplication of generated value and is thread-safe. The encoded opaque id is based on the current time since epoch or the `.ToUnixTimeMilliseconds()`.

To start off is rather easy - instantiate new `OpaqueIdGenerator` and call either the `GetOpaqueId` or `GetOpaqueIdWithTimestamp`.

* #### Default Base 36 encoding
```csharp
OpaqueIdGenerator generator = new OpaqueIdGenerator();

// Get only the opaque id generated:
string opaqueId = generator.GetOpaqueId();

// Get the opaque id together with the timestamp 
(DateTimeOffset Timestamp, string OpaqueId) opaqueIdWithTimestamp = generator.GetOpaqueIdWithTimestamp();
```

* #### Choose from available base character set
```csharp
// Octal (0-7)
OpaqueIdGenerator generator = new OpaqueIdGenerator(CharacterSet.Octal);

// Hexadecimal (0-9, A-F)
OpaqueIdGenerator generator = new OpaqueIdGenerator(CharacterSet.Hexadecimal);

// Base 36 (0-9, A-Z)
OpaqueIdGenerator generator = new OpaqueIdGenerator(CharacterSet.Base36);

// Base 64 (A-Z, a-z, 0-9, comma, and underscore)
OpaqueIdGenerator generator = new OpaqueIdGenerator(CharacterSet.Base64);
```

* #### Or you want to customize and have your own base characters for encoding
```csharp
OpaqueIdGenerator generator = new OpaqueIdGenerator("ABC123");
```

## NuGet package for [OpaqueId](https://www.nuget.org/packages/OpaqueId/)
