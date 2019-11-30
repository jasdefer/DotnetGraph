# DotnetGraph
This library is a collection of algorithms running on directed or undirected graphs.
It is very easy to create the graph before running many differnt algorithms.
Currently the following algorithms are implemented:
- [Directed graph](https://en.wikipedia.org/wiki/Directed_graph)
  - [Shrotest path tree](https://en.wikipedia.org/wiki/Shortest-path_tree)
    - [Dijkstra's algorithm](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)
    - FIFO algorithm
  - [Distance matrix](https://en.wikipedia.org/wiki/Distance_matrix)
    - Running a shortest path tree for each node in the graph
- [Undirected graph](https://en.wikipedia.org/wiki/Graph_(discrete_mathematics))
  - Minimum spanning tree
    - [Kruskal's algorithm](https://en.wikipedia.org/wiki/Kruskal%27s_algorithm)

An edge connects two nodes `new Arc<string>("A","B",1)` with a given weight.
Both nodes are of the type `string`, but any other type can be used as well.
In this example the nodes `A` and `B` are connected with a weight of 1.

An arc connects two nodes in a specific direction `new Arc<string>("A","B",1)`.
The destination node "B" can be reached from the origin node "A" (with a weight of 1) but not the other way round.

The following code runs Dijkstra's algorithm on a small graph with the origin `A`:

```c#
var arcs = new Arc<string>[]()
{
	new Arc<string>("A", "B", 1),
	new Arc<string>("A", "C", 2),
	new Arc<string>("B", "C", 1)
};
var dijkstra = new Dijkstra();
var shortestPathTree = dijkstra.GetShortestPathTree(arcs, "A");
```

The `shortestPathTree` is a `Dictionary<T, Arc<T>[]>` listing the arcs of the shortest path (value) for each node in the graph (key).
`T` is the type of the nodes and is a `string` for the above example.