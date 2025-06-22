using ByteForge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ByteForgeTest;

[TestClass]
public class TimingBenchmarkTest
{
    [TestMethod]
    public void BenchmarkInt16()
    {
        // Characterize the performance of Endian.SwapInt16
        const int iterations = 1000000; 
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            _ = Endian.SwapInt16((short)i);
        }
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"SwapInt16 took {elapsed} ms for {iterations} iterations.");
    }

    [TestMethod]
    public void BenchmarkInt128()
    {
        // Characterize the performance of Endian.SwapInt16
        const int iterations = 1000000;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (Int128 i = 0; i < iterations; i++)
        {
            _ = Endian.SwapInt128(i);
        }
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"SwapInt16 took {elapsed} ms for {iterations} iterations.");
    }

    [TestMethod]
    public void BenchmarkUInt128()
    {
        // Characterize the performance of Endian.SwapInt16
        const int iterations = 1000000;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (UInt128 i = 0; i < iterations; i++)
        {
            _ = Endian.SwapUInt128(i);
        }
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"SwapInt16 took {elapsed} ms for {iterations} iterations.");
    }

    [TestMethod]
    public void BenchmarkInt64()
    {
        // Characterize the performance of Endian.SwapInt16
        const int iterations = 1000000;
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (Int64 i = 0; i < iterations; i++)
        {
            _ = Endian.SwapInt64(i);
        }
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"SwapInt16 took {elapsed} ms for {iterations} iterations.");
    }
}
