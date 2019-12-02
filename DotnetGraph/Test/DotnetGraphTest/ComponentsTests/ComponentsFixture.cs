using DotnetGraph.Algorithms.Contracts;
using DotnetGraph.Algorithms.GraphGeneration;
using DotnetGraph.Model;
using DotnetGraph.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace DotnetGraphTest.ComponentsTests
{
    [TestClass]
    public abstract class ComponentsFixture
    {
        protected abstract IComponentAlgorithm GetComponentAlgorithm();

        [TestMethod]
        public void Null()
        {
            var algorithm = GetComponentAlgorithm();
            var components = algorithm.GetComponents<int>(null);
            Assert.IsNotNull(components);
            Assert.AreEqual(0, components.Length);
        }

        [TestMethod]
        public void Empty()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = new List<Edge<int>>();
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(0, components.Length);
        }

        [TestMethod]
        public void SingleEdge()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A","B", 1)
            };
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(1, components.Length);
            Assert.AreEqual(2, components[0].Length);
            Assert.IsTrue(components[0].Contains("A"));
            Assert.IsTrue(components[0].Contains("B"));
        }

        [TestMethod]
        public void TwoEdgesOneComponent()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A","B", 1),
                new Edge<string>("B","C", 1),
            };
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(1, components.Length);
            Assert.AreEqual(3, components[0].Length);
            Assert.IsTrue(components[0].Contains("A"));
            Assert.IsTrue(components[0].Contains("B"));
            Assert.IsTrue(components[0].Contains("C"));
        }

        [TestMethod]
        public void TwoEdgesTwoComponents()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A","B", 1),
                new Edge<string>("C","D", 1),
            };
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(2, components.Length);
            Assert.AreEqual(2, components[0].Length);
            Assert.AreEqual(2, components[1].Length);
        }

        [TestMethod]
        public void SmallGraph()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = UndirectedGraphGenerator.GetSmallGraph();
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(1, components.Length);
            Assert.AreEqual(6, components[0].Length);
        }

        [TestMethod]
        public void TwoComponents()
        {
            var algorithm = GetComponentAlgorithm();
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A","B", 1),
                new Edge<string>("B","C", 1),
                new Edge<string>("C","D", 1),
                new Edge<string>("E","F", 1),
                new Edge<string>("F","B", 1),
                new Edge<string>("D","B", 1),
                new Edge<string>("X","Y", 1),
            };
            var components = algorithm.GetComponents(edges);
            Assert.IsNotNull(components);
            Assert.AreEqual(2, components.Length);
            Assert.AreEqual(8, components[0].Length + components[1].Length);
        }

        [TestMethod]
        public void MonkeyTest()
        {
            var generator = new ErdosRenyi();
            for (int i = 0; i < 1000; i++)
            {
                var rnd = new Random(i);
                generator.Random = rnd;
                var nodes = Enumerable.Range(0, rnd.Next(10, 200)).ToArray();
                generator.P = rnd.NextDouble();
                var graph = generator.GenerateGraph(nodes);
                nodes = graph.ExtractNodes();
                var components = generator.ComponentAlgorithm.GetComponents(graph);
                Assert.IsNotNull(components, $"Iteration {i} returns null.");
                Assert.AreEqual(1, components.Length, $"Iteration {i} returns not exactly one component.");
                Assert.AreEqual(nodes.Length, components[0].Length, $"Iteration {i} returns a single component with {components[0].Length} nodes instead of {nodes.Length}.");
            }
        }
    }
}