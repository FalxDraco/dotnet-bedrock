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
bool IsEqual(this byte[], byte[]);
```

```csharp
bool IsEqual(this uint[], uint[]);
```

```csharp
bool IsEmpty(this byte[]);
```

```csharp
bool IsEmpty(this uint[]);
```

- Span extensions:

```csharp
bool IsEqual(this Span<byte>, Span<byte>);
```

```csharp
void Split(this Span<uint>, Span<byte>);
```

```csharp
void Merge(this Span<byte>, Span<uint>);
```

```csharp
void XOR(this Span<byte>, Span<byte>);
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
byte XOR(this byte, byte);
```

- Integers scaling to arbitrary ranges:

```csharp
ulong ScaleUnsigned64(ulong, ulong, ulong, ulong, ulong);
```

```csharp
long ScaleSigned64(long, long, long, long, long);
```

```csharp
BigInteger ScaleBigInt(BigInteger, BigInteger, BigInteger, BigInteger, BigInteger);
```

```csharp
#if NET7_0_OR_GREATER
UInt128 ScaleUnsigned128(UInt128, UInt128, UInt128, UInt128, UInt128);
#endif
```

```csharp
#if NET7_0_OR_GREATER
Int128 ScaleSigned128(Int128, Int128, Int128, Int128, Int128);
#endif
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

