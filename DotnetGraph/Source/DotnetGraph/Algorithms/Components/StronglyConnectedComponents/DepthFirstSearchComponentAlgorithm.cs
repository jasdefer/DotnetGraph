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
            CormenDepthFirstSearchNode[] orderedNodes = nodes
                .OrderByDescending(n => n.ExploredTime)
                .ToArray();
            algorithm.Run(orderedNodes);
            var result = GetResult(nodes);
            return result;
        }

        private static StronglyConnectedComponentsResult<CormenDepthFirstSearchNode> GetResult(IReadOnlyList<CormenDepthFirstSearchNode> nodes)
        {
            var list = new List<CormenDepthFirstSearchNode>(nodes);
            var components = new List<List<CormenDepthFirstSearchNode>>();
            while (list.Count > 0)
            {
                var component = new List<CormenDepthFirstSearchNode>();
                var node = list[^1];
                do
                {
                    list.Remove(node);
                    component.Add(node);
                    node = node.PredecessorNode;
                }
                while (node != null);
                components.Add(component);
            }
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