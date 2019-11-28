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
        public static Arc<string>[] GetSmallGraph()
        {
            var arcs = new List<Arc<string>>()
            {
                new Arc<string>("A", "B", 5),
                new Arc<string>("A", "D", 1),
                new Arc<string>("B", "C", 5),
                new Arc<string>("B", "E", 2),
                new Arc<string>("C", "F", 1),
                new Arc<string>("D", "E", 1),
                new Arc<string>("E", "F", 1),
            };
            var arcCount = arcs.Count;
            for (int i = 0; i < arcCount; i++)
            {
                arcs.Add(arcs[i].Reverse());
            }
            return arcs.ToArray();
        }

        /// <summary>
        /// The graph contains 6 nodes and 7 edges resulting in 14 arcs, since each edge is represented by two arcs.
        /// 
        /// +-+  5   +-+   5  +-+
        /// |A+------+B+------+C|
        /// +++      +++      +++
        ///  |        |        |
        ///-1|       2|        |1
        ///  |        |        |
        /// +++      +++      +++
        /// |D+------+E+------+F|
        /// +-+  1   +-+   1  +-+
        /// </summary>
        public static Arc<string>[] GetSmallGraphWithNegativeArc()
        {
            var arcs = new List<Arc<string>>()
            {
                new Arc<string>("A", "B", 5),
                new Arc<string>("A", "D", -1),
                new Arc<string>("B", "C", 5),
                new Arc<string>("B", "E", 2),
                new Arc<string>("C", "F", 1),
                new Arc<string>("D", "E", 1),
                new Arc<string>("E", "F", 1),
            };
            var arcCount = arcs.Count;
            for (int i = 0; i < arcCount; i++)
            {
                if (arcs[i].Weight >= 0)
                {
                    arcs.Add(arcs[i].Reverse());
                }
            }
            return arcs.ToArray();
        }
    }
}