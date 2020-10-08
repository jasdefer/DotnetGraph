using DotnetGraph.Model;
using DotnetGraph.Model.ModelImplementation;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraNode : Node<IWeightedArc>
    {
        public DijkstraNode(string label, IList<IWeightedArc> outgoingArcs) : base(label, outgoingArcs)
        {
        }

        public IWeightedArc BestPredecessor { get; set; }
    }
}