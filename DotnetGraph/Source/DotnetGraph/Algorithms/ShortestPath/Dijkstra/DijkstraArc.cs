using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraArc : WeightedArc<DijkstraNode>,
        IHasWeight,
        IHasDestination<DijkstraNode>,
        IHasId
    {
        public DijkstraArc(int id, DijkstraNode origin, DijkstraNode destination, double weight) : base(destination, weight)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Id = id;
        }

        public DijkstraNode Origin { get; }
        public int Id { get; }

        public override string ToString()
        {
            return $"{Origin.Id}->{Destination.Id} ({Weight})";
        }
    }
}