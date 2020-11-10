using DotnetGraph.Model.Properties;
using System.Collections.Generic;


namespace DotnetGraph.Algorithms.DfSearch
{
    public class DfSearchNode : IHasOutgoingArcs<DfSearchArc>, IHasLabel
    {
        public int DiscoveredAt { get; set; }

        public int FinishedAt { get; set; }

        public string Label { get; }
        
        public IReadOnlyCollection<DfSearchArc> OutgoingArcs { get; }

    }
}
