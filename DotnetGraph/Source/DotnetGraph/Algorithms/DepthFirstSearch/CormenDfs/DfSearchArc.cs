using DotnetGraph.Model.Properties;
using System;

namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDfs
{
    public class DfSearchArc : IConnectsNodes<DfSearchNode>, IHasId
    {

        public DfSearchArc(DfSearchNode n1, DfSearchNode n2)
        {
            Node1 = n1 ?? throw new ArgumentNullException(nameof(n1));
            Node2 = n2 ?? throw new ArgumentNullException(nameof(n2));
        }

        public DfSearchNode Node1 { get; }

        public DfSearchNode Node2 { get; }
        public int Id { get; }
    }
}