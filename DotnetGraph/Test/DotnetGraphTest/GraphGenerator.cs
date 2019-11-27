using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraphTest
{
    public static class GraphGenerator
    {
        /// <summary>
        /// The graph contains 6 nodes and 7 edges resulting in 14 arcs, since each edge is represented by two arcs.
        /// 
        /// +-+  5   +-+   5  +-+
        /// |A+------+B+------+C|
        /// +++      +++      +++
        ///  |        |        |
        /// 1|       2|        |1
        ///  |        |        |
        /// +++      +++      +++
        /// |D+------+E+------+F|
        /// +-+  1   +-+   1  +-+
        /// </summary>
        public static Arc<Node>[] GetSmallGraph()
        {
            var nodes = new Node[]
            {
                new Node("A"),
                new Node("B"),
                new Node("C"),
                new Node("D"),
                new Node("E"),
                new Node("F")
            };
            var arcs = new List<Arc<Node>>()
            {
                new Arc<Node>(nodes[0], nodes[1], 5),
                new Arc<Node>(nodes[0], nodes[3], 1),
                new Arc<Node>(nodes[1], nodes[2], 5),
                new Arc<Node>(nodes[1], nodes[4], 2),
                new Arc<Node>(nodes[2], nodes[5], 1),
                new Arc<Node>(nodes[3], nodes[4], 1),
                new Arc<Node>(nodes[4], nodes[5], 1),
            };
            var arcCount = arcs.Count;
            for (int i = 0; i < arcCount; i++)
            {
                arcs.Add(arcs[i].Reverse());
            }
            return arcs.ToArray();
        }
    }
}