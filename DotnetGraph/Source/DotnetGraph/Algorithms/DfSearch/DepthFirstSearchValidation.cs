using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotnetGraph.Algorithms.DfSearch
{
    public static class DepthFirstSearchValidation
    {
        public static bool IsValid(IEnumerable<DfSearchNode> nodes, out string errors)
        {
            StringBuilder b = new StringBuilder();
            bool valid = true;
            if (nodes == null || !nodes.Any())
            {
                b.AppendLine("Nodecollection is empty.");
                valid = false;
            }
            else
            {
                foreach (DfSearchNode node in nodes)
                {
                    if (node.OutgoingArcs == null)
                    {
                        b.AppendLine($"Node {node.Label} has no Arc Collection defined.");
                        valid = false;
                    }
                    else
                    {
                        foreach (DfSearchArc arc in node.OutgoingArcs)
                        {
                            if (arc.Node1 == null || arc.Node2 == null)
                            {
                                b.AppendLine($"Node {node.Label} has invald arcs in its arc collection.");
                                valid = false;
                            }
                        }
                    }
                }
            }
            errors = b.ToString();
            return valid;
        }
    }
}
