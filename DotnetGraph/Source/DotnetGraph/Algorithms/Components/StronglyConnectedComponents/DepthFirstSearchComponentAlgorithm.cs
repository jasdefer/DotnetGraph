using DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents
{
    public class DepthFirstSearchComponentAlgorithm : IGetStronglyConnectedComponents
    {
        public StronglyConnectedComponentsResult<TNode> GetCompontents<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            CormenDepthFirstSearchNode[] dfsNodes = Convert<TNode, TArc>(nodes);
            var rawResult = GetComponents(dfsNodes);
            var result = ConvertResult<TNode, TArc>(nodes, rawResult);
            return result;
        }

        private static StronglyConnectedComponentsResult<CormenDepthFirstSearchNode> GetComponents(IReadOnlyList<CormenDepthFirstSearchNode> nodes)
        {
            CormenDepthFirstSearchAlgorithm algorithm = new();
            algorithm.Run(nodes);

            var orderedNodes = nodes
                .OrderByDescending(n => n.ExploredTime)
                .ToArray();
            orderedNodes = Invert(orderedNodes);
            algorithm.Run(orderedNodes);
            var result = GetResult(orderedNodes);
            return result;
        }

        private static CormenDepthFirstSearchNode[] Invert(IReadOnlyList<CormenDepthFirstSearchNode> nodes)
        {
            var invertedNodes = new Dictionary<int, CormenDepthFirstSearchNode>(nodes.Count);
            for (int i = 0; i < nodes.Count; i++)
            {
                var invertedNode = AddOrGetNode(invertedNodes, nodes[i].Id);

                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var destination = AddOrGetNode(invertedNodes, arc.Destination.Id);
                    var invertedArc = new CormenDepthFirstSearchArc(arc.Id, destination, invertedNode);
                    destination.AddArc(invertedArc);
                }
            }
            return invertedNodes.Values.ToArray();
        }

        private static CormenDepthFirstSearchNode AddOrGetNode(Dictionary<int, CormenDepthFirstSearchNode> dict, int id)
        {
            if (!dict.ContainsKey(id))
            {
                var node = new CormenDepthFirstSearchNode(id);
                dict.Add(id, node);
                return node;
            }
            return dict[id];
        }

        private static StronglyConnectedComponentsResult<CormenDepthFirstSearchNode> GetResult(IReadOnlyList<CormenDepthFirstSearchNode> nodes)
        {
            var components = new List<List<CormenDepthFirstSearchNode>>();
            
            var result = new StronglyConnectedComponentsResult<CormenDepthFirstSearchNode>(components);
            return result;
        }

        private static CormenDepthFirstSearchNode[] Convert<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
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
                    var dijkstraArc = new CormenDepthFirstSearchArc(arc.Id, origin, dict[arc.Destination.Id]);
                    origin.AddArc(dijkstraArc);
                }
            }
            return dict.Values.ToArray();
        }

        private static StronglyConnectedComponentsResult<TNode> ConvertResult<TNode, TArc>(IReadOnlyList<TNode> nodes, StronglyConnectedComponentsResult<CormenDepthFirstSearchNode> rawResult)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            var components = new TNode[rawResult.NumberOfComponents][];
            var componentIndex = 0;
            var dict = nodes.ToDictionary(x => x.Id, x => x);
            foreach (var rawComponent in rawResult.Components)
            {
                var nodeIndex = 0;
                components[componentIndex] = new TNode[rawComponent.Count];
                foreach (var rawNode in rawComponent)
                {
                    var node = dict[rawNode.Id];
                    components[componentIndex][nodeIndex] = node;
                    nodeIndex++;
                }
                componentIndex++;
            }
            var result = new StronglyConnectedComponentsResult<TNode>(components);
            return result;
        }
    }
}