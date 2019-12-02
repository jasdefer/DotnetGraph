using DotnetGraph.Algorithms.Contracts.GraphGeneration;
using System;

namespace DotnetGraph.Algorithms.GraphGeneration
{
    public class UniformWeightGenerator : IWeightGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public double Min { get; set; } = 1;
        public double Max { get; set; } = 100;

        public int Digits { get; set; } = 0;

        public double Create()
        {
            var weight = Random.NextDouble() * (Max - Min) + Min;
            weight = Math.Round(weight, Digits);
            return weight;
        }
    }
}