namespace DotnetGraph.Algorithms.Components.ConnectedComponents
{
    public class ConnectedComponentResult<TNode>
    {
        public ConnectedComponentResult(ReadOnlyCollection<ConnectedComponent<TNode>> components)
        {
            Components = components ?? throw new ArgumentNullException(nameof(components));
        }

        public ReadOnlyCollection<ConnectedComponent<TNode>> Components { get; set; }
        public int NumberOfComponents => Components.Count;
    }
}