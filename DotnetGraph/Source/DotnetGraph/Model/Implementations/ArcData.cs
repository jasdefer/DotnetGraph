using System;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations
{
    public class ArcData : IEquatable<ArcData>
    {
        public ArcData(int originNodeId, int destinationNodeId, double weight)
        {
            OriginNodeId = originNodeId;
            DestinationNodeId = destinationNodeId;
            Weight = weight;
        }

        public int OriginNodeId { get; }
        public int DestinationNodeId { get; }
        public double Weight { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ArcData);
        }

        public bool Equals(ArcData other)
        {
            return other != null &&
                   OriginNodeId == other.OriginNodeId &&
                   DestinationNodeId == other.DestinationNodeId &&
                   Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OriginNodeId, DestinationNodeId, Weight);
        }

        public static bool operator ==(ArcData left, ArcData right)
        {
            return EqualityComparer<ArcData>.Default.Equals(left, right);
        }

        public static bool operator !=(ArcData left, ArcData right)
        {
            return !(left == right);
        }
    }
}