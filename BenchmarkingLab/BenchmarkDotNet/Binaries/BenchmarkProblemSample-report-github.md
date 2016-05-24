```ini

BenchmarkDotNet=v0.9.4.0
OS=Microsoft Windows NT 6.1.7601 Service Pack 1
Processor=Intel(R) Core(TM) i7-4600M CPU @ 2.90GHz, ProcessorCount=4
Frequency=2825546 ticks, Resolution=353.9139 ns, Timer=TSC
HostCLR=MS.NET 4.0.30319.42000, Arch=64-bit RELEASE [RyuJIT]
JitModules=clrjit-v4.6.1055.0

Type=BenchmarkProblemSample  Mode=Throughput  

```
         Method |      Median |    StdDev |
--------------- |------------ |---------- |
 ContainsSample | 103.4612 ns | 9.7814 ns |
  IndexOfSample | 102.5153 ns | 4.7816 ns |
