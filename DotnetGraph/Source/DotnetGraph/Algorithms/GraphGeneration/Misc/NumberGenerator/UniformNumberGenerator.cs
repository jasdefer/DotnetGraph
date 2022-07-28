namespace DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;

public class UniformNumberGenerator : INumberGenerator
{
    public UniformNumberGenerator()
    {

    }

    public UniformNumberGenerator(double min = 0,
        double max = 100,
        int decimals = 0,
        int seed = 1)
    {
        Min = min;
        Max = max;
        Decimals = decimals;
        Random = new Random(seed);
    }
    public Random Random { get; set; } = new Random(1);
    public double Min { get; set; }
    public double Max { get; set; } = 100;
    public int Decimals { get; set; }

    public double Generate()
    {
        var weight = Random.NextDouble() * (Max - Min) + Min;
        weight = Math.Round(weight, Decimals);
        return weight;
    }
}
