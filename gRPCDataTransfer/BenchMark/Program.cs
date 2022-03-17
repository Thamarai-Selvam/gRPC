using System;
using System.Diagnostics;

public class Benchmark : IDisposable
{
    private readonly Stopwatch timer = null;
    private readonly string benchmarkName;

    public Benchmark(string benchmarkName)
    {
        this.timer = new Stopwatch();
        this.benchmarkName = benchmarkName;
        timer.Start();
    }

    public void Dispose()
    {
        this.timer.Stop();
        Console.WriteLine($"{benchmarkName} {timer.Elapsed}");
    }
}