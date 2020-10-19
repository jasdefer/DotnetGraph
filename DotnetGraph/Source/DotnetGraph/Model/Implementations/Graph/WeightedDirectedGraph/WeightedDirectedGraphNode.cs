using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph
{
    public class WeightedDirectedGraphNode : Node<WeightedDirectedGraphArc>,
        IHasOutgoingArcs<WeightedDirectedGraphArc>,
        IHasId
    {
        public WeightedDirectedGraphNode(int id)
        {
            Id = id;
        }

        public WeightedDirectedGraphNode(int id, IEnumerable<WeightedDirectedGraphArc> outgoingArcs) : base(outgoingArcs)
        {
            Id = id;
        }

        public int Id { get; }

        public override string ToString()
        {
            return $"Node {Id}";
        }
    }
}