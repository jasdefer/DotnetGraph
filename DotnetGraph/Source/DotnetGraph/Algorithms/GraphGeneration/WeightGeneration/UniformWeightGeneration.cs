using System;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightGeneration
{
    public class UniformWeightGeneration : IWeightGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public double MinWeight { get; set; }
        public double MaxWeight { get; set; }

        public double GetWeight()
        {
            var weight = (Random.NextDouble() * (MaxWeight + MinWeight)) - MinWeight;
            return weight;
        }
    }
}