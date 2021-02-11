using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages our Maze's functions
/// </summary>
public class MazeManager : MonoBehaviour {

    public VisualizerMaze mazePrefab;
    private VisualizerMaze mazeInstance;

    /// <summary>
    /// Called before Update()
    /// </summary>
    void Start() {
        //GenerateMaze();
    }

    /// <summary>
    /// Called once per frame,
    /// checks if we press space to 
    /// </summary>
    void Update() {

        // Make sure visualizer is ready
        if (!Conductor.visualizerReady) {
            return;
        }

        // Check if our visualizer is ready
        if (Conductor.visualizerReady) {

            
            //Visualize();
        }

    }

    /// <summary>
    /// Called in update
    /// 
    /// Start generating a maze if it isn't already being generated,
    /// </summary>
    public void Visualize() {

        // Don't start generating a new maze one already exists
        if (!mazeInstance) {
            GenerateMaze();
        } else {

            // We already have a mazeInstance, so re-generate if it's done
            if (mazeInstance.MazeGenerated) {
                RestartGeneration();
            }

        }

    }

    /// <summary>
    /// Generates our maze via coroutines
    /// </summary>
    private void GenerateMaze() {
        mazeInstance = Instantiate(mazePrefab) as VisualizerMaze;

        mazeInstance.transform.parent = transform;

        mazeInstance.transform.position = new Vector3(0f, 10f, 30f);
        mazeInstance.transform.Rotate(new Vector3(30f, 0f, 0f));

        StartCoroutine(mazeInstance.Generate());
    }

    /// <summary>
    /// Set up our environment to re-generate our maze
    /// </summary>
    private void RestartGeneration() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        GenerateMaze();
    }

}
