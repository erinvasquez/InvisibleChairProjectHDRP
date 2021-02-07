using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Our 4 different maze directions
/// </summary>
public enum MazeDirection {
    North,
    East,
    South,
    West
}

/// <summary>
/// Manages our MazeDirections
/// </summary>
public static class MazeDirections {
    public const int Count = 4;

    /// <summary>
    /// A property that gives a random Maze Direction
    /// </summary>
    public static MazeDirection RandomValue {
        get {
            return (MazeDirection)Random.Range(0, Count);
        }
    }

    /// <summary>
    /// Our MazeDirections array as IntVector2
    /// </summary>
    private static IntVector2[] vectors = {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0)
    };

    /// <summary>
    /// Opposite MazeDirection array
    /// </summary>
    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    /// <summary>
    /// A quaternion array of rotations
    /// </summary>
    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    /// <summary>
    /// Converts a MazeDirection into a IntVector2 direction type
    /// </summary>
    /// <param name="direction">Our desired directions</param>
    /// <returns></returns>
    public static IntVector2 ToIntVector2 (this MazeDirection direction) {
        return vectors[(int) direction];
    }

    /// <summary>
    /// Converts a MazeDirection into an IntVector2 direction type
    /// in the opposite direction as ToIntVector()
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static MazeDirection GetOpposite (this MazeDirection direction) {
        return opposites[(int)direction];
    }

    /// <summary>
    /// Converts a MazeDirection into a Quaternion rotation type
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public static Quaternion ToRotation (this MazeDirection direction) {
        return rotations[(int)direction];
    }

}
