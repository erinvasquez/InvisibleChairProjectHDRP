using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages our Maze's functions
/// </summary>
public class MazeManager : MonoBehaviour {

    public Maze mazePrefab;
    private Maze mazeInstance;

    /// <summary>
    /// Called before Update()
    /// </summary>
    void Start() {
        GenerateMaze();
    }

    /// <summary>
    /// Called once per frame,
    /// checks if we press space to 
    /// </summary>
    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            RestartGame();
        }

    }

    /// <summary>
    /// Generates our maze via coroutines
    /// </summary>
    private void GenerateMaze() {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }

    /// <summary>
    /// Set up our environment to re-generate our maze
    /// </summary>
    private void RestartGame() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        GenerateMaze();
    }

}
