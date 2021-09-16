using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
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
        public WeightedUndirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
        {
            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var nodes = GenerateNodes(numberOfNodes);
            ConnectNodesInALine(nodes, weightGenerator);
            var dict = GetPossibleNeighbors(nodes);
            CreateRandomEdges(nodes, dict, weightGenerator, density);

            return nodes;
        }

        private void CreateRandomEdges(WeightedUndirectedGraphNode[] nodes,
            Dictionary<int, List<int>> dict,
            INumberGenerator weightGenerator,
            double density)
        {
            //Add random edges between random nodes until the required density (number of edges) is reached
            var numberOfEdges = GraphPropertyHelper.NumberOfEdges(nodes.Length, density);

            //Improves the performances of randomly selecting an Id from the dict above
            var keys = dict.Keys.ToList();

            //The Id of the edges start at this number, because there already exist some edges connecting all nodes in a line
            var startIndex = nodes.Length - 1;

            for (int i = startIndex; i < numberOfEdges; i++)
            {
                //Select two random node Ids from the dict
                var key = Random.Next(0, dict.Count);
                var node1Id = keys[key];
                var node2Id = dict[node1Id][Random.Next(0, dict[node1Id].Count)];

                //Create the edge
                var weight = weightGenerator.Generate();
                var edge = new WeightedUndirectedGraphEdge(i + 1, weight, nodes[node1Id - 1], nodes[node2Id - 1]);
                nodes[node1Id - 1].Add(edge);
                nodes[node2Id - 1].Add(edge);

                //Remove the connection from the dict
                dict[node1Id].Remove(node2Id);
                if (dict[node1Id].Count == 0)
                {
                    dict.Remove(node1Id);
                    keys.RemoveAt(key);
                }
            }
        }

        /// <summary>
        /// Key: Id of a node, Value: List of the Ids of possible neighbors of the key node
        /// </summary>
        private static Dictionary<int, List<int>> GetPossibleNeighbors(WeightedUndirectedGraphNode[] nodes)
        {
            var dict = new Dictionary<int, List<int>>();
            var baseList = Enumerable.Range(3, nodes.Length - 2).ToList();
            for (int i = 0; i < nodes.Length - 2; i++)
            {
                var baseListCopy = new List<int>(baseList);
                dict.Add(i + 1, baseListCopy);
                baseList.RemoveAt(0);
            }
            return dict;
        }

        /// <summary>
        /// Connect all nodes in a line: 1-2-3-...-n
        /// </summary>
        private static void ConnectNodesInALine(WeightedUndirectedGraphNode[] nodes, INumberGenerator weightGenerator)
        {
            var numberOfConnectingEdges = nodes.Length - 1;
            for (int i = 0; i < numberOfConnectingEdges; i++)
            {
                var weight = weightGenerator.Generate();
                var edge = new WeightedUndirectedGraphEdge(i + 1, weight, nodes[i], nodes[i + 1]);
                nodes[i].Add(edge);
                nodes[i + 1].Add(edge);
            }
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