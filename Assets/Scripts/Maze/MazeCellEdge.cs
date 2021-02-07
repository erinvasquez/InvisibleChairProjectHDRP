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
    /// Extended method must be declared virtual
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="otherCell"></param>
    /// <param name="direction"></param>
    public virtual void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;

        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }


}
