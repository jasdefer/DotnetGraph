using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    public class LineWeightedDirectedGraphGenerator : IWeightedDirectedGraphGenerator
    {
        public WeightedDirectedGraphNode[] Generate(int numberOfNodes, double density, INumberGenerator weightGenerator)
        {
            if (weightGenerator is null)
            {
                throw new ArgumentNullException(nameof(weightGenerator));
            }

            var lineGraphGenerator = new LineGraphGenerator();
            var nodes = lineGraphGenerator.Generate(numberOfNodes, Math.Min(1, 2 * density));
            var dict = nodes.ToDictionary(
                x => x.Id,
                x => new WeightedDirectedGraphNode(x.Id));
            for (int i = 0; i < nodes.Length; i++)
            {
                var origin = dict[nodes[i].Id];
                foreach (var edge in nodes[i].Edges)
                {
                    if (edge.Node2.Id > origin.Id)
                    {
                        var weight = weightGenerator.Generate();
                        var flowDirectedGraphArc = new WeightedDirectedGraphArc(edge.Id, weight, dict[edge.Node2.Id]);
                        origin.Add(flowDirectedGraphArc);
                    }
                }
            }
            return dict.Values.ToArray();
        }
    }
}