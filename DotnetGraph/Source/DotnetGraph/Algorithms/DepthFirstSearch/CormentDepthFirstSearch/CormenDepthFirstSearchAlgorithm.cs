using DotnetGraph.Helper;
using DotnetGraph.Model.Enums;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch
{
    public class CormenDepthFirstSearchAlgorithm : IDepthFirstSearchAlgorithm
    {
        public static void ValidateInput<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            GraphValidation.ValidateUniqueIds(nodes);
            GraphValidation.ValidateUniqueArcIds<TNode, TArc>(nodes);
        }

        private int time;

        public void Run(IReadOnlyCollection<CormenDepthFirstSearchNode> nodes)
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

        public DepthFirstSearchResult Run<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasId, IHasDiscoverInformation, IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            throw new NotImplementedException();
        }
    }
}