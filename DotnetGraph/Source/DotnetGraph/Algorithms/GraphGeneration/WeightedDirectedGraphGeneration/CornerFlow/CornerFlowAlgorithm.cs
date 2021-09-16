using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.CornerFlow
{
    public class CornerFlowAlgorithm : IWeightedDirectedGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
        {
            var expectedNumberOfArcs = GraphPropertyHelper.NumberOfArcs(numberOfNodes, density);
            var dimensions = (int)Math.Ceiling((double)expectedNumberOfArcs / (numberOfNodes - 1));
            var coordinates = GenerateCoordinates(numberOfNodes, dimensions);
            var orderOfNodesPerDimension = GetOrderOfNodesPerDimension(coordinates, dimensions);
            var nodes = CreateNodes(numberOfNodes);
            AddArcs(nodes, orderOfNodesPerDimension, dimensions, weightGenerator, expectedNumberOfArcs);
            return nodes;
        }

        private static void AddArcs(WeightedDirectedGraphNode[] nodes,
            int[][] orderOfNodesPerDimension,
            int dimensions,
            INumberGenerator weightGenerator,
            int maxNumberOfArcs)
        {
            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var arcId = 0;
            var dict = nodes.ToDictionary(x => x.Id, x => x);
            for (int i = 0; i < dimensions; i++)
            {
                
                for (int j = 1; j < orderOfNodesPerDimension[i].Length; j++)
                {
                    var origin = dict[orderOfNodesPerDimension[i][j-1]];
                    var destination = dict[orderOfNodesPerDimension[i][j]];
                    var weight = weightGenerator.Generate();
                    var arc = new WeightedDirectedGraphArc(++arcId, weight, destination);
                    origin.Add(arc);
                    origin = destination;
                    if (arcId >= maxNumberOfArcs)
                    {
                        return;
                    }
                }
            }
        }

        private static WeightedDirectedGraphNode[] CreateNodes(int numberOfNodes)
        {
            var nodes = new WeightedDirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedDirectedGraphNode(i + 1);
            }
            return nodes;
        }

        private static int[][] GetOrderOfNodesPerDimension(Dictionary<int, int[]> coordinates, int dimensions)
        {
            var orderOfNodesPerDimension = new int[coordinates.Count][];
            for (int i = 0; i < dimensions; i++)
            {
                orderOfNodesPerDimension[i] = coordinates.OrderBy(x => x.Value[i])
                    .Select(x => x.Key)
                    .ToArray();
            }
            return orderOfNodesPerDimension;
        }

        private Dictionary<int, int[]> GenerateCoordinates(int numberOfNodes, int dimensions)
        {
            var dict = new Dictionary<int, int[]>();
            for (int i = 0; i < numberOfNodes; i++)
            {
                var coordinates = new int[dimensions];
                for (int j = 0; j < coordinates.Length; j++)
                {
                    coordinates[j] = Random.Next(0, int.MaxValue);
                }
                dict.Add(i + 1, coordinates);
            }
            return dict;
        }
    }
}