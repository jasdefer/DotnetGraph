using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotnetGraph.Helper
{
    public static class PrintGraph
    {
        public static void Print<TNode, TArc>(string path, IEnumerable<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasWeight, IHasDestination<TNode>
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var sb = new StringBuilder();

            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    sb.AppendLine($"{node.Id}\t{arc.Destination.Id}\t{arc.Weight}");
                }
            }
            File.WriteAllText(path, sb.ToString());
        }
    }
}