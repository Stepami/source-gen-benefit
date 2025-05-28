# Что нам стоит Source Generator встроить

https://github.com/amis92/csharp-source-generators

## MediatR

Замена - https://github.com/martinothamar/Mediator

![](https://github.com/martinothamar/Mediator/blob/main/img/benchmarks.png?raw=true)

## FluentValidation
Зависимость от рефлексии:
- https://github.com/FluentValidation/FluentValidation/issues/2307

Замена - https://github.com/Hookyns/validly

Docs - https://validly.gitbook.io/docs

Молодое решение:
- https://github.com/Hookyns/validly/pull/9
- https://github.com/Hookyns/validly/pull/10
- https://github.com/Hookyns/validly/pull/12

![](https://validly.gitbook.io/~gitbook/image?url=https%3A%2F%2F4231388055-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252Fsrj2ue2EzleR5DCq3FJb%252Fuploads%252FoLTBUAF9y1sHTwVudhlY%252Fimage.png%3Falt%3Dmedia%26token%3D7175cf89-0c82-4c20-80ab-448fad1e1a67&width=768&dpr=4&quality=100&sign=57989cd&sv=2)
![](https://validly.gitbook.io/~gitbook/image?url=https%3A%2F%2F4231388055-files.gitbook.io%2F%7E%2Ffiles%2Fv0%2Fb%2Fgitbook-x-prod.appspot.com%2Fo%2Fspaces%252Fsrj2ue2EzleR5DCq3FJb%252Fuploads%252FLfVAUVqPZGkQogWw3GfJ%252Fimage.png%3Falt%3Dmedia%26token%3D9c30a0aa-7b3f-4caa-ad38-58161ba38556&width=768&dpr=4&quality=100&sign=f0699630&sv=2)

## AutoMapper

Замена - https://github.com/riok/mapperly

https://mapperly.riok.app/docs/intro/#performance
![](https://habrastorage.org/webt/fw/id/bp/fwidbptyj6k0oqmtsfejpuahp_a.png)

## Refit

Замена - https://webapiclient.github.io/en/

![](https://habrastorage.org/webt/xk/xj/cv/xkxjcv69ayvhevmvkj2oikarzrs.png)

## System.Text.Json

Замена - https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/reflection-vs-source-generation

Нюанс - https://t.me/epeshkblog/213
Проблемы:
- https://github.com/dotnet/runtime/issues/78602
- https://github.com/dotnet/runtime/issues/55043
- https://github.com/dotnet/aspnetcore/issues/61867

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
| BeforeSourceGen | 1                 |   1,290.5 ns |   0.78 ns |   0.73 ns |  0.2079 |    1312 B |
| AfterSourceGen  | 1                 |     754.3 ns |   0.50 ns |   0.44 ns |  0.0153 |      96 B |
| BeforeSourceGen | 10                |  12,930.6 ns |   9.82 ns |   9.19 ns |  2.0905 |   13120 B |
| AfterSourceGen  | 10                |   7,538.4 ns |   4.21 ns |   3.51 ns |  0.1526 |     960 B |
| BeforeSourceGen | 100               | 128,966.2 ns | 141.99 ns | 125.87 ns | 20.7520 |  131200 B |
| AfterSourceGen  | 100               |  75,803.1 ns |  58.10 ns |  51.51 ns |  1.4648 |    9600 B |

## GetList in-proc
| Method          | QueryCount |        Mean |     Error |    StdDev |    Gen0 |   Gen1 | Allocated |
|-----------------|------------|------------:|----------:|----------:|--------:|-------:|----------:|
| BeforeSourceGen | 1          |    396.4 ns |   3.87 ns |   3.43 ns |  0.2103 |      - |    1320 B |
| AfterSourceGen  | 1          |    157.9 ns |   1.65 ns |   1.54 ns |  0.1032 |      - |     648 B |
| BeforeSourceGen | 10         |  3,900.8 ns |  25.16 ns |  23.54 ns |  2.0981 | 0.0076 |   13200 B |
| AfterSourceGen  | 10         |  1,546.3 ns |  20.53 ns |  19.20 ns |  1.0319 | 0.0019 |    6480 B |
| BeforeSourceGen | 100        | 39,000.4 ns | 209.98 ns | 175.35 ns | 20.9961 | 0.0610 |  132000 B |
| AfterSourceGen  | 100        | 15,125.4 ns |  19.29 ns |  16.11 ns | 10.3149 |      - |   64800 B |

## GetList API

| Method                  | QueryCount |      Mean |     Error |    StdDev |    Median |
|-------------------------|------------|----------:|----------:|----------:|----------:|
| BeforeSourceGen         | 10         |  3.128 ms | 0.0612 ms | 0.0916 ms |  3.073 ms |
| AfterSourceGen          | 10         |  2.045 ms | 0.0407 ms | 0.1188 ms |  1.982 ms |
| AfterSourceGenNativeAot | 10         |  3.626 ms | 0.0724 ms | 0.0889 ms |  3.567 ms |
| BeforeSourceGen         | 100        | 31.265 ms | 0.6193 ms | 0.7832 ms | 31.450 ms |
| AfterSourceGen          | 100        | 20.620 ms | 0.4116 ms | 1.1406 ms | 20.258 ms |
| AfterSourceGenNativeAot | 100        | 35.570 ms | 0.2380 ms | 0.1988 ms | 35.561 ms |

> Few things that cause the slightly lower performance in native AOT apps right now. First (in apps using the web SDK) is the new DATAS Server GC mode. This new GC mode uses far less memory than traditional ServerGC by dynamically adapting memory use based on the app's demands, but in this 1st generation it impacts the performance slightly. The goal is to remove the performance impact and enable DATAS for all Server GC apps in the future.
Second is CoreCLR in .NET 8 has Dynamic PGO enabled by default, which allows the JIT to recompile hot methods with more aggressive optimizations based on what it observes while the app is running. Native AOT has static PGO with a default profile applied and by definition can never have Dynamic PGO.
Thirdly, JIT can detect hardware capabilities (e.g. CPU intrinsics) at runtime and target those in the code it generates. Native AOT however defaults to a highly compatible target instruction set which won't have those optimizations but you can specify them at compile time based on the hardware you know you're going to run on.
Running the tests in video with DATAS disabled and native AOT configured for the target CPU could improve the results slightly.


## Memory dumps

Лежат в папке memory-snapshots, открываются через dotMemory: https://www.jetbrains.com/dotmemory/

![](https://habrastorage.org/webt/t8/9s/nu/t89snubhlfanftmhpxscpzp0kia.png)