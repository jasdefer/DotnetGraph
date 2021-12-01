namespace DotnetGraph.Model.Properties;

public interface IHasDestination<out TNode>
{
    TNode Destination { get; }
}
