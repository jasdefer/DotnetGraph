using System;
using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    internal class DijkstraQueue
    {
        private readonly List<DijkstraNode> queue = new List<DijkstraNode>();

        internal DijkstraQueue(DijkstraNode origin)
        {
            queue.Add(origin);
        }

        internal int Count => queue.Count;

        internal DijkstraNode ExctractNext()
        {
            var min = double.MaxValue;
            var index = (int?)null;
            for (int i = 0; i < queue.Count; i++)
            {
                if (queue[i].DistanceFromOrigin < min)
                {
                    min = queue[i].DistanceFromOrigin.Value;
                    index = i;
                }
            }
            if (index.HasValue)
            {
                var nextNode = queue[index.Value];
                queue.RemoveAt(index.Value);
                return nextNode;
            }

            throw new Exception("The destination is not connected to the origin.");
        }

        internal void Add(DijkstraNode destination)
        {
            queue.Add(destination);
        }
    }
}