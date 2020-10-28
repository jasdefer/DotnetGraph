namespace DotnetGraph.Model.Properties
{
    public interface IConnectsNodes<out TNode>
    {
        TNode Node1 { get; }
        TNode Node2 { get; }
    }
}