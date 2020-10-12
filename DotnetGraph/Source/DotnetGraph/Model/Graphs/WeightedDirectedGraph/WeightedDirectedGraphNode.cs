using System.Collections.Generic;

namespace DotnetGraph.Model.Graphs.WeightedDirectedGraph
{
    public class WeightedDirectedGraphNode : IWeightedDirectedGraphNode
    {
        public WeightedDirectedGraphNode()
        {
            OutgoingArcs = new List<IWeightedDirectedGraphArc>();
        }

        public WeightedDirectedGraphNode(IList<IWeightedDirectedGraphArc> outgoingArcs)
        {
            OutgoingArcs = outgoingArcs ?? new List<IWeightedDirectedGraphArc>();
        }
        public IList<IWeightedDirectedGraphArc> OutgoingArcs { get; }
    }
}