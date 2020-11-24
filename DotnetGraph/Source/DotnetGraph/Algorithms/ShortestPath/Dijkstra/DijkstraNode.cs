using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraNode :
        IHasOutgoingArcs<DijkstraArc>,
        IHasId
    {
        private readonly List<DijkstraArc> arcs;
        public DijkstraNode(int id, IList<DijkstraArc> arcs = null)
        {
            Id = id;
            this.arcs = arcs is null ? new List<DijkstraArc>() : new List<DijkstraArc>(arcs);
        }
        public int Id { get; }
        public IReadOnlyCollection<DijkstraArc> OutgoingArcs => arcs;
        public DijkstraArc BestPredecessor { get; internal set; }
        public double? DistanceFromOrigin { get; internal set; }
        internal int IndexInHeap { get; set; } = -1;
        public void AddArc(DijkstraArc arc)
        {
            arcs.Add(arc);
        }
    }
}