using DotnetGraph.Model.Graphs.WeightedDirectedGraph;
using System;
using System.Collections.Generic;

namespace DotnetGraph.Helper
{
    public static class NodeConnector
    {
        private static void AddArcWithoutValidation(this IList<IWeightedDirectedGraphNode> nodes, int originIndex, int destinationIndex, double weight)
        {
            var arc = new WeightedDirectedGraphArc(nodes[destinationIndex], weight);
            nodes[originIndex].OutgoingArcs.Add(arc);
        }

        private static void Validate(IList<IWeightedDirectedGraphNode> nodes, int originIndex, int destinationIndex)
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

        public static void AddArc(this IList<IWeightedDirectedGraphNode> nodes, int originIndex, int destinationIndex, double weight)
        {
            Validate(nodes, originIndex, destinationIndex);
            nodes.AddArcWithoutValidation(originIndex, destinationIndex, weight);
        }

        public static void AddArcInBothDirections(this IList<IWeightedDirectedGraphNode> nodes, int firstNodeIndex, int secondNodeIndex, double weight)
        {
            Validate(nodes, firstNodeIndex, secondNodeIndex);
            nodes.AddArcWithoutValidation(firstNodeIndex, secondNodeIndex, weight);
            nodes.AddArcWithoutValidation(secondNodeIndex, firstNodeIndex, weight);
        }
    }
}