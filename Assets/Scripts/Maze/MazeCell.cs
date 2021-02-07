using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A maze cell
/// </summary>
public class MazeCell : MonoBehaviour {

    /// <summary>
    /// X and Z coordinates in our Maze
    /// </summary>
    public IntVector2 coordinates;

    /// <summary>
    /// An arry of edges available to this cell
    /// </summary>
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    /// <summary>
    /// The number of edges this cell has initialized successfully
    /// </summary>
    private int initializedEdgeCount;

    /// <summary>
    /// Returns true if all edges in all directions have been initialized
    /// </summary>
    public bool IsFullyInitialized {

        get {
            return initializedEdgeCount == MazeDirections.Count;
        }

    }

    /// <summary>
    /// Returns a MazeDirection for an edge that hasn't been initialized
    /// at random
    /// </summary>
    public MazeDirection RandomUninitializedDirection {

        get {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);

            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }

                    skips -= 1;
                }
            }

            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");

        }

    }

    /// <summary>
    /// Gets a MazeDirection for an Edge
    /// </summary>
    /// <param name="direction">MazeDirection for where the edge is relative to this cell</param>
    /// <returns></returns>
    public MazeCellEdge GetEdge(MazeDirection direction) {
        return edges[(int)direction];
    }

    /// <summary>
    /// Sets an edge for this cell in a certain direction
    /// </summary>
    /// <param name="direction">Direction the edge is relative to the cell</param>
    /// <param name="edge">The edge we're attaching to this cell</param>
    public void SetEdge(MazeDirection direction, MazeCellEdge edge) {
        edges[(int)direction] = edge;

        initializedEdgeCount += 1;
    }

    

    

}
