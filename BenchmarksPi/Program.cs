using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance
    .AddExporter(MarkdownExporter.GitHub)
    .AddExporter(HtmlExporter.Default)
    .AddExporter(CsvExporter.Default);

BenchmarkRunner.Run<PiBenchmarks>(config);

[MemoryDiagnoser]
public class PiBenchmarks
{
    private const int Iterations = 100_000;
    
    [Benchmark]
    public double Pi_Leibniz()
    {
        double sum = 0;
        for (var i = 0; i < Iterations; i++)
        {
            var term = ((i & 1) == 0 ? 1.0 : -1.0) / (2 * i + 1);
            sum += term;
        }
        return sum * 4;
    }
    
    [Benchmark]
    public double Pi_MonteCarlo()
    {
        var rng = new Random(42);
        var inside = 0;

        for (var i = 0; i < Iterations; i++)
        {
            var x = rng.NextDouble();
            var y = rng.NextDouble();

            if (x * x + y * y <= 1.0)
                inside++;
        }

        return 4.0 * inside / Iterations;
    }
}