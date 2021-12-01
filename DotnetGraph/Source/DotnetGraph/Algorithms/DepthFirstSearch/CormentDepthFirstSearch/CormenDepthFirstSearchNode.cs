using System.Diagnostics;

namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch;

[DebuggerDisplay("Node {Id}. Discover {DiscoveredTime} / {ExploredTime}")]
public class CormenDepthFirstSearchNode :
    IHasOutgoingArcs<CormenDepthFirstSearchArc>,
    IHasId,
    IHasDiscoverInformation
{
    private readonly List<CormenDepthFirstSearchArc> outgoingArcs;

    public CormenDepthFirstSearchNode(int id, IReadOnlyCollection<CormenDepthFirstSearchArc> outgoingArcs = null)
    {
        Id = id;
        this.outgoingArcs = outgoingArcs is null ? new List<CormenDepthFirstSearchArc>() : new List<CormenDepthFirstSearchArc>(outgoingArcs);
    }

    public void AddArc(CormenDepthFirstSearchArc arc)
    {
        outgoingArcs.Add(arc);
    }

    public int DiscoveredTime { get; set; }

    public int ExploredTime { get; set; }

    public IReadOnlyCollection<CormenDepthFirstSearchArc> OutgoingArcs => outgoingArcs;

    public SearchState SearchState { get; set; }

    public CormenDepthFirstSearchNode PredecessorNode { get; set; }
    public int Id { get; }
}
