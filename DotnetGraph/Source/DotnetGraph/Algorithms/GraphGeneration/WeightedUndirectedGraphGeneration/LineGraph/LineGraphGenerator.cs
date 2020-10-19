using DotnetGraph.Algorithms.GraphGeneration.Misc.WeightGenerator;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph
{
    public class LineGraphGenerator : IWeightedUndirectedGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, IWeightGenerator weightGenerator)
        {
            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var nodes = GenerateNodes(numberOfNodes);
            for (int i = 0; i < numberOfNodes - 1; i++)
            {
                var weight = weightGenerator.Generate();
                var edge = new WeightedUndirectedGraphEdge(i + 1, weight, nodes[i], nodes[i + 1]);
                nodes[i].AddEdge(edge);
                nodes[i + 1].AddEdge(edge);
            }

            var numberOfEdges = (int)Math.Round((density * GraphPropertyHelper.NumberOfPossibleEdges(numberOfNodes)));
            var dict = new Dictionary<int, List<int>>();
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                dict.Add(i + 1, Enumerable.Range(i + 2, nodes.Length - i - 1).ToList());
            }
            for (int i = numberOfNodes - 1; i < numberOfEdges; i++)
            {
                var node1Id = dict.Keys.ElementAt(Random.Next(0, dict.Count));
                var node2Id = dict[node1Id][Random.Next(0, dict[node1Id].Count)];

                var weight = weightGenerator.Generate();
                var edge = new WeightedUndirectedGraphEdge(i + 1, weight, nodes[node1Id - 1], nodes[node2Id - 1]);
                nodes[node1Id - 1].AddEdge(edge);
                nodes[node2Id - 1].AddEdge(edge);

                dict[node1Id].Remove(node2Id);
                if (dict[node1Id].Count == 0)
                {
                    dict.Remove(node1Id);
                }
            }
            return nodes;
        }

        private static WeightedUndirectedGraphNode[] GenerateNodes(int numberOfNodes)
        {
            var nodes = new WeightedUndirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedUndirectedGraphNode(i + 1);
            }
            return nodes;
        }
    }
}