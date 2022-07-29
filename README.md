# DotnetGraph

## Features
This library is a collection of algorithms running on directed or undirected graphs.
You can use build in implementations for nodes, arcs, and edges or just pass your own classes to the algorithms.
Currently, the following algorithms are implemented:
- [Undirected graph](https://en.wikipedia.org/wiki/Graph_(discrete_mathematics))
  - Graph Components
     - [Connected Components](https://en.wikipedia.org/wiki/Component_(graph_theory))
  - Minimum spanning tree
    - [Kruskal's algorithm](https://en.wikipedia.org/wiki/Kruskal%27s_algorithm)
- [Directed graph](https://en.wikipedia.org/wiki/Directed_graph)
  - [Shrotest path](https://en.wikipedia.org/wiki/Shortest_path_problem)
    - [Dijkstra's algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)
  - [Shrotest path tree](https://en.wikipedia.org/wiki/Shortest-path_tree)
    - FIFO Algorithm
  - [k shortest path routing](https://en.wikipedia.org/wiki/K_shortest_path_routing#:~:text=The%20k%20shortest%20path%20routing,the%20loopless%20k%20shortest%20paths.)
    - [Yen's algorithm](https://en.wikipedia.org/wiki/Yen%27s_algorithm)
  - Graph Components
    - [Strongly Connected Components](https://en.wikipedia.org/wiki/Strongly_connected_component)
  - [Depth-first search](https://en.wikipedia.org/wiki/Depth-first_search)
    - Cormen depth first search
  - [Flow network](https://en.wikipedia.org/wiki/Flow_network)
    - Max flow
      - [Ford-Fulkerson algorithm](https://en.wikipedia.org/wiki/Ford%E2%80%93Fulkerson_algorithm)

Additionally, algorithms to create random graphs and some minor graph	helper are implemented.

## Get started

### Installation

Get the library `DotnetGraphCore` from [NuGet](https://www.nuget.org/packages/DotnetGraphCore/):

```
Install-Package DotnetGraphCore
```

or download the library from GitHub and reference it locally.

### Example usage for the shortest path algorithm

Calculating the shortest path from an origin to a destination is easy.
First, create your graph as a collection of nodes.
You can use your [own implementations](#custom-graph-implementations) for the nodes and arcs.
Here, a helper creates a graph with three out of the box implemented nodes.
Pass your nodes to any implementation of the shortest path algorithm.
In this case Dijkstra's algorithm computes the shortest path from node 1 to node 3.

```c#
//Create nodes
var arcs = new ArcData[]
{
	new ArcData(1,2,10), //Creates an arc from a Node 1 to Node 2 with a weight of 10
	new ArcData(1,3,22), //Creates an arc from a Node 1 to Node 3 with a weight of 22
	new ArcData(2,2,11)  //Creates an arc from a Node 2 to Node 3 with a weight of 11
};
var nodes = GraphConverter.GetNodes(arcs);

//Get the shortest path from the origin to the destination
IShortestPathAlgorithm shortestPathAlgorithm = new DijkstraAlgorithm();
var result = shortestPathAlgorithm.GetShortestPath<WeightedDirectedGraphNode, WeightedDirectedGraphArc>(nodes, 1, 3);

//The shortest path from Node 1 to Node 3 are the arcs: 1 -> 2 -> 3 with a total weight of 21
Console.WriteLine(result.TotalWeight); // 21
Console.WriteLine(result.Arcs.Count); // 2
```

## Custom graph implementations

Dotnet Graph provides various implementations for nodes, arcs, and edges.
You can use them to create graphs and run all implemented algorithms.
Let us assume, that you are working on a project with your own classes which represent nodes, arcs, or edges.
They can be easily passed to the algorithms of this library without a problem.
Just add interfaces required by the algorithm you want to run on your classes.
The shortest path algorithm for example needs a collection of nodes which are connected by arcs.
The nodes and arcs need a unique id, and the arcs must have a weight used for calculating the shortest path.
Each arc is stored at its origin node and points to its destination.
That's it!
```c#
ShortestPathResult<TArc> GetShortestPath<TNode, TArc>(IList<TNode> nodes,
	int originNodeId,
	int destinationNodeId)
	where TNode : IHasOutgoingArcs<TArc>, IHasId
	where TArc : IHasDestination<TNode>, IHasWeight, IHasId;
```
If your nodes implement `IHasOutgoingArcs<TArc>` and `IHasId` and your arcs implement `IHasDestination<TNode>`, `IHasWeight` and `IHasId` they can be passed to any shortest path algorithm.
No worries, this maybe looks like a lot, but each interface requires a single property:

```c#
public interface IHasId
{
	int Id { get; }
}

public interface IHasDestination<out TNode>
{
	TNode Destination { get; }
}

public interface IHasWeight
{
	double Weight { get; }
}
	
public interface IHasOutgoingArcs<out TArc>
{
	IReadOnlyCollection<TArc> OutgoingArcs { get; }
}
```

This information should already be present in your own implementation if you want to run a shortest path algorithm.
Just add the interfaces to your nodes and arcs and return the required information.

## High performance

All algorithms are optimized to run as fast as possible.
Feel free to submit a pull request if you find any improvements.
Many algorithms convert the generic input to algorithm specific implementations.
The shortest path algorithm Dijkstra for example stores algorithm specific information at the nodes to run efficiently.
You can run the algorithm with any nodes and arcs if they implement the correct interfaces as described [above](#custom-graph-implementations).
But operating with the algorithm specific implementations improves the computational times.
You could either create the required objects by yourself or let the algorithm itself convert the input.

```c#
// Let the algorithm convert your custom nodes and compute the result
var shortestPathResult = dijkstraAlgorithm.GetShortestPath<Node, Arc>(customNodes, 1, 3);

// Pass the algorithm specific implementations of the nodes and arcs to compute the result
var dijkstraNodes = DijkstraAlgorithm.Convert<Node, Arc>(customNodes);
var shortestPathResult1 = DijkstraAlgorithm.GetShortestPath(dijkstraNodes, 1, 3);
var shortestPathResult2 = DijkstraAlgorithm.GetShortestPath(dijkstraNodes, 1, 4);
```

So, you can either chose the convenient but computational slower approach and just let the algorithm convert your input.
Alternatively, you can pass the algorithm specific implementations and get a solution faster.
The latter approach is recommended if you need to run an algorithm multiple times on the same set of nodes.
E.g. finding the shortest paths between many pairs of origin and destination nodes, because the nodes does not need to be converted for every computation of the shortest path.
The corresponding implementation of Dijkstra's algorithm looks like this:

```c#
public ShortestPathResult<TArc> GetShortestPath<TNode, TArc>(IList<TNode> nodes,
	int originNodeId,
	int destinationNodeId)
	where TArc : IHasDestination<TNode>, IHasWeight, IHasId
	where TNode : IHasOutgoingArcs<TArc>, IHasId
{
	var dijkstraNodes = Convert<TNode, TArc>(nodes);
	var dijkstraResult = GetShortestPath(dijkstraNodes, originNodeId, destinationNodeId);
	var shortestPathResult = ConvertResult<TNode, TArc>(nodes, dijkstraResult);
	return shortestPathResult;
}

public static ShortestPathResult<DijkstraArc> GetShortestPath(List<DijkstraNode> inputNodes,
	int originNodeId,
	int destinationNodeId)
{
	...
}
```



