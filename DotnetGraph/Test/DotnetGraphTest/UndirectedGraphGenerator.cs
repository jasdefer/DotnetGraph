using DotnetGraph.Model;
using System.Collections.Generic;

namespace DotnetGraphTest
{
    public static class UndirectedGraphGenerator
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
        public static Edge<string>[] GetSmallGraph()
        {
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A", "B", 5),
                new Edge<string>("A", "D", 1),
                new Edge<string>("B", "C", 5),
                new Edge<string>("B", "E", 2),
                new Edge<string>("C", "F", 1),
                new Edge<string>("D", "E", 1),
                new Edge<string>("E", "F", 1),
            };
            return edges.ToArray();
        }

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
        /// +-+  -1  +-+   1  +-+
        /// </summary>
        public static Edge<string>[] GetSmallGraphWithNegativeEdge()
        {
            var edges = new List<Edge<string>>()
            {
                new Edge<string>("A", "B", 5),
                new Edge<string>("A", "D", 1),
                new Edge<string>("B", "C", 5),
                new Edge<string>("B", "E", 2),
                new Edge<string>("C", "F", 1),
                new Edge<string>("D", "E", -1),
                new Edge<string>("E", "F", 1),
            };
            return edges.ToArray();
        }
    }
}