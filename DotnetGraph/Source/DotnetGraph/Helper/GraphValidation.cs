﻿using DotnetGraph.Model.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotnetGraph.Helper
{
    public static class GraphValidation
    {
        /// <summary>
        /// Check, if each id is unique for given entities.
        /// </summary>
        public static void ValidateUniqueIds(IEnumerable<IHasId> entities)
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
                throw new Exception($"Found {uniqueIds.Count} unique ids in {numberOfEntities} elements.");
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have non negative capacities.
        /// </summary>
        public static void ValidateOnlyPositiveCapacities<TNode, TArc>(IList<TNode> nodes)
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
                        throw new Exception($"At least one arc has a negative capacity of {arc.Capacity}");
                    }
                }
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have a unique id.
        /// </summary>
        public static void ValidateUniqueArcIds<TArc>(IEnumerable<IHasOutgoingArcs<TArc>> nodes)
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
                throw new Exception($"Found {uniqueArcIds.Count} unique arc ids for {numberOfArcs} arcs");
            }
        }

        /// <summary>
        /// Check if all given ids exists at least once in the given entites.
        /// </summary>
        public static void IdExists(IEnumerable<IHasId> entities, params int[] ids)
        {
            if (entities is null)
            {
                throw new ArgumentNullException(nameof(entities));
            }

            if (ids is null)
            {
                return;
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
                throw new Exception($"Not every id exists in the list of entities.");
            }
        }

        /// <summary>
        /// Check, if all arcs leaving the given nodes have non negative weights.
        /// </summary>
        public static void ValidateOnlyPositiveWeights<TNode, TArc>(IEnumerable<TNode> nodes)
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
                        throw new Exception($"At least one arc has a negative weight of {arc.Weight}");
                    }
                }
            }
        }

        /// <summary>
        /// Check, if any node has antiparallel arcs. Two arcs are antiparallel, if the first connects node 2 from node 1 and the second node 1 from node 2.
        /// </summary>
        public static void ValidateNoAntiparallelArcs<TNode, TArc>(IList<TNode> nodes)
            where TNode : IHasId, IHasOutgoingArcs<TArc>
            where TArc : IHasDestination<TNode>
        {
            if (nodes is null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                var hasAntiparallelArcs = nodes[i].OutgoingArcs.Any(x => x.Destination.OutgoingArcs.Any(y => y.Destination.Id == nodes[i].Id));
                if (hasAntiparallelArcs)
                {
                    throw new Exception($"Node {nodes[i].Id} has antiparallel arcs.");
                }
            }
        }
    }
}