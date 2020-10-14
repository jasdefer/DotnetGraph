using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.LineGraph
{
    public class LineGraphGenerator : IWeightedDirectGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator)
        {
            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var nodes = new WeightedDirectedGraphNode[numberOfNodes];
            var arcId = 1;
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedDirectedGraphNode(i + 1);
                if (i > 0)
                {
                    var weight = weightGenerator.Generate();
                    AddArcInBothDirections(arcId, nodes[i - 1], nodes[i], weight);
                    arcId += 2;
                }
            }

            var nodeDictionary = nodes.ToDictionary(x => x.Id, x => x);
            var numberOfEdges = numberOfNodes * (numberOfNodes + 1) / 2 * density - (nodes.Length - 1);
            var ids = Enumerable.Range(1, nodes.Length).ToList();
            var dict = nodes.ToDictionary(x => x.Id, x => new List<int>(ids));
            for (int i = 0; i < nodes.Length; i++)
            {
                dict[i + 1].Remove(i + 2);
                dict[i + 1].Remove(i + 1);
                dict[i + 1].Remove(i);
            }
            while (numberOfEdges > 0)
            {
                var nodeId1 = ids[Random.Next(0, ids.Count)];
                var nodeId2 = dict[nodeId1][Random.Next(0, dict[nodeId1].Count)];
                var weight = weightGenerator.Generate();
                AddArcInBothDirections(arcId, nodeDictionary[nodeId1], nodeDictionary[nodeId2], weight);
                arcId += 2;

                dict[nodeId1].Remove(nodeId2);
                dict[nodeId2].Remove(nodeId1);
                if (dict[nodeId1].Count == 0)
                {
                    ids.Remove(nodeId1);
                }
                if (dict[nodeId2].Count == 0)
                {
                    ids.Remove(nodeId2);
                }
                numberOfEdges--;
            }
            return nodes;
        }

        private static void AddArcInBothDirections(int id, WeightedDirectedGraphNode node1, WeightedDirectedGraphNode node2, double weight)
        {
            var arc = new WeightedDirectedGraphArc(id, node2, weight);
            node1.AddArc(arc);
            arc = new WeightedDirectedGraphArc(id + 1, node1, weight);
            node2.AddArc(arc);
        }
    }
}