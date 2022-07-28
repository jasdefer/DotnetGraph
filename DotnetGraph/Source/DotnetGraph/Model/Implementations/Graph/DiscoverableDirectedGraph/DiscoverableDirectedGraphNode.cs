namespace DotnetGraph.Model.Implementations.Graph.DiscoverableDirectedGraph;

public class DiscoverableDirectedGraphNode :
    IHasId,
    IHasDiscoverInformation,
    IHasOutgoingArcs<DiscoverableDirectedGraphArc>
{
    private readonly List<DiscoverableDirectedGraphArc> outgoingArcs;
    public int Id { get; }
    public int ExploredTime { get; set; }
    public int DiscoveredTime { get; set; }

    public DiscoverableDirectedGraphNode(int id, IList<DiscoverableDirectedGraphArc> outgoingArcs = null)
    {
        Id = id;
        this.outgoingArcs = outgoingArcs is null ? new List<DiscoverableDirectedGraphArc>() : new List<DiscoverableDirectedGraphArc>(outgoingArcs);
    }

    public void AddArc(DiscoverableDirectedGraphArc arc)
    {
        if (arc is null)
        {
            throw new ArgumentNullException(nameof(arc));
        }
        outgoingArcs.Add(arc);
    }

    public IReadOnlyCollection<DiscoverableDirectedGraphArc> OutgoingArcs => outgoingArcs;
}
