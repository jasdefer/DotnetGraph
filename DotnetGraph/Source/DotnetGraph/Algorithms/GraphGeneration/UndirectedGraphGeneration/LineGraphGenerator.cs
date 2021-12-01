using DotnetGraph.Model.Implementations.Graph.UndirectedGraph;

namespace DotnetGraph.Algorithms.GraphGeneration.UndirectedGraphGeneration
{
    /// <summary>
    /// Connect all nodes in a line, afterwards add random edges until the density is reached.
    /// </summary>
    public class LineGraphGenerator : IUndirectedGraphGenerator
    {
        public Random Random { get; set; } = new Random(1);
        public UndirectedGraphNode[] Generate(int numberOfNodes, double density)
        {
            var nodes = GenerateNodes(numberOfNodes);
            ConnectNodesInALine(nodes);
            var dict = GetPossibleNeighbors(nodes);
            CreateRandomEdges(nodes, dict, density);

            return nodes;
        }

        private void CreateRandomEdges(UndirectedGraphNode[] nodes,
            Dictionary<int, List<int>> dict,
            double density)
        {
            //Add random edges between random nodes until the required density (number of edges) is reached
            var numberOfEdges = GraphPropertyHelper.NumberOfEdges(nodes.Length, density);

            //Improves the performances of randomly selecting an Id from the dict above
            var keys = dict.Keys.ToList();

            //The Id of the edges start at this number, because there already exist some edges connecting all nodes in a line
            var startIndex = nodes.Length - 1;

            for (int i = startIndex; i < numberOfEdges; i++)
            {
                //Select two random node Ids from the dict
                var key = Random.Next(0, dict.Count);
                var node1Id = keys[key];
                var node2Id = dict[node1Id][Random.Next(0, dict[node1Id].Count)];

                //Create the edge
                var edge = new UndirectedGraphEdge(i + 1, nodes[node1Id - 1], nodes[node2Id - 1]);
                nodes[node1Id - 1].Add(edge);
                nodes[node2Id - 1].Add(edge);

                //Remove the connection from the dict
                dict[node1Id].Remove(node2Id);
                if (dict[node1Id].Count == 0)
                {
                    dict.Remove(node1Id);
                    keys.RemoveAt(key);
                }
            }
        }

        /// <summary>
        /// Key: Id of a node, Value: List of the Ids of possible neighbors of the key node
        /// </summary>
        private static Dictionary<int, List<int>> GetPossibleNeighbors(UndirectedGraphNode[] nodes)
        {
            var dict = new Dictionary<int, List<int>>();
            var baseList = Enumerable.Range(3, nodes.Length - 2).ToList();
            for (int i = 0; i < nodes.Length - 2; i++)
            {
                var baseListCopy = new List<int>(baseList);
                dict.Add(i + 1, baseListCopy);
                baseList.RemoveAt(0);
            }
            return dict;
        }

        /// <summary>
        /// Connect all nodes in a line: 1-2-3-...-n
        /// </summary>
        private static void ConnectNodesInALine(UndirectedGraphNode[] nodes)
        {
            var numberOfConnectingEdges = nodes.Length - 1;
            for (int i = 0; i < numberOfConnectingEdges; i++)
            {
                var edge = new UndirectedGraphEdge(i + 1, nodes[i], nodes[i + 1]);
                nodes[i].Add(edge);
                nodes[i + 1].Add(edge);
            }
        }

        private static UndirectedGraphNode[] GenerateNodes(int numberOfNodes)
        {
            var nodes = new UndirectedGraphNode[numberOfNodes];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new UndirectedGraphNode(i + 1);
            }
            return nodes;
        }
    }
}