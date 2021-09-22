using DotnetGraph.Algorithms.DepthFirstSearch.CormenDepthFirstSearch;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.DfsBasedConnectedComponent
{
    public class DfsConnectedComponetAlgorithm : IGetStronglyConnectedComponents
    {
        public StronglyConnectedComponentsResult<TNode> GetCompontents<TNode, TArc>(IEnumerable<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            CormenDepthFirstSearchNode[] dfsNodes = Convert<TNode, TArc>(nodes);
            CormenDepthFirstSearchAlgorithm a = new CormenDepthFirstSearchAlgorithm();
            a.Run(dfsNodes);
            CormenDepthFirstSearchNode[] orderedNodes = dfsNodes.OrderByDescending(n => n.ExploredTime).ToArray();//müsste man besser ohne kopieren hinkriegen
            a.Run(orderedNodes);
            throw new NotImplementedException();
        }

        private static CormenDepthFirstSearchNode[] Convert<TNode, TArc>(IEnumerable<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            //TArc ist IHasDestination, aber DfSearch hat nur IConnectsNodes <- daher kann man nicht einfach DfSearchNodes benutzen

            Dictionary<int, CormenDepthFirstSearchNode> nodeIdx = new Dictionary<int, CormenDepthFirstSearchNode>(nodes.Count());

            //foreach (var nodeInterface in nodes)
            //{
            //    CormenDepthFirstSearchNode n = GetNode(nodeInterface.Id, nodeIdx);
            //    foreach (var e in nodeInterface.OutgoingArcs)
            //    {
            //        CormenDepthFirstSearchNode target = GetNode(e.Destination.Id, nodeIdx);
            //        n.CreateAndAddArc(target);
            //    }
            //}
            return nodeIdx.Values.ToArray();
        }

        private static CormenDepthFirstSearchNode GetNode(int id, Dictionary<int, CormenDepthFirstSearchNode> nlu)
        {
            if (!nlu.ContainsKey(id))
                nlu[id] = new CormenDepthFirstSearchNode(id);
            return nlu[id];
        }
    }
}