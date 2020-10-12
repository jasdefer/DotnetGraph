using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    public class DijkstraAlgorithm : IShortestPathAlgorithm
    {
        public static void ValidateInput(IList<IWeightedDirectedGraphNode> nodes, int originIndex, int destinationIndex)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }
            if (originIndex < 0 || originIndex >= nodes.Count)
            {
                throw new ArgumentOutOfRangeException($"The origin index {originIndex} must point to an element of the {nodes.Count} nodes.");
            }
            if (destinationIndex < 0 || destinationIndex >= nodes.Count)
            {
                throw new ArgumentOutOfRangeException($"The destination index {destinationIndex} must point to an element of the {nodes.Count} nodes.");
            }
        }

        public ShortestPathResult GetShortestPath(IList<IWeightedDirectedGraphNode> nodes, int originIndex, int destinationIndex)
        {
            ValidateInput(nodes, originIndex, destinationIndex);
            var dijkstraNodes = new DijkstraNode[nodes.Count];
            for (int i = 0; i < dijkstraNodes.Length; i++)
            {
                dijkstraNodes[i] = new DijkstraNode(nodes[i].OutgoingArcs);
            }
            var shortestPathResult = GetShortestPath(dijkstraNodes, originIndex, destinationIndex);
            return shortestPathResult;
        }

        public static ShortestPathResult GetShortestPath(DijkstraNode[] nodes, int originIndex, int destinationIndex)
        {
            ValidateInput(nodes, originIndex, destinationIndex);
            throw new NotImplementedException();
        }
    }
}