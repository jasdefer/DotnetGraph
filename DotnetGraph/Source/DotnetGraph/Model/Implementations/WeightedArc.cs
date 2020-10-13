using DotnetGraph.Model.Properties;

namespace DotnetGraph.Model.Implementations
{
    public class WeightedArc<TNode> : Arc<TNode>, IHasWeight
        where TNode : IHasOutgoingArcs<WeightedArc<TNode>>
    {
        public WeightedArc(TNode destination, double weight) : base(destination)
        {
            Weight = weight;
        }

        public double Weight { get; }
    }
}