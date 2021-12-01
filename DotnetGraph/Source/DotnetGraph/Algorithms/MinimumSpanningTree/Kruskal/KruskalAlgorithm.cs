namespace DotnetGraph.Algorithms.MinimumSpanningTree.Kruskal
{
    public class KruskalAlgorithm : IMinimumSpanningTreeAlgorithm
    {
        public MinimumSpanningTreeResult<TEdge> GetMinimumSpanningTree<TNode, TEdge>(IEnumerable<TNode> nodes)
            where TEdge : IConnectsNodes<TNode>, IHasId, IHasWeight
            where TNode : IHasId, IHasEdges<TEdge>
        {
            var orderdEdges = nodes
                .SelectMany(x => x.Edges)
                .Distinct()
                .OrderBy(x => x.Weight)
                .ToArray();
            var tree = new List<TEdge>();

            var dict = nodes.ToDictionary(x => x.Id, x => new HashSet<int>() { x.Id });
            var treeSize = dict.Count - 1;
            var totalWeight = 0d;
            var index = 0;
            while (tree.Count < treeSize)
            {
                var edge = orderdEdges[index];
                var connectsTwoTrees = !dict[edge.Node1.Id].Contains(edge.Node2.Id);
                if (connectsTwoTrees)
                {
                    dict[edge.Node1.Id].UnionWith(dict[edge.Node2.Id]);
                    dict[edge.Node2.Id] = dict[edge.Node1.Id];
                    tree.Add(orderdEdges[index]);
                    totalWeight += orderdEdges[index].Weight;
                }
                index++;
            }
            var result = new MinimumSpanningTreeResult<TEdge>(totalWeight, tree);
            return result;
        }
    }
}