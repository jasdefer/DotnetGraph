using System.IO;
using System.Text;

namespace DotnetGraph.Helper;

public static class PrintGraph
{
    public static void PrintDirectedGraph<TNode, TArc>(string path, IEnumerable<TNode> nodes)
        where TNode : IHasId, IHasOutgoingArcs<TArc>
        where TArc : IHasDestination<TNode>
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
                sb.AppendLine($"{node.Id}\t{arc.Destination.Id}");
            }
        }
        File.WriteAllText(path, sb.ToString());
    }

    public static void PrintWeightedDirectedGraph<TNode, TArc>(string path, IEnumerable<TNode> nodes)
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

    public static void PrintFlowDirectedGraph<TNode, TArc>(string path, IEnumerable<TNode> nodes)
        where TNode : IHasId, IHasOutgoingArcs<TArc>
        where TArc : IHasCapacity, IHasFlow, IHasDestination<TNode>
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
                sb.AppendLine($"{node.Id}\t{arc.Destination.Id}\t{arc.Flow}\t{arc.Capacity}");
            }
        }
        File.WriteAllText(path, sb.ToString());
    }
}
