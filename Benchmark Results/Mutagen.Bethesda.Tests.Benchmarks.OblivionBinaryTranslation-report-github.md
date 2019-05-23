``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.765 (1803/April2018Update/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3984652 Hz, Resolution=250.9629 ns, Timer=TSC
.NET Core SDK=2.1.700-preview-009618
  [Host]     : .NET Core 2.1.11 (CoreCLR 4.6.27617.04, CoreFX 4.6.27617.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.11 (CoreCLR 4.6.27617.04, CoreFX 4.6.27617.02), 64bit RyuJIT


```
|       Method |     Mean |    Error |   StdDev |        Gen 0 |       Gen 1 |     Gen 2 | Allocated |
|------------- |---------:|---------:|---------:|-------------:|------------:|----------:|----------:|
| CreateBinary | 16.502 s | 0.3196 s | 0.3282 s | 1634000.0000 | 398000.0000 | 1000.0000 | 264.92 MB |
|  WriteBinary |  3.724 s | 0.0202 s | 0.0179 s |  105000.0000 |  33000.0000 |         - | 623.18 MB |