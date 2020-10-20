using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotnetGraphTest.Helper
{
    [TestClass]
    public class GraphValidationTests
    {
        [TestMethod]
        public void ValidateUniqueIdsValid()
        {
            var entites = CreateEntities(2);
            GraphValidation.ValidateUniqueIds(entites);
        }

        [TestMethod]
        public void ValidateUniqueIdsInvalid()
        {
            var entites = new Entity[2]
            {
                new Entity(1),
                new Entity(1)
            };
            Assert.ThrowsException<Exception>(() => GraphValidation.ValidateUniqueIds(entites));
        }

        [TestMethod]
        public void ValidateUniqueArcIdsValid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3),
            };

            nodes[0].AddArc(new DirectedGraphArc(1, nodes[1]));
            nodes[0].AddArc(new DirectedGraphArc(2, nodes[2]));
            nodes[1].AddArc(new DirectedGraphArc(3, nodes[2]));
            GraphValidation.ValidateUniqueArcIds(nodes);
        }

        [TestMethod]
        public void ValidateUniqueArcIdsInvalid()
        {
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3),
            };

            nodes[0].AddArc(new DirectedGraphArc(1, nodes[1]));
            nodes[0].AddArc(new DirectedGraphArc(2, nodes[2]));
            nodes[1].AddArc(new DirectedGraphArc(2, nodes[2]));
            Assert.ThrowsException<Exception>(() => GraphValidation.ValidateUniqueArcIds(nodes));
        }

        [TestMethod]
        public void IdExistsValid()
        {
            var entites = CreateEntities(4);
            GraphValidation.IdExists(entites, 1, 4, 2);
        }

        [TestMethod]
        public void IdExistsInvalid()
        {
            var entites = CreateEntities(4);
            Assert.ThrowsException<Exception>(() => GraphValidation.IdExists(entites, 1, 5, 2));
        }

        [TestMethod]
        public void ValidateOnlyPositiveWeightsValid()
        {
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
            };

            nodes[0].AddArc(new WeightedDirectedGraphArc(1, nodes[1], 0));
            nodes[0].AddArc(new WeightedDirectedGraphArc(2, nodes[1], double.MaxValue));
            GraphValidation.ValidateOnlyPositiveWeights<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes);
        }

        [TestMethod]
        public void ValidateOnlyPositiveWeightsInvalid()
        {
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2)
            };

            nodes[0].AddArc(new WeightedDirectedGraphArc(1, nodes[1], 1));
            nodes[0].AddArc(new WeightedDirectedGraphArc(2, nodes[1], -1));
            Assert.ThrowsException<Exception>(() => GraphValidation.ValidateOnlyPositiveWeights<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes));
        }

        private static Entity[] CreateEntities(int numberOfEntities)
        {
            var entities = new Entity[numberOfEntities];
            for (int i = 0; i < entities.Length; i++)
            {
                entities[i] = new Entity(i + 1);
            }
            return entities;
        }
    }
}