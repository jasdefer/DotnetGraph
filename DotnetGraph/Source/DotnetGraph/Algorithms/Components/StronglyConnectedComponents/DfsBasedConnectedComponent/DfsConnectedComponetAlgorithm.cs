using DotnetGraph.Algorithms.DepthFirstSearch;
using DotnetGraph.Algorithms.DepthFirstSearch.CormenDfs;
using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.DfsBasedConnectedComponent
{
    public class DfsConnectedComponetAlgorithm : IGetStronglyConnectedComponents
    {
        private static CultureInfo _ci = new CultureInfo("en-US");

        public StronglyConnectedComponentsResult<TNode> GetCompontents<TNode, TArc>(IEnumerable<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {

            DfSearchNode[] dfsNodes = Convert<TNode, TArc>(nodes);
            CormenDfsAlgorithm a = new CormenDfsAlgorithm();
            a.Run(dfsNodes);
            DfSearchNode[] orderedNodes = dfsNodes.OrderByDescending(n => n.FinishedAt).ToArray();//müsste man besser ohne kopieren hinkriegen
            a.Run(orderedNodes);


        }



        private static DfSearchNode[] Convert<TNode, TArc>(IEnumerable<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>, IHasId
            where TArc : IHasDestination<TNode>, IHasId
        {
            //TArc ist IHasDestination, aber DfSearch hat nur IConnectsNodes <- daher kann man nicht einfach DfSearchNodes benutzen
            
            Dictionary<int, DfSearchNode> nodeIdx = new Dictionary<int, DfSearchNode>(nodes.Count());
            
            
            foreach (var nodeInterface in nodes)
            {
                DfSearchNode n = GetNode(nodeInterface.Id, nodeIdx);
                foreach (var e in nodeInterface.OutgoingArcs)
                {
                    DfSearchNode target = GetNode(e.Destination.Id, nodeIdx);
                    n.LinkToNode(target);
                }
            }
            return nodeIdx.Values.ToArray();
        }

        private static DfSearchNode GetNode(int id, Dictionary<int, DfSearchNode> nlu)
        {
            if (!nlu.ContainsKey(id))
                nlu[id] = new DfSearchNode(id.ToString(_ci));
            return nlu[id];
        }

        
    }
}
