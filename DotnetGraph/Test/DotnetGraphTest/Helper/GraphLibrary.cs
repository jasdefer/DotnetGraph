using DotnetGraph.Helper;
using DotnetGraph.Model.Implementations;
using DotnetGraph.Model.Implementations.Graph.DirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedDirectedGraph;
using DotnetGraph.Model.Implementations.Graph.WeightedUndirectedGraph;

namespace DotnetGraphTest.Helper
{
    public static class GraphLibrary
    {
        public static WeightedDirectedGraphNode[] SmallWeightedDirectedGraph()
        {
            var nodes = new WeightedDirectedGraphNode[]
            {
                new WeightedDirectedGraphNode(1),
                new WeightedDirectedGraphNode(2),
                new WeightedDirectedGraphNode(3),
                new WeightedDirectedGraphNode(4),
                new WeightedDirectedGraphNode(5),
                new WeightedDirectedGraphNode(6)
            };

            nodes[0].Add(new WeightedDirectedGraphArc(1, 5, nodes[1]));
            nodes[1].Add(new WeightedDirectedGraphArc(2, 5, nodes[0]));

            nodes[0].Add(new WeightedDirectedGraphArc(3,1, nodes[3]));
            nodes[3].Add(new WeightedDirectedGraphArc(4,1, nodes[0]));

            nodes[1].Add(new WeightedDirectedGraphArc(5,3, nodes[2]));
            nodes[2].Add(new WeightedDirectedGraphArc(6,3, nodes[1]));

            nodes[1].Add(new WeightedDirectedGraphArc(7,2, nodes[4]));
            nodes[4].Add(new WeightedDirectedGraphArc(8,2, nodes[1]));

            nodes[2].Add(new WeightedDirectedGraphArc(9,1, nodes[5]));
            nodes[5].Add(new WeightedDirectedGraphArc(10,1, nodes[2]));

            nodes[3].Add(new WeightedDirectedGraphArc(11,1, nodes[4]));
            nodes[4].Add(new WeightedDirectedGraphArc(12,1, nodes[3]));

            nodes[4].Add(new WeightedDirectedGraphArc(13,1, nodes[5]));
            nodes[5].Add(new WeightedDirectedGraphArc(14,1, nodes[4]));
            return nodes;
        }

        public static WeightedDirectedGraphNode[] SmallComplicatedWeightedDirectedGraph()
        {
            var arcs = new ArcData[]
            {
                new ArcData(1,2,55),
                new ArcData(1,3,15),
                new ArcData(2,5,16),
                new ArcData(4,2,11),
                new ArcData(3,4,12),
                new ArcData(3,6,42),
                new ArcData(4,5,30),
                new ArcData(4,7,40),
                new ArcData(4,6,16),
                new ArcData(6,9,62),
                new ArcData(5,8,45),
                new ArcData(5,7,11),
                new ArcData(7,8,17),
                new ArcData(7,9,31),
                new ArcData(8,9,13)
            };

            var nodes = GraphConverter.GetNodes(arcs);

            return nodes;
        }

        public static DirectedGraphNode[] SmallDirectedGraph()
        {
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

            nodes[0].Add(new DirectedGraphArc(1, nodes[1]));
            nodes[1].Add(new DirectedGraphArc(2, nodes[4]));
            nodes[4].Add(new DirectedGraphArc(3, nodes[0]));
            nodes[1].Add(new DirectedGraphArc(4, nodes[5]));
            nodes[4].Add(new DirectedGraphArc(5, nodes[5]));
            nodes[1].Add(new DirectedGraphArc(6, nodes[2]));
            nodes[2].Add(new DirectedGraphArc(7, nodes[3]));
            nodes[3].Add(new DirectedGraphArc(8, nodes[2]));
            nodes[2].Add(new DirectedGraphArc(9, nodes[6]));
            nodes[3].Add(new DirectedGraphArc(10, nodes[7]));
            nodes[7].Add(new DirectedGraphArc(11, nodes[3]));
            nodes[7].Add(new DirectedGraphArc(12, nodes[6]));
            nodes[5].Add(new DirectedGraphArc(13, nodes[6]));
            nodes[6].Add(new DirectedGraphArc(14, nodes[5]));

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
                edges[i].Node1.Add(edges[i]);
                edges[i].Node2.Add(edges[i]);
            }
            return nodes;
        }
    }
}