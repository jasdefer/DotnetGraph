using System.Collections.Generic;

namespace DotnetGraph.Algorithms.ShortestPath.Dijkstra
{
    internal class DijkstraHeap
    {
        private readonly List<DijkstraNode> heap = new List<DijkstraNode>();

        internal DijkstraHeap(DijkstraNode origin)
        {
            Add(origin);
        }

        internal int Count => heap.Count;

        private static int GetLeftChildIndex(int parentIndex)
        {
            var indexOfLeftChild = parentIndex * 2;
            return indexOfLeftChild;
        }

        private static int GetRightChildIndex(int parentIndex)
        {
            var indexOfRightChild = parentIndex * 2 + 1;
            return indexOfRightChild;
        }

        private static int GetParentIndex(int childIndex)
        {
            var indexOfParent = childIndex / 2;
            return indexOfParent;
        }

        private void UpdateHeap(int index)
        {
            var indexOfLeftChild = GetLeftChildIndex(index);
            var indexOfRightChild = GetRightChildIndex(index);
            var indexOfSmallestValue = index;
            if(indexOfLeftChild < heap.Count &&
                IsSmaller(indexOfLeftChild, index))
            {
                indexOfSmallestValue = indexOfLeftChild;
            }
            if (indexOfRightChild < heap.Count &&
                IsSmaller(indexOfRightChild, indexOfSmallestValue))
            {
                indexOfSmallestValue = indexOfRightChild;
            }

            if(indexOfSmallestValue != index)
            {
                SwitchPosition(index, indexOfSmallestValue);
                UpdateHeap(indexOfSmallestValue);
            }
        }

        private void SwitchPosition(int index1, int index2)
        {
            var temp = heap[index1];
            heap[index1] = heap[index2];
            heap[index2] = temp;

            heap[index1].IndexInHeap = index1;
            heap[index2].IndexInHeap = index2;
        }

        private bool IsSmaller(int index1, int index2)
        {
            var isSmaller = heap[index1].DistanceFromOrigin.Value < heap[index2].DistanceFromOrigin.Value;
            return isSmaller;
        }

        private bool IsBigger(int index1, int index2)
        {
            var isSmaller = heap[index1].DistanceFromOrigin.Value > heap[index2].DistanceFromOrigin.Value;
            return isSmaller;
        }

        internal DijkstraNode ExtractMinimum()
        {
            var minimum = heap[0];
            SwitchPosition(0, heap.Count - 1);
            heap.RemoveAt(heap.Count - 1);
            UpdateHeap(0);
            return minimum;
        }

        internal void UpdateArc(DijkstraArc arc, double distance)
        {
            var node = arc.Destination;
            if (!node.DistanceFromOrigin.HasValue)
            {
                node.DistanceFromOrigin = distance;
                node.BestPredecessor = arc;
                Add(node);
                return;
            }
            if(distance >= node.DistanceFromOrigin.Value)
            {
                return;
            }
            var index = node.IndexInHeap;
            heap[index].DistanceFromOrigin = distance;
            SlideUpdwars(index);
        }

        internal void SlideUpdwars(int index)
        {
            var parent = GetParentIndex(index);
            while (index > 0 &&
                IsBigger(parent, index))
            {
                SwitchPosition(index, parent);
                index = parent;
                parent = GetParentIndex(index);
            }
        }

        internal void Add(DijkstraNode node)
        {
            node.IndexInHeap = heap.Count;
            heap.Add(node);
            SlideUpdwars(node.IndexInHeap);
        }
    }
}