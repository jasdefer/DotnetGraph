using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDfs
{
    public class CormenDfsAlgorithm : IDepthFirstSearchAlgorithm
    {
        private int _time;

        public void Run(IEnumerable<DfSearchNode> nodes)
        {
            if (nodes == null || !nodes.Any())
                throw new ArgumentException($"{nameof(nodes)} is null or empty.");

            Initialize(nodes);
            foreach (DfSearchNode n in nodes)
            {
                if (n.SearchState == DfSearchState.Undiscovered)
                {
                    Visit(n);
                }
            }
        }

        private void Visit(DfSearchNode node)
        {
            _time++;
            node.DiscoveredAt = _time;
            node.SearchState = DfSearchState.Discovered;
            foreach (DfSearchArc incidentArc in node.OutgoingArcs)
            {
                DfSearchNode adjacentNode = incidentArc.Node2;
                if (adjacentNode.SearchState == DfSearchState.Undiscovered)
                {
                    adjacentNode.PredecessorNode = node;
                    Visit(adjacentNode);
                }
            }
            node.SearchState = DfSearchState.Visited;
            _time++;
            node.FinishedAt = _time;
        }

        private void Initialize(IEnumerable<DfSearchNode> nodes)
        {
            _time = 0;
            foreach (DfSearchNode n in nodes)
            {
                n.SearchState = DfSearchState.Undiscovered;
                n.PredecessorNode = null;
            }
        }

        public DepthFirstSearchResult Run<TNode, TArc>(IList<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            throw new NotImplementedException();
        }
    }
}
