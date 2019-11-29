namespace DotnetGraph.Model
{
    /// <summary>
    /// An edge is an undirected connection between two nodes.
    /// </summary>
    /// <typeparam name="T">The type of the nodes</typeparam>
    public class Edge<T>
    {
        public T Node1 { get; }
        public T Node2 { get; }
        public double Weight { get; }

        public Edge(T node1, T node2, double weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }

        /// <summary>
        /// Converts the edge in two directed arcs.
        /// Both arcs have the same weight as this edge.
        /// </summary>
        /// <returns>The first arc leads from <para>Node1</para> to <para>Node2</para> and the second arc the other way round.</returns>
        public (Arc<T> arc1, Arc<T> arc2) ToArcs()
        {
            var arc1 = new Arc<T>(Node1, Node2, Weight);
            var arc2 = new Arc<T>(Node2, Node1, Weight);
            return (arc1, arc2);
        }

        public override string ToString()
        {
            return $"{Node1}-{Node2} ({Weight})";
        }
    }
}