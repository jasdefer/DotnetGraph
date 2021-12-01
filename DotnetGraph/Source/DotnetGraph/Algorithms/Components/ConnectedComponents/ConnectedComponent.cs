namespace DotnetGraph.Algorithms.Components.ConnectedComponents;

public class ConnectedComponent<TNode>
{
    public ConnectedComponent(ReadOnlyCollection<TNode> nodes)
    {
        Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
    }

    public ReadOnlyCollection<TNode> Nodes { get; }
}
