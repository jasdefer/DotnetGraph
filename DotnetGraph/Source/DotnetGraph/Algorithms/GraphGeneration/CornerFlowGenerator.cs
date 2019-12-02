using DotnetGraph.Algorithms.Contracts.GraphGeneration;
using DotnetGraph.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration
{
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