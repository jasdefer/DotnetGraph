namespace DotnetGraph.Algorithms.Components.StronglyConnectedComponents.Tarjan;

public record TarjanArc(int Id, TarjanNode Destination) :
    IHasId,
    IHasDestination<TarjanNode>;
