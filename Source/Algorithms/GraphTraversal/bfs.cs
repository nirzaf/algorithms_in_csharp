﻿#region copyright
/* 
 * Copyright (c) 2019 (PiJei) 
 * 
 * This file is part of CSFundamentalAlgorithms project.
 *
 * CSFundamentalAlgorithms is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * CSFundamentalAlgorithms is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with CSFundamentals.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

// TODO: Specify time and space complexity for BFS and DFS. 
// TODO: In recursive versions of BFS and DFS also compute distance from root. 
using System.Collections.Generic;

namespace CSFundamentals.Algorithms.GraphTraversal
{
    /// <summary>
    /// Implements Breadth First Search algorithm for graph traversal.
    /// </summary>
    public class BFS
    {
        /// <summary>
        /// An iterative version of Breadth First Search algorithm for traversing a graph that might have cycles.
        /// </summary>
        /// <remarks>
        /// A graph can have many BFS orderings. Each ordering depends on (a) the start node, and (b) the order by which each node's adjacent nodes are visited. 
        /// </remarks>
        /// <typeparam name="TValue">Type of the values stored in graph nodes.</typeparam>
        /// <param name="root">A node in the graph from which traversal starts. </param>
        /// <returns>A sequence of all graph nodes, put into a BFS ordering.</returns>
        public static List<GraphNode<TValue>> BFS_Iterative<TValue>(GraphNode<TValue> root) /* Root is the node from which the search starts.*/
        {
            var queue = new Queue<GraphNode<TValue>>();

            root.IsInserted = true;
            root.DistanceFromRoot = 0;
            queue.Enqueue(root);

            /* Stores a BFS ordering of the nodes. */
            var bfsOrdering = new List<GraphNode<TValue>>();

            while (queue.Count > 0) /* While queue is not empty.*/
            {
                GraphNode<TValue> nextNode = queue.Dequeue();
                bfsOrdering.Add(nextNode); /* Appending the queue head to bfsOrdering.*/

                /* Enqueue all the adjacent neighbors of nextNode. */
                foreach (GraphEdge<TValue> edge in nextNode.Adjacents)
                {
                    if (!edge.Node.IsInserted) /* Without this, and in the presence of cycles, the loop will be endless, the program can get out of memory exceptions. */
                    {
                        edge.Node.IsInserted = true;
                        edge.Node.DistanceFromRoot = nextNode.DistanceFromRoot + 1;
                        queue.Enqueue(edge.Node);
                    }
                }
            }

            return bfsOrdering;
        }

        /// <summary>
        /// A recursive version of Breadth First Search algorithm for traversing a graph that might have cycles.
        /// <remarks>
        /// A graph can have many BFS orderings. Each ordering depends on (a) the start node, and (b) the order by which each node's adjacent nodes are visited. 
        /// </remarks>
        /// </summary>
        /// <typeparam name="TValue">Type of the values stored in graph nodes.</typeparam>
        /// <param name="queue">A queue structure for implementing recursion. The queue is expected to have the start node enqueued in it. </param>
        /// <param name="bfsOrdering">A sequence of all graph nodes, put into a BFS ordering.</param>
        public static void BFS_Recursive<TValue>(Queue<GraphNode<TValue>> queue, List<GraphNode<TValue>> bfsOrdering)
        {
            if (queue.Count == 0)
            {
                return;
            }

            GraphNode<TValue> node = queue.Dequeue();
            bfsOrdering.Add(node);

            foreach (GraphEdge<TValue> edge in node.Adjacents)
            {
                if (!edge.Node.IsInserted) /* To prevent endless recursion in case graph has cycles. */
                {
                    edge.Node.IsInserted = true;
                    queue.Enqueue(edge.Node);
                }
            }
            BFS_Recursive(queue, bfsOrdering);
        }
    }
}
