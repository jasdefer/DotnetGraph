using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Properties;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraNode : IdNode<DijkstraArc>,
        IHasOutgoingArcs<DijkstraArc>,
        IHasId
    {
        public DijkstraNode(int id) : base(id)
        {
        }

        public DijkstraArc BestPredecessor { get; internal set; }
        public double? DistanceFromOrigin { get; internal set; }
        internal int IndexInHeap { get; set; } = -1;

        public override string ToString()
        {
            return $"Id {Id} ({DistanceFromOrigin ?? double.PositiveInfinity})";
        }
    }
}