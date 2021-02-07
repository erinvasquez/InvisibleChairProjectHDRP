using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Maze Cell Edge Passage in the form of a Door
/// </summary>
public class MazeDoor : MazePassage{

    public Transform hinge;

    private MazeDoor OtherSideOfDoor {
        get {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
        }
    }

    public override void Initialize (MazeCell primary, MazeCell other, MazeDirection direction) {
        base.Initialize(primary, other, direction);

        if (OtherSideOfDoor != null) {
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        }


    }

}
