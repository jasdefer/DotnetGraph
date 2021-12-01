namespace DotnetGraph.Model.Properties
{
    public interface IHasOutgoingArcs<out TArc>
    {
        IReadOnlyCollection<TArc> OutgoingArcs { get; }
    }
}