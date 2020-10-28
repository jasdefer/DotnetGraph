using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Properties;

namespace DotnetGraph.Algorithms.ShortestPathTree.Fifo
{
    public class FifoNode : IdNode<FifoArc>,
        IHasOutgoingArcs<FifoArc>,
        IHasId
    {
        public FifoNode(int id) : base(id)
        {
        }

        public double? DistanceFromOrigin { get; internal set; }
        public FifoArc BestPredecessor { get; internal set; }
        internal int IndexInHeap { get; set; } = -1;

        public override string ToString()
        {
            return $"Id {Id}";
        }
    }
}
