using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace DotnetGraphTest.Helper
{
    public static class GraphLibrary
    {
        public static WeightedDirectedGraphNode[] SmallWeightedDirectedGraph()
        {
            //Data preparation
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2),
                new WeightedDirectedGraphNode(3),
                new WeightedDirectedGraphNode(4),
                new WeightedDirectedGraphNode(5),
                new WeightedDirectedGraphNode(6)
            };

            nodes[0].AddArc(new WeightedDirectedGraphArc(1, nodes[1], 5));
            nodes[1].AddArc(new WeightedDirectedGraphArc(2, nodes[0], 5));

            nodes[0].AddArc(new WeightedDirectedGraphArc(3, nodes[3], 1));
            nodes[3].AddArc(new WeightedDirectedGraphArc(4, nodes[0], 1));

            nodes[1].AddArc(new WeightedDirectedGraphArc(5, nodes[2], 3));
            nodes[2].AddArc(new WeightedDirectedGraphArc(6, nodes[1], 3));

            nodes[1].AddArc(new WeightedDirectedGraphArc(7, nodes[4], 2));
            nodes[4].AddArc(new WeightedDirectedGraphArc(8, nodes[1], 2));

            nodes[2].AddArc(new WeightedDirectedGraphArc(9, nodes[5], 1));
            nodes[5].AddArc(new WeightedDirectedGraphArc(10, nodes[2], 1));

            nodes[3].AddArc(new WeightedDirectedGraphArc(11, nodes[4], 1));
            nodes[4].AddArc(new WeightedDirectedGraphArc(12, nodes[3], 1));

            nodes[4].AddArc(new WeightedDirectedGraphArc(13, nodes[5], 1));
            nodes[5].AddArc(new WeightedDirectedGraphArc(14, nodes[4], 1));
            return nodes;
        }

        public static DirectedGraphNode[] SmallDirectedGraph()
        {
            //Data preparation
            var nodes = new DirectedGraphNode[]
            {
                new DirectedGraphNode(1),
                new DirectedGraphNode(2),
                new DirectedGraphNode(3),
                new DirectedGraphNode(4),
                new DirectedGraphNode(5),
                new DirectedGraphNode(6),
                new DirectedGraphNode(7),
                new DirectedGraphNode(8),
            };

            nodes[0].AddArc(new DirectedGraphArc(1, nodes[1]));
            nodes[1].AddArc(new DirectedGraphArc(2, nodes[4]));
            nodes[4].AddArc(new DirectedGraphArc(3, nodes[0]));
            nodes[1].AddArc(new DirectedGraphArc(4, nodes[5]));
            nodes[4].AddArc(new DirectedGraphArc(5, nodes[5]));
            nodes[1].AddArc(new DirectedGraphArc(6, nodes[2]));
            nodes[2].AddArc(new DirectedGraphArc(7, nodes[3]));
            nodes[3].AddArc(new DirectedGraphArc(8, nodes[2]));
            nodes[2].AddArc(new DirectedGraphArc(9, nodes[6]));
            nodes[3].AddArc(new DirectedGraphArc(10, nodes[7]));
            nodes[7].AddArc(new DirectedGraphArc(11, nodes[3]));
            nodes[7].AddArc(new DirectedGraphArc(12, nodes[6]));
            nodes[5].AddArc(new DirectedGraphArc(13, nodes[6]));
            nodes[6].AddArc(new DirectedGraphArc(14, nodes[5]));

            return nodes;
        }

        public static WeightedUndirectedGraphNode[] SmallWeightedUndirectedGraph()
        {
            var nodes = new WeightedUndirectedGraphNode[7];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new WeightedUndirectedGraphNode(i + 1);
            }
            var edges = new WeightedUndirectedGraphEdge[]
            {
                new WeightedUndirectedGraphEdge(1, 7, nodes[0], nodes[1]),
                new WeightedUndirectedGraphEdge(2, 5, nodes[0], nodes[3]),
                new WeightedUndirectedGraphEdge(3, 8, nodes[1], nodes[2]),
                new WeightedUndirectedGraphEdge(4, 9, nodes[1], nodes[3]),
                new WeightedUndirectedGraphEdge(5, 7, nodes[1], nodes[4]),
                new WeightedUndirectedGraphEdge(6, 5, nodes[2], nodes[4]),
                new WeightedUndirectedGraphEdge(7, 15, nodes[3], nodes[4]),
                new WeightedUndirectedGraphEdge(8, 6, nodes[3], nodes[5]),
                new WeightedUndirectedGraphEdge(9, 8, nodes[4], nodes[5]),
                new WeightedUndirectedGraphEdge(10, 9, nodes[4], nodes[6]),
                new WeightedUndirectedGraphEdge(11, 11, nodes[5],nodes[6])
            };

            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].Node1.AddEdge(edges[i]);
                edges[i].Node2.AddEdge(edges[i]);
            }
            return nodes;
        }
    }
}