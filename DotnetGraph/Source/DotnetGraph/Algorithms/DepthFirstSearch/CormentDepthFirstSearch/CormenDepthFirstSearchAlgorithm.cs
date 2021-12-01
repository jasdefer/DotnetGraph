namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch
{
    public class CormenDepthFirstSearchAlgorithm : IDepthFirstSearchAlgorithm
    {
        public void Run<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>
        {
            var convertedNodes = Convert<TNode, TArc>(nodes);
            Run(convertedNodes);
            StoreResult<TNode, TArc>(nodes, convertedNodes);
        }

        private static void StoreResult<TNode, TArc>(IReadOnlyList<TNode> nodes, IReadOnlyList<CormenDepthFirstSearchNode> convertedNodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>
        {
            var dict = nodes.ToDictionary(x => x.Id, x => x);
            for (int i = 0; i < convertedNodes.Count; i++)
            {
                var node = dict[convertedNodes[i].Id];
                node.DiscoveredTime = convertedNodes[i].DiscoveredTime;
                node.ExploredTime = convertedNodes[i].ExploredTime;
            }
        }

        private static IReadOnlyList<CormenDepthFirstSearchNode> Convert<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId, IHasDestination<TNode>
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var dict = nodes.ToDictionary(x => x.Id, x => new CormenDepthFirstSearchNode(x.Id));

            for (int i = 0; i < nodes.Count; i++)
            {
                var origin = dict[nodes[i].Id];
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var cormenDepthFirstSearchArc = new CormenDepthFirstSearchArc(arc.Id, origin, dict[arc.Destination.Id]);
                    origin.AddArc(cormenDepthFirstSearchArc);
                }
            }
            return dict.Values.ToList();
        }

        public static void ValidateInput<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            GraphValidation.ValidateUniqueIds(nodes);
            GraphValidation.ValidateUniqueArcIds<TNode, TArc>(nodes);
        }

        private int time;

        public void Run(IReadOnlyList<CormenDepthFirstSearchNode> nodes)
        {
            if (nodes == null || !nodes.Any())
            {
                throw new ArgumentException($"{nameof(nodes)} is null or empty.");
            }

            Initialize(nodes);
            foreach (CormenDepthFirstSearchNode n in nodes)
            {
                if (n.SearchState == SearchState.Undiscovered)
                {
                    Visit(n);
                }
            }
        }

        private void Visit(CormenDepthFirstSearchNode node)
        {
            time++;
            node.DiscoveredTime = time;
            node.SearchState = SearchState.Discovered;
            foreach (CormenDepthFirstSearchArc incidentArc in node.OutgoingArcs)
            {
                CormenDepthFirstSearchNode adjacentNode = incidentArc.Destination;
                if (adjacentNode.SearchState == SearchState.Undiscovered)
                {
                    adjacentNode.PredecessorNode = node;
                    Visit(adjacentNode);
                }
            }
            node.SearchState = SearchState.Visited;
            time++;
            node.ExploredTime = time;
        }

        private void Initialize(IEnumerable<CormenDepthFirstSearchNode> nodes)
        {
            time = 0;
            foreach (CormenDepthFirstSearchNode node in nodes)
            {
                node.SearchState = SearchState.Undiscovered;
                node.PredecessorNode = null;
            }
        }
    }
}