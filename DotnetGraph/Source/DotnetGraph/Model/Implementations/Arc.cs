using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Model.Implementations
{
    public class Arc<TNode> : IHasDestination<TNode>
        where TNode : IHasOutgoingArcs<Arc<TNode>>
    {
        public Arc(TNode destination)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public TNode Destination { get; }
    }
}