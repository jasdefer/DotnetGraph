using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraAlgorithm : IShortestPathAlgorithm
    {
        public ShortestPathResult GetShortestPath(IEnumerable<INode<IWeightedArc>> nodes,
            INode<IWeightedArc> origin,
            INode<IWeightedArc> destination)
        {
            throw new NotImplementedException();
        }

        public ShortestPathResult GetShortestPath(IEnumerable<DijkstraNode> nodes,
            DijkstraNode origin,
            DijkstraNode destination)
        {
            throw new NotImplementedException();
        }
    }
}