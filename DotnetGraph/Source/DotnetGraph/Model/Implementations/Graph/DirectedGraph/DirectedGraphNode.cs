using System.Diagnostics;

namespace DotnetGraph.Model.Implementations.Graph.DirectedGraph;

[DebuggerDisplay("Node {Id}")]
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
