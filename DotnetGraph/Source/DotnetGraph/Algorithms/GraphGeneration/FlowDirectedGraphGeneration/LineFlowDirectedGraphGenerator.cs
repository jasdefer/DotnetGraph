using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    public class LineFlowDirectedGraphGenerator : IFlowDirectedGraphGenerator
    {
        public FlowDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator capacityGenerator)
        {
            if (capacityGenerator is null)
            {
                throw new ArgumentNullException(nameof(capacityGenerator));
            }

            var lineGraphGenerator = new LineGraphGenerator();
            var nodes = lineGraphGenerator.Generate(numberOfNodes, Math.Min(1, 2 * density));
            var dict = nodes.ToDictionary(
                x => x.Id,
                x => new FlowDirectedGraphNode(x.Id, new List<FlowDirectedGraphArc>()));
            for (int i = 0; i < nodes.Length; i++)
            {
                var origin = dict[nodes[i].Id];
                foreach (var edge in nodes[i].Edges)
                {
                    if (edge.Node2.Id > origin.Id)
                    {
                        var capacity = capacityGenerator.Generate();
                        var flowDirectedGraphArc = new FlowDirectedGraphArc(edge.Id, capacity, dict[edge.Node2.Id]);
                        origin.Add(flowDirectedGraphArc);
                    }

                }
            }
            return dict.Values.ToArray();
        }
    }
}