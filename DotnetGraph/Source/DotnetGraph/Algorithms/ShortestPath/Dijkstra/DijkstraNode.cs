using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraNode : IWeightedDirectedGraphNode
    {
        public DijkstraNode()
        {
            OutgoingArcs = new List<IWeightedDirectedGraphArc>();
        }

        public DijkstraNode(IList<IWeightedDirectedGraphArc> outgoingArcs)
        {
            OutgoingArcs = outgoingArcs ?? new List<IWeightedDirectedGraphArc>();
        }
        public IList<IWeightedDirectedGraphArc> OutgoingArcs { get; }
    }
}