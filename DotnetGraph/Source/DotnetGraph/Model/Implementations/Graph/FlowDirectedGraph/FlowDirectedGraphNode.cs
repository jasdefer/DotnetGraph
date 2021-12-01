namespace DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph
{
    public class FlowDirectedGraphNode :
        IHasId,
        IHasOutgoingArcs<FlowDirectedGraphArc>
    {
        private readonly List<FlowDirectedGraphArc> outgoingArcs;

        public FlowDirectedGraphNode(int id, IReadOnlyCollection<FlowDirectedGraphArc> outgoingArcs = null)
        {
            Id = id;
            this.outgoingArcs = outgoingArcs is null ? new List<FlowDirectedGraphArc>() : new List<FlowDirectedGraphArc>(outgoingArcs);
        }

        public IReadOnlyCollection<FlowDirectedGraphArc> OutgoingArcs => outgoingArcs;

        public int Id { get; }

        public void Add(FlowDirectedGraphArc arc)
        {
            outgoingArcs.Add(arc);
        }
    }
}