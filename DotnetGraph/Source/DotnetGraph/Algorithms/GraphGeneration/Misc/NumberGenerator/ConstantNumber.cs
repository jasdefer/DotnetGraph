namespace DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator
{
    public class ConstantNumber : INumberGenerator
    {
        public double Number { get; set; } = 1;
        public double Generate()
        {
            return Number;
        }
    }
}