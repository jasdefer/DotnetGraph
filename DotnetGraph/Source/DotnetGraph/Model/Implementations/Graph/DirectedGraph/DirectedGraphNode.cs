using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph
{
    public class DirectedGraphNode :
        IHasOutgoingArcs<DirectedGraphArc>,
        IHasId
    {
        private readonly List<DirectedGraphArc> outgoingArcs;

        public DirectedGraphNode(int id, IList<DirectedGraphArc> outgoingArcs = null)
        {
            Id = id;
            this.outgoingArcs = outgoingArcs is null ? new List<DirectedGraphArc>() : new List<DirectedGraphArc>(outgoingArcs);
        }

        public IReadOnlyCollection<DirectedGraphArc> OutgoingArcs => outgoingArcs;
        public int Id { get; }
        public void Add(DirectedGraphArc arc)
        {
            outgoingArcs.Add(arc);
        }
    }
}