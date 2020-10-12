namespace DotnetGraph.Model.Properties
{
    public interface IHasDestination<TNode>
    {
        TNode Destination { get; }
    }
}