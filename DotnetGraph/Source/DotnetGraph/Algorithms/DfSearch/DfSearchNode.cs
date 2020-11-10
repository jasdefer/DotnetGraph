using DotnetGraph.Model.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DotnetGraph.Algorithms.DfSearch
{
    public class DfSearchNode : IHasOutgoingArcs<DfSearchArc>, IHasLabel
    {
        private List<DfSearchArc> _arcs;

        public DfSearchNode(string label) : this()
        {
            Label = label;
        }

        private DfSearchNode()
        {
            _arcs = new List<DfSearchArc>();
            OutgoingArcs = new ReadOnlyCollection<DfSearchArc>(_arcs);
        }

        public void Link(DfSearchArc arc)
        {
            _arcs.Add(arc);
        }

        public void LinkToNode(DfSearchNode node)
        {
            _arcs.Add(new DfSearchArc(this, node));
        }

        public int DiscoveredAt { get; set; }

        public int FinishedAt { get; set; }

        public string Label { get; }

        public IReadOnlyCollection<DfSearchArc> OutgoingArcs { get; }

        public DfSearchState SearchState { get; set; }

        public DfSearchNode PredecessorNode { get; set; }

    }
}
