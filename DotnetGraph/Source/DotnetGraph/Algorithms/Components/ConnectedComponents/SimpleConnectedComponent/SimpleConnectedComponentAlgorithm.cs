namespace DotnetGraph.Algorithms.Components.ConnectedComponents.SimpleConnectedComponent;

public class SimpleConnectedComponentAlgorithm : IConnectedComponentsAlgorithm
{
    public ConnectedComponentResult<TNode> GetComponents<TNode, TEdge>(IEnumerable<TNode> nodes)
        where TNode : IHasId, IHasEdges<TEdge>
        where TEdge : IConnectsNodes<TNode>, IHasId
    {
        if (nodes is null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        var components = new List<ConnectedComponent<TNode>>();
        var visitedNodes = new HashSet<int>();
        var component = new List<TNode>();
        foreach (var node in nodes)
        {
            if (!visitedNodes.Contains(node.Id))
            {
                AddNeighbors<TNode, TEdge>(node, component, visitedNodes);
                components.Add(new ConnectedComponent<TNode>(component.AsReadOnly()));
                component = new List<TNode>();
            }
        }
        var result = new ConnectedComponentResult<TNode>(components.AsReadOnly());
        return result;
    }

    private static void AddNeighbors<TNode, TEdge>(TNode node, List<TNode> component, HashSet<int> visitedNodes)
        where TNode : IHasId, IHasEdges<TEdge>
        where TEdge : IConnectsNodes<TNode>, IHasId
    {
        component.Add(node);
        visitedNodes.Add(node.Id);
        foreach (var edge in node.Edges)
        {
            var neighbor = edge.Node1.Equals(node) ? edge.Node2 : edge.Node1;
            if (!visitedNodes.Contains(neighbor.Id))
            {
                AddNeighbors<TNode, TEdge>(neighbor, component, visitedNodes);
            }
        }
    }
}
