using System;

namespace DotnetGraph.Model.Graphs.WeightedDirectedGraph
{
    public class WeightedDirectedGraphArc : IWeightedDirectedGraphArc
    {
        public WeightedDirectedGraphArc(IWeightedDirectedGraphNode destination, double weight)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Weight = weight;
        }

        public IWeightedDirectedGraphNode Destination { get; }
        public double Weight { get; }
    }
}