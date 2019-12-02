using DotnetGraph.Algorithms.Contracts.GraphGeneration;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration
{
    /// <summary>
    /// Distribute the nodes in a n-dimensional space and connect each node to each nearest node in each dimension.
    /// So the number of leaving arcs is not greater than the number of dimensions.
    /// </summary>
    public class CornerFlowGenerator : IDirectedGraphGeneration
    {
        private int dimensions = 2;

        public int Dimensions
        {
            get { return dimensions; }
            set 
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                dimensions = value; 
            }
        }

        public Random Random { get; set; } = new Random(1);
        public IWeightGenerator WeightGenerator { get; set; } = new UniformWeightGenerator();
        public Arc<T>[] GenerateGraph<T>(IEnumerable<T> nodes)
        {
            var coordinates = GenerateCoordinates(nodes);
            T[,] sortedNodes = GetSortedNodes(coordinates);
            var arcs = GenerateArcs(sortedNodes);
            return arcs.ToArray();
        }

        /// <summary>
        /// Connect each node the clostest node for each dimension.
        /// The arc connects the nodes in one direction only.
        /// </summary>
        private Arc<T>[] GenerateArcs<T>(T[,] sortedNodes)
        {
            var arcs = new List<Arc<T>>();
            var nodeCount = sortedNodes.GetLength(0);
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 1; j < nodeCount; j++)
                {
                    var weight = WeightGenerator.Create();
                    var origin = sortedNodes[j - 1, i];
                    var destination = sortedNodes[j, i];
                    var arc = new Arc<T>(origin, destination, weight);
                    var isNewArc = IsNewArc(arc, arcs);
                    if (isNewArc)
                    {
                        arcs.Add(arc);
                    }
                }
            }
            return arcs.ToArray();
        }

        /// <summary>
        /// Check if <paramref name="arcs"/> already contains a connection with the same origin and destination as <paramref name="arc"/>.
        /// </summary>
        /// <returns>True, if <paramref name="arcs"/> does not contain an arc with the same origin and destination as <paramref name="arc"/></returns>
        private bool IsNewArc<T>(Arc<T> arc, List<Arc<T>> arcs)
        {
            for (int i = 0; i < arcs.Count; i++)
            {
                if(arcs[i].Origin.Equals(arc.Origin) &&
                    arcs[i].Destination.Equals(arc.Destination))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sort the node in each dimension.
        /// </summary>
        /// <returns>
        /// Each element of the 2d array represents a node.
        /// Each column represents the order of nodes for each dimension.
        /// </returns>
        private T[,] GetSortedNodes<T>(Dictionary<T, int[]> coordinates)
        {
            var order = new T[coordinates.Count, Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                var elements = coordinates.OrderBy(x => x.Value.ElementAt(i))
                    .Select(x => x.Key).ToArray();
                for (int j = 0; j < elements.Length; j++)
                {
                    order[j, i] = elements[j];
                }
            }
            return order;
        }

        /// <summary>
        /// Generate random coordinates for each node in each dimension
        /// </summary>
        /// <returns>Key: node, Value: Set of coordinates.</returns>
        private Dictionary<T, int[]> GenerateCoordinates<T>(IEnumerable<T> nodes)
        {
            var dict = new Dictionary<T, int[]>();
            if (nodes is null)
            {
                return dict;
            }
            foreach (var node in nodes)
            {
                var coordinates = new int[Dimensions];
                for (int i = 0; i < Dimensions; i++)
                {
                    coordinates[i] = Random.Next(0, 1000);
                }
                dict.Add(node, coordinates);
            }
            return dict;
        }
    }
}