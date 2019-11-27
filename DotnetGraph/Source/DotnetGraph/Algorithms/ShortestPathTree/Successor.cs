using System;

namespace DotnetGraph.Algorithms.ShortestPathTree
{
    public struct Successor : IEquatable<Successor>
    {
        public int Node { get; }
        public double Weight { get; }

        public Successor(int node, double weight)
        {
            Node = node;
            Weight = weight;
        }

        public override bool Equals(object obj)
        {
            return obj is Successor successor && Equals(successor);
        }

        public bool Equals(Successor other)
        {
            return Node == other.Node &&
                   Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Node, Weight);
        }

        public static bool operator ==(Successor left, Successor right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Successor left, Successor right)
        {
            return !(left == right);
        }
    }
}