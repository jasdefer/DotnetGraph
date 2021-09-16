using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.CornerFlow;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    public class CornerFlowFlowDirectedGraphGenerator : IFlowDirectedGraphGenerator
    {
        public FlowDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator capacityGenerator)
        {
            var flowGenerator = new CornerFlowAlgorithm();
            var nodes = flowGenerator.Generate(numberOfNodes, density, capacityGenerator);
            var dict = nodes.ToDictionary(
                x => x.Id,
                x => new FlowDirectedGraphNode(x.Id, new List<FlowDirectedGraphArc>()));
            for (int i = 0; i < nodes.Length; i++)
            {
                var origin = dict[nodes[i].Id];
                foreach (var arc in nodes[i].OutgoingArcs)
                {
                    var flowDirectedGraphArc = new FlowDirectedGraphArc(arc.Id, arc.Weight, dict[arc.Destination.Id]);
                    origin.Add(flowDirectedGraphArc);
                }
            }
            return dict.Values.ToArray();
        }
    }
}