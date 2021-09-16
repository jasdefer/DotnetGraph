using DotnetGraph.Algorithms.GraphGeneration.FlowDirectedGraphGeneration;
using DotnetGraph.Algorithms.GraphGeneration.Misc.NumberGenerator;
using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations.Graph.FlowDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DotnetGraphTest.Algorithms.GraphGeneration.FlowDirectedGraphGeneration
{
    [TestClass]
    public abstract class FlowDirectedGraphGenerationTests
    {
        protected abstract IFlowDirectedGraphGenerator GetGenerator();

        [TestMethod]
        public void Monkey()
        {
            // Arrange
            var generator = GetGenerator();
            var capacityGenerator = new Iterator();

            for (int i = 0; i < 20; i++)
            {
                // Act
                var numberOfNodes = (i + 1) * 10;
                var density = 2.5 / (numberOfNodes);
                var nodes = generator.Generate(numberOfNodes, density, capacityGenerator);
                PrintGraph.PrintFlowDirectedGraph<FlowDirectedGraphNode, FlowDirectedGraphArc>($"i_{i}.csv", nodes);
                // Assert
                Assert.IsNotNull(nodes);
                Assert.AreEqual(numberOfNodes, nodes.Length);
                var numberOfArcs = nodes.Sum(x => x.OutgoingArcs.Count);
                var numberOfExpectedArcs = numberOfNodes * numberOfNodes * density;
                var delta = Math.Max(3, numberOfExpectedArcs * 0.2);
                Assert.AreEqual(numberOfExpectedArcs, numberOfArcs, delta);
            }
        }
    }
}