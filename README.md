### GoeaLabs.Bedrock

![GitHub](https://img.shields.io/github/license/GoeaLabs/dotnet-bedrock?style=for-the-badge)
![GitHub release (latest SemVer)](https://img.shields.io/github/v/release/GoeaLabs/dotnet-bedrock?include_prereleases&style=for-the-badge)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/GoeaLabs.Bedrock?style=for-the-badge)

# Project Description
A collection of general purpose helper methods used by my other libraries.

# Compatibility
Compatible with .NET6 and higher, includes .NET7 build.

# API(s)

- Array extensions:

```csharp
byte[] FillRandom(this byte[]);
```

```csharp
uint[] FillRandom(this uint[]);
```

```csharp
ulong[] FillRandom(this ulong[]);
```

- Span extensions:

```csharp
void Split(this Span<uint>, Span<byte>);
```

```csharp
void Merge(this Span<byte>, Span<uint>);
```

```csharp
void Merge(this Span<byte>, Span<ulong>);
```

```csharp
void Merge(this Span<uint>, Span<ulong>);
```

```csharp
#if NET7_0_OR_GREATER
void Merge(this Span<uint>, Span<UInt128>);
#endif
```

```csharp
void Xor(this Span<byte>, Span<byte>);
```

```csharp
void FillRandom(this Span<byte>);
```

```csharp
void FillRandom(this Span<uint>);
```

```csharp
void FillRandom(this Span<ulong>);
```

Note that ```Split``` and ```Merge``` above are **endianness agnostic**: you will get the 
same results even on **IBM System/390**, the only Big Endian platform with .NET support
I am aware of.

- Integers extensions:

```csharp
void Halve(this ushort, out byte, out byte);
```

```csharp
void Halve(this uint, out ushort, out ushort);
```

```csharp
void Halve(this ulong, out uint, out uint);
```

```csharp
#if NET7_0_OR_GREATER
void Halve(this UInt128, out ulong, out ulong);
#endif
```

```csharp
ushort Merge(this byte, byte);
```

```csharp
uint Merge(this ushort, ushort);
```

```csharp
ulong Merge(this uint, uint);
```

```csharp
#if NET7_0_OR_GREATER
UInt128 Merge(this ulong, ulong);
#endif
```

```csharp
byte Xor(this byte, byte);
```

## Installation

Install with NuGet Package Manager Console
```
Install-Package GoeaLabs.Bedrock
```

Install with .NET CLI
```
dotnet add package GoeaLabs.Bedrock
```

