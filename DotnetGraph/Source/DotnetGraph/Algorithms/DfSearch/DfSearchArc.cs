using DotnetGraph.Model.Properties;


namespace DotnetGraph.Algorithms.DfSearch
{
    public class DfSearchArc : IConnectsNodes<DfSearchNode>
    {
        public DfSearchNode Node1 { get; }

        public DfSearchNode Node2 { get; }
    }
}
