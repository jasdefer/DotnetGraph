using DotnetGraph.Model.Properties;
using System.Collections.Generic;

namespace DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph
{
    public class WeightedDirectedGraphNode : 
        IHasOutgoingArcs<WeightedDirectedGraphArc>,
        IHasId 
    {
        private readonly List<WeightedDirectedGraphArc> outgoingArcs;
        public WeightedDirectedGraphNode(int id, IReadOnlyCollection<WeightedDirectedGraphArc> outgoingArcs = null)
        {
            Id = id;
            this.outgoingArcs = outgoingArcs is null ? new List<WeightedDirectedGraphArc>() : new List<WeightedDirectedGraphArc>(outgoingArcs);
        }

        public IReadOnlyCollection<WeightedDirectedGraphArc> OutgoingArcs => outgoingArcs;

        public int Id { get; }

        public void Add(WeightedDirectedGraphArc arc)
        {
            outgoingArcs.Add(arc);
        }
    };
}