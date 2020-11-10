using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.DfSearch
{
    public class DepthFirstSearch
    {
        private int _time;

        public void Run(IEnumerable<DfSearchNode> nodes)
        {
            if (nodes == null || !nodes.Any())
                throw new ArgumentException($"{nameof(nodes)} is null or empty.");

            Initialize(nodes);
            foreach (DfSearchNode n in nodes)
            {
                if(n.SearchState == DfSearchState.Undiscovered)
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
                if(adjacentNode.SearchState == DfSearchState.Undiscovered)
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

    }
}
