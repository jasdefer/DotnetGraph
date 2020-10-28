using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Algorithms.ShortestPathTree.Fifo
{
    public class FifoArc : WeightedArc<FifoNode>,
        IHasWeight,
        IHasDestination<FifoNode>,
        IHasId
    {
        public FifoArc(int id, FifoNode origin, FifoNode destination, double weight) : base(destination, weight)
        {
            Id = id;
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
        }

        public FifoNode Origin { get; }
        public int Id { get; }

        public override string ToString()
        {
            return $"{Origin.Id}->{Destination.Id} ({Weight})";
        }
    }
}