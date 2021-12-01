namespace DotnetGraph.Helper
{
    public static class GraphValidation
    {
        /// <summary>
        /// Check, if each id is unique for given entities.
        /// </summary>
        public static void ValidateUniqueIds<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : IHasId
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var uniqueIds = new HashSet<int>();
            var numberOfEntities = 0;
            foreach (var entity in entities)
            {
                numberOfEntities++;
                uniqueIds.Add(entity.Id);
            }
            if (uniqueIds.Count != numberOfEntities)
            {
                throw new IdsNotUniqueException($"Found {uniqueIds.Count} unique ids in {numberOfEntities} elements.");
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have non negative capacities.
        /// </summary>
        public static void ValidateOnlyPositiveCapacities<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>
            where TArc : IHasCapacity
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    if (arc.Capacity < 0)
                    {
                        throw new NegativeWeightException($"At least one arc has a negative capacity of {arc.Capacity}");
                    }
                }
            }
        }

        public static void ValidateUniqueEdgeIds<TNode, TEdge>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasEdges<TEdge>
            where TEdge : IHasId
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var uniqueEdgeIds = new HashSet<int>();
            var numberOfEdges = 0;
            foreach (var node in nodes)
            {
                foreach (var edge in node.Edges)
                {
                    uniqueEdgeIds.Add(edge.Id);
                    numberOfEdges++;
                }
            }

            // each edge should be counted double, because it is found once per connected node
            if (uniqueEdgeIds.Count * 2 != numberOfEdges)
            {
                throw new IdsNotUniqueException($"Found {uniqueEdgeIds.Count} unique edges ids for {numberOfEdges} edges");
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have a unique id.
        /// </summary>
        public static void ValidateUniqueArcIds<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>
            where TArc : IHasId
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var uniqueArcIds = new HashSet<int>();
            var numberOfArcs = 0;
            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    uniqueArcIds.Add(arc.Id);
                    numberOfArcs++;
                }
            }

            if (uniqueArcIds.Count != numberOfArcs)
            {
                throw new IdsNotUniqueException($"Found {uniqueArcIds.Count} unique arc ids for {numberOfArcs} arcs");
            }
        }

        /// <summary>
        /// Check if all given ids exists at least once in the given entites.
        /// </summary>
        public static void IdExists<TEntity>(IReadOnlyCollection<TEntity> entities, params int[] ids)
            where TEntity : IHasId
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            var foundId = new bool[ids.Length];
            foreach (var entity in entities)
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    if (entity.Id == ids[i])
                    {
                        foundId[i] = true;
                    }
                }
            }

            if (foundId.Any(x => x == false))
            {
                throw new InvalidIdException($"Not every id exists in the list of entities.");
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have non negative weights.
        /// </summary>
        public static void ValidateOnlyPositiveWeights<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasOutgoingArcs<TArc>
            where TArc : IHasWeight
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
            {
                foreach (var arc in node.OutgoingArcs)
                {
                    if (arc.Weight < 0)
                    {
                        throw new NegativeWeightException($"At least one arc has a negative weight of {arc.Weight}");
                    }
                }
            }
        }

        /// <summary>
        /// Check, if any node has antiparallel arcs. Two arcs are antiparallel, if the first connects node 2 from node 1 and the second node 1 from node 2.
        /// </summary>
        public static void ValidateNoAntiparallelArcs<TNode, TArc>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasDestination<TNode>
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
            {
                var hasAntiparallelArcs = node.OutgoingArcs.Any(x => x.Destination.OutgoingArcs.Any(y => y.Destination.Id == node.Id));
                if (hasAntiparallelArcs)
                {
                    throw new HasAntiparallelArcException($"Node {node.Id} has antiparallel arcs.");
                }
            }
        }

        /// <summary>
        /// Check, if each edge is in the edge list of both nodes
        /// </summary>
        public static void ValidateConsistentEdgeNodes<TNode, TEdge>(IReadOnlyCollection<TNode> nodes)
            where TNode : IHasId, IHasEdges<TEdge>
            where TEdge : IHasId, IConnectsNodes<TNode>
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            foreach (var node in nodes)
            {
                foreach (var edge in node.Edges)
                {
                    if (edge.Node1.Id != edge.Node2.Id)
                    {
                        TNode otherNode;
                        if (edge.Node1.Id == node.Id)
                        {
                            otherNode = edge.Node2;
                        }
                        else if (edge.Node2.Id == node.Id)
                        {
                            otherNode = edge.Node1;
                        }
                        else
                        {
                            throw new InvalidEdgeException($"The edge {edge.Id} does not connect {node.Id} but is in its edge list.");
                        }
                        if (!otherNode.Edges.Any(x => x.Id == edge.Id))
                        {
                            throw new InvalidEdgeException($"The edge {edge.Id} is not in the list of node {otherNode.Id}.");
                        }
                    }
                }
            }
        }
    }
}