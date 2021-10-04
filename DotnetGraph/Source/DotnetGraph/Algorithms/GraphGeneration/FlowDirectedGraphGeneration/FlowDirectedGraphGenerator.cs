using DotnetGraph.Algorithms.GraphGeneration.DirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
using System;
using System.Linq;

namespace DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    public class FlowDirectedGraphGenerator : IFlowDirectedGraphGenerator
    {
        public IDirectedGraphGenerator DirectedGraphGenerator { get; set; } = new UndirectedToDirectedGraphGenerator();
        public FlowDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator capacityGenerator)
        {
            if (capacityGenerator is null)
            {
                throw new ArgumentNullException(nameof(capacityGenerator));
            }

            var directedGraphNodes = DirectedGraphGenerator.Generate(numberOfNodes, density);
            var nodes = new FlowDirectedGraphNode[directedGraphNodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new FlowDirectedGraphNode(directedGraphNodes[i].Id);
            }

            var dict = nodes.ToDictionary(x => x.Id, x => x);
            for (int i = 0; i < nodes.Length; i++)
            {
                foreach (var arc in directedGraphNodes[i].OutgoingArcs)
                {
                    var capacity = capacityGenerator.Generate();
                    var destination = dict[arc.Destination.Id];
                    var weightedArc = new FlowDirectedGraphArc(arc.Id, capacity, destination);
                    nodes[i].Add(weightedArc);
                }
            }
            return nodes;
        }
    }
}