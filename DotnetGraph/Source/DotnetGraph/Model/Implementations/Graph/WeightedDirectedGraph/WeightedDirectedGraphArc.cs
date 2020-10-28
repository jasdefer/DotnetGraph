using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph
{
    public class WeightedDirectedGraphArc : WeightedArc<WeightedDirectedGraphNode>,
        IHasWeight,
        IHasDestination<WeightedDirectedGraphNode>,
        IHasId
    {
        public WeightedDirectedGraphArc(int id,
            WeightedDirectedGraphNode destination,
            double weight) : base(destination, weight)
        {
            Id = id;
        }

        public int Id { get; }

        public override string ToString()
        {
            return $"Arc {Id}: to {Destination.Id} ({Weight})";
        }
    }
}