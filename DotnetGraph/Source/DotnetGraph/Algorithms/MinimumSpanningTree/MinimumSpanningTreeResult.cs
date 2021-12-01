namespace DotnetGraph.Algorithms.MinimumSpanningTree;

public class MinimumSpanningTreeResult<TEdge>
{
    public MinimumSpanningTreeResult(double totalWeight, IReadOnlyCollection<TEdge> tree)
    {
        Tree = tree ?? throw new ArgumentNullException(nameof(tree));
        TotalWeight = totalWeight;
    }
    public double TotalWeight { get; }
    public IReadOnlyCollection<TEdge> Tree { get; set; }
}
