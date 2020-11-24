using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Algorithms.NetworkFlow.MaxFlow.FordFulkerson
{
    public class FordFulkersonArc :
        IHasId,
        IHasCapacity,
        IHasFlow,
        IHasDestination<FordFulkersonNode>
    {
        public FordFulkersonArc(int id,
            double capacity,
            FordFulkersonNode origin,
            FordFulkersonNode destination,
            double flow = 0)
        {
            Id = id;
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            Capacity = capacity;
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Flow = flow;

        }

        public int Id { get; }
        public double Capacity { get; }
        public double Flow { get; set; }
        public FordFulkersonNode Origin { get; set; }
        public FordFulkersonNode Destination { get; }

        public override string ToString()
        {
            return $"{Origin.Id} to {Destination.Id} ({Flow}/{Capacity})";
        }
    }
}