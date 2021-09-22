namespace DotnetGraph.Model.Properties
{
    public interface IHasDiscoverInformation
    {
        int DiscoveredTime { get; }
        int ExploredTime { get; }
    }
}