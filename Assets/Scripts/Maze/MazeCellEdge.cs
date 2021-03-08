using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Maze Cell Edge abstract class that other
/// edges will inherit
/// </summary>
public abstract class MazeCellEdge : MonoBehaviour {
    public MazeCell cell, otherCell;
    public MazeDirection direction;


    /// <summary>
    /// Initialization called by VisualizerMaze
    /// 
    /// Intialize this cell after it's instantiated
    /// </summary>
    /// <param name="cell">Cell we're currently processing</param>
    /// <param name="otherCell">The other cell across the edge</param>
    /// <param name="direction">MazeDirection that points to our edge and the cell across it</param>
    public virtual void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;

        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
        transform.localScale = cell.transform.localScale;
    }


}
