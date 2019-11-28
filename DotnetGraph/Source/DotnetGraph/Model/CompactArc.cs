using System;

namespace DotnetGraph.Model
{
    public struct CompactArc : IEquatable<CompactArc>
    {
        public CompactArc(int origin, int destination, double weight)
        {
            Origin = origin;
            Destination = destination;
            Weight = weight;
        }

        public int Origin { get;}
        public int Destination { get;  }
        public double Weight { get;  }

        public override bool Equals(object obj)
        {
            return obj is CompactArc arc && Equals(arc);
        }

        public bool Equals(CompactArc other)
        {
            return Origin == other.Origin &&
                   Destination == other.Destination &&
                   Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Origin, Destination, Weight);
        }

        public static bool operator ==(CompactArc left, CompactArc right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CompactArc left, CompactArc right)
        {
            return !(left == right);
        }
    }
}