# Что нам стоит Source Generator встроить

https://github.com/amis92/csharp-source-generators

## MediatR

Замена - https://github.com/martinothamar/Mediator

![](https://github.com/martinothamar/Mediator/blob/main/img/benchmarks.png?raw=true)

## FluentValidation

Замена - https://github.com/Hookyns/validly

Docs - https://validly.gitbook.io/docs

![](https://validly.gitbook.io/~gitbook/image?url=https%3A%2F%2F4231388055-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252Fsrj2ue2EzleR5DCq3FJb%252Fuploads%252FoLTBUAF9y1sHTwVudhlY%252Fimage.png%3Falt%3Dmedia%26token%3D7175cf89-0c82-4c20-80ab-448fad1e1a67&width=768&dpr=4&quality=100&sign=57989cd&sv=2)
![](https://validly.gitbook.io/~gitbook/image?url=https%3A%2F%2F4231388055-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252Fsrj2ue2EzleR5DCq3FJb%252Fuploads%252FLfVAUVqPZGkQogWw3GfJ%252Fimage.png%3Falt%3Dmedia%26token%3D9c30a0aa-7b3f-4caa-ad38-58161ba38556&width=768&dpr=4&quality=100&sign=f0699630&sv=2)

## AutoMapper

Замена - https://github.com/riok/mapperly

https://mapperly.riok.app/docs/intro/#performance
![](https://habrastorage.org/webt/fw/id/bp/fwidbptyj6k0oqmtsfejpuahp_a.png)

## System.Text.Json

Замена - https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/reflection-vs-source-generation

Нюанс - https://t.me/epeshkblog/213

https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/

![](https://habrastorage.org/webt/bh/r0/jr/bhr0jrclhujcft2k8t19lmvqd1i.png)

## ILogger

Замена - https://learn.microsoft.com/en-us/dotnet/core/extensions/logger-message-generator

https://andrewlock.net/exploring-dotnet-6-part-8-improving-logging-performance-with-source-generators/

https://github.com/stbychkov/AutoLoggerMessage/wiki/Benchmarks

![](https://habrastorage.org/webt/xq/hn/p1/xqhnp1mwgks7kucfcfzi58dwfxs.png)

## IConfiguration

Замена - https://learn.microsoft.com/en-us/dotnet/core/extensions/configuration-generator

https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/runtime#configuration-binding-source-generator

## Dapper, EF, linq2db GET

Замена - https://github.com/jitbit/MapDataReader

![](https://habrastorage.org/webt/9d/8j/vi/9d8jvidms1buyqnjvgrz0jr51ha.png)

# Benchmarks

## Create in-proc

| Method          | TestEntitiesCount |         Mean |     Error |    StdDev |    Gen0 | Allocated |
|-----------------|-------------------|-------------:|----------:|----------:|--------:|----------:|
| BeforeSourceGen | 1                 |   1,367.0 ns |  10.46 ns |   9.28 ns |  0.2098 |    1320 B |
| AfterSourceGen  | 1                 |     755.7 ns |   0.75 ns |   0.71 ns |  0.0162 |     104 B |
| BeforeSourceGen | 10                |  13,259.7 ns |  13.67 ns |  12.12 ns |  2.0905 |   13200 B |
| AfterSourceGen  | 10                |   7,680.5 ns |  94.92 ns |  88.79 ns |  0.1602 |    1040 B |
| BeforeSourceGen | 100               | 130,288.1 ns | 172.14 ns | 152.60 ns | 20.9961 |  132001 B |
| AfterSourceGen  | 100               |  76,087.7 ns |  46.56 ns |  43.55 ns |  1.5869 |   10400 B |

## GetList in-proc
| Method          | QueryCount |        Mean |     Error |    StdDev |    Gen0 |   Gen1 | Allocated |
|-----------------|------------|------------:|----------:|----------:|--------:|-------:|----------:|
| BeforeSourceGen | 1          |    398.7 ns |   6.58 ns |   5.84 ns |  0.2103 |      - |    1320 B |
| AfterSourceGen  | 1          |    192.5 ns |   1.47 ns |   1.38 ns |  0.1032 |      - |     648 B |
| BeforeSourceGen | 10         |  3,911.5 ns |  25.34 ns |  23.70 ns |  2.0981 | 0.0076 |   13200 B |
| AfterSourceGen  | 10         |  1,548.8 ns |  15.51 ns |  12.95 ns |  1.0319 | 0.0019 |    6480 B |
| BeforeSourceGen | 100        | 38,554.5 ns | 181.80 ns | 161.16 ns | 20.9961 | 0.0610 |  132000 B |
| AfterSourceGen  | 100        | 15,241.3 ns |  88.52 ns |  82.80 ns | 10.3149 |      - |   64800 B |

## Create API

| Method          | TestEntitiesCount |       Mean |       Error |      StdDev |
|-----------------|-------------------|-----------:|------------:|------------:|
| AfterSourceGen  | 10                | 4,392.8 us |   196.15 us |   575.28 us |
| BeforeSourceGen | 10                | 8,738.9 us | 1,383.93 us | 3,925.98 us |


## GetList API

| Method          | QueryCount |      Mean |     Error |     StdDev |    Median |        P95 |
|-----------------|------------|----------:|----------:|-----------:|----------:|-----------:|
| AfterSourceGen  | 10         |  2.316 ms | 0.0461 ms |  0.0615 ms |  2.314 ms |   2.389 ms |
| BeforeSourceGen | 10         |  3.831 ms | 0.1039 ms |  0.2980 ms |  3.826 ms |   4.431 ms |
| AfterSourceGen  | 100        | 27.095 ms | 2.5310 ms |  6.8857 ms | 24.303 ms |  42.186 ms |
| BeforeSourceGen | 100        | 51.849 ms | 7.8106 ms | 20.4389 ms | 46.462 ms | 120.946 ms |

> Few things that cause the slightly lower performance in native AOT apps right now. First (in apps using the web SDK) is the new DATAS Server GC mode. This new GC mode uses far less memory than traditional ServerGC by dynamically adapting memory use based on the app's demands, but in this 1st generation it impacts the performance slightly. The goal is to remove the performance impact and enable DATAS for all Server GC apps in the future.
Second is CoreCLR in .NET 8 has Dynamic PGO enabled by default, which allows the JIT to recompile hot methods with more aggressive optimizations based on what it observes while the app is running. Native AOT has static PGO with a default profile applied and by definition can never have Dynamic PGO.
Thirdly, JIT can detect hardware capabilities (e.g. CPU intrinsics) at runtime and target those in the code it generates. Native AOT however defaults to a highly compatible target instruction set which won't have those optimizations but you can specify them at compile time based on the hardware you know you're going to run on.
Running the tests in video with DATAS disabled and native AOT configured for the target CPU could improve the results slightly.
