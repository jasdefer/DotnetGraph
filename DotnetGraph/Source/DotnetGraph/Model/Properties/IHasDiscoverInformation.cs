namespace DotnetGraph.Model.Properties;

public interface IHasDiscoverInformation
{
    int DiscoveredTime { get; set; }
    int ExploredTime { get; set; }
}
