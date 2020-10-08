using DotnetGraph.Model;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Fifo
{
    public class FifoAlgorithm : IShortestPathAlgorithm
    {
        public ShortestPathResult GetShortestPath(IEnumerable<INode<IWeightedArc>> nodes,
            INode<IWeightedArc> origin,
            INode<IWeightedArc> destination)
        {
            throw new NotImplementedException();
        }
    }
}