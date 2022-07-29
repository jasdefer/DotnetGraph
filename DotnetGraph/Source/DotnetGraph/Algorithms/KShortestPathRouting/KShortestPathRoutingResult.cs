using Ardalis.GuardClauses;

namespace DotnetGraph.Algorithms.KShortestPathRouting;
public class KShortestPathRoutingResult<TArc>
    where TArc : IHasId
{
    public KShortestPathRoutingResult(IReadOnlyCollection<ReadOnlyCollection<TArc>> setOfPaths)
    {
        SetOfPaths = Guard.Against.Null(setOfPaths);
    }

    public IReadOnlyCollection<ReadOnlyCollection<TArc>> SetOfPaths { get; }
}
