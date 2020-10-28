using System;

namespace DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator
{
    public class UniformWeightGenerator : IWeightGenerator
    {
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
}