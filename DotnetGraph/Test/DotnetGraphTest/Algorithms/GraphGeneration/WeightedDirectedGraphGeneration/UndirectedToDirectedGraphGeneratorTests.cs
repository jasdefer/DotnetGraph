using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration.UndirectedToDirectedGraph;
using DotnetGraph.Algorithms.GraphGeneration.WeightedUndirectedGraphGeneration.LineGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.WeightedDirectedGraphGeneration
{
    [TestClass]
    public class UndirectedToDirectedGraphGeneratorTests : WeightedDirectedGraphGenerationTests
    {
        protected override IWeightedDirectedGraphGenerator GetGenerator()
        {
            return new UndirectedToDirectedGraphGenerator();
        }

        [TestMethod]
        public void TwoNodes()
        {
            var nodes = new WeightedUndirectedGraphNode[2];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedUndirectedGraphNode(i + 1);
            }
            var undirectedEdges = new WeightedUndirectedGraphEdge[]
            {
                new WeightedUndirectedGraphEdge(1, 1, nodes[0], nodes[1])
            };
            for (int i = 0; i < undirectedEdges.Length; i++)
            {
                undirectedEdges[i].Node1.Add(undirectedEdges[i]);
                undirectedEdges[i].Node2.Add(undirectedEdges[i]);
            }

            var directedNodes = UndirectedToDirectedGraphGenerator.Convert(nodes);
            Assert.IsNotNull(directedNodes);
            Assert.AreEqual(2, directedNodes.Length);
            Assert.AreEqual(1, directedNodes[0].OutgoingArcs.Count);
            Assert.AreEqual(1, directedNodes[1].OutgoingArcs.Count);
            Assert.AreNotEqual(directedNodes[0].OutgoingArcs.Single().Id, directedNodes[1].OutgoingArcs.Single().Id);
        }

        [TestMethod]
        public void NumberOfArcsMonkey()
        {
            var generator = GetGenerator();
            var weightGenerator = new UniformNumberGenerator();
            var undirectedEdgeGenerator = new LineGraphGenerator();
            for (int i = 0; i < 10; i++)
            {
                var numberOfNodes = 10 + 100 * i;
                var density = 2.5 / numberOfNodes;
                var undirectedNodes = undirectedEdgeGenerator.Generate(numberOfNodes, density, weightGenerator);
                var directedNodes = UndirectedToDirectedGraphGenerator.Convert(undirectedNodes);
                var numberOfEdges = undirectedNodes.SelectMany(x => x.Edges.Select(y => y.Id)).Distinct().Count();
                var numberofArcs = directedNodes.SelectMany(x => x.OutgoingArcs.Select(y => y.Id)).Distinct().Count();
                Assert.AreEqual(2 * numberOfEdges, numberofArcs);
            }
        }
    }
}