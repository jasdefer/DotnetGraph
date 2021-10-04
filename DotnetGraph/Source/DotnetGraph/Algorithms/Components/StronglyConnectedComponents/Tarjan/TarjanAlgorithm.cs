using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan
{
    public class TarjanAlgorithm : IGetStronglyConnectedComponents
    {
        private int index;
        private List<TarjanNode> stack;
        private List<ReadOnlyCollection<TarjanNode>> components;

        public StronglyConnectedComponentsResult<TNode> GetCompontents<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            TarjanNode[] tarjanNodes = Convert<TNode, TArc>(nodes);
            var tarjanResult = GetComponents(tarjanNodes);
            var result = ConvertResult<TNode>(nodes, tarjanResult);
            return result;
        }

        private static StronglyConnectedComponentsResult<TNode> ConvertResult<TNode>(IReadOnlyList<TNode> nodes, StronglyConnectedComponentsResult<TarjanNode> tarjanResult)
            where TNode : IHasId
        {
            var dict = nodes.ToDictionary(x => x.Id);
            var components = new List<ReadOnlyCollection<TNode>>();
            foreach (var tarjanComponent in tarjanResult.Components)
            {
                var component = new List<TNode>();
                foreach (var tarjanNode in tarjanComponent)
                {
                    component.Add(dict[tarjanNode.Id]);
                }
                components.Add(component.AsReadOnly());
            }
            var result = new StronglyConnectedComponentsResult<TNode>(components.AsReadOnly());
            return result;
        }

        public static TarjanNode[] Convert<TNode, TArc>(IReadOnlyList<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var dict = nodes.ToDictionary(x => x.Id, x => new TarjanNode(x.Id));
            for (int i = 0; i < nodes.Count; i++)
            {
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var tarjanArc = new TarjanArc(1, dict[arc.Destination.Id]);
                    dict[nodes[i].Id].Add(tarjanArc);
                }
            }
            return dict.Values.ToArray();
        }

        public StronglyConnectedComponentsResult<TarjanNode> GetComponents(TarjanNode[] nodes)
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }
            index = 0;
            components = new List<ReadOnlyCollection<TarjanNode>>();
            stack = new List<TarjanNode>();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (!nodes[i].Index.HasValue)
                {
                    ConnectNode(nodes[i]);
                }
            }
            var result = new StronglyConnectedComponentsResult<TarjanNode>(components.AsReadOnly());
            return result;
        }

        private void ConnectNode(TarjanNode tarjanNode)
        {
            stack.Add(tarjanNode);
            index++;
            tarjanNode.Index = index;
            tarjanNode.LowLink = index;
            tarjanNode.IsOnStack = true;

            //Find the component recursively
            foreach (var arc in tarjanNode.OutgoingArcs)
            {
                if (!arc.Destination.Index.HasValue)
                {
                    ConnectNode(arc.Destination);
                    tarjanNode.LowLink = Math.Min(tarjanNode.LowLink, arc.Destination.LowLink);
                }
                else if (arc.Destination.IsOnStack)
                {
                    tarjanNode.LowLink = Math.Min(tarjanNode.LowLink, arc.Destination.Index.Value);
                }
            }

            //Build the component
            if (tarjanNode.LowLink == tarjanNode.Index &&
                stack.Count > 0)
            {
                var component = new List<TarjanNode>();
                TarjanNode node;
                do
                {
                    node = stack[^1];
                    node.IsOnStack = false;
                    stack.RemoveAt(stack.Count - 1);
                    component.Add(node);
                }
                while (node != tarjanNode);
                components.Add(component.AsReadOnly());
            }
        }
    }
}