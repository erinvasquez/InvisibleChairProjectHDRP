using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizerMazeManager : MonoBehaviour {

    public IntVector2 mazeSize;
    public GameObject[] mazeObjects;
    public int mazeGenerationRate;
    public bool mazesGenerating = false;

    /// <summary>
    /// Do something during update
    /// </summary>
    private void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.musicSourceReadyForVisualizing) {
            return;
        }

        // Once the conductor is ready, we can start generating

        // If any mazes we have out that have finished generating
        for (int a = 0; a < mazeObjects.Length; a++) {

            if (mazeObjects[a].transform.GetChild(0).GetComponent<VisualizerMaze>().mazeGenerated) {
                // restart generation for [the maze/ all mazes?]
            }

        }


        // If we don't have any mazes generating
        if (!mazesGenerating) {
            StartMazeGenerationCoroutine();
        }


    }

    /// <summary>
    /// Called by Visualizer spawner to set up our
    /// VMManager
    /// </summary>
    /// <param name="size"></param>
    /// <param name="regenRate"></param>
    public void Initialize(IntVector2 size, int regenRate) {
        mazeSize = size;
        mazeGenerationRate = regenRate;
    }

    /// <summary>
    /// Called in VisualizerSpawner,
    /// instantiates our Maze prefabs
    /// </summary>
    /// <param name="VMazePrefab">Visualizer Maze Prefab</param>
    /// <param name="startPos">Starting position vector</param>
    /// <param name="endPos">Ending position vector</param>
    /// <param name="dir">Movement direction vector</param>
    /// <param name="rotation">Rotation vector</param>
    /// <param name="size">Maze size</param>
    public void InitializeMazes(GameObject VMazePrefab, Vector3 startPos, Vector3 endPos, Vector3 dir, Vector3 rotation, IntVector2 size) {

        for (int a = 0; a < mazeObjects.Length; a++) {
            mazeObjects[a] = Instantiate(VMazePrefab, dir, Quaternion.identity) as GameObject;

            // Parent our new objects to this
            mazeObjects[a].transform.parent = transform;
            //Debug.Log("maze " + a + " has " + mazeObjects[a].transform.childCount + " children");

            // Initialize
            //old mazeregenrate param is (Conductor.secondsPerBeat * mazeGenerationRate) / (mazeSize.x * mazeSize.z)
            mazeObjects[a].transform.GetChild(0).GetComponent<VisualizerMaze>().Initialize(startPos, endPos, dir, rotation, size, mazeGenerationRate);
        }

    }

    /// <summary>
    /// Called in Visualizer Spawner
    /// </summary>
    public void StartMazeGenerationCoroutine() {

        // Stop any coroutines that exist already
        StopAllCoroutines();

        mazesGenerating = false;

        // if our mazes already exist, restart any coroutines
        for (int a = 0; a < mazeObjects.Length; a++) {

            if (mazeObjects[a]) {
                // TODO: Change this to only stop maze coroutines
                // TODO: also make it so we don't call this every time

                //mazeObjects[a].transform.GetChild(0).gameObject.GetComponent<VisualizerMaze>().Generate(mazeSize);
                StartCoroutine(mazeObjects[a].transform.GetChild(0).GetComponent<VisualizerMaze>().Generate(mazeSize));

            }
        }

        mazesGenerating = true;

    }

    public void RestartMazeRegenerationCoroutine() {
        StopAllCoroutines();

        mazesGenerating = false;

        for (int a = 0; a < mazeObjects.Length; a++) {
            // Destroy our maze ojbects
            StartCoroutine(mazeObjects[a].transform.GetChild(0).GetComponent<VisualizerMaze>().Generate(mazeSize));
        }

        mazesGenerating = true;

    }

    public void SetMazeMoving(int mazeIndex) {
        mazeObjects[mazeIndex].transform.GetChild(0).gameObject.GetComponent<VisualizerMaze>().moving = true;
    }

}
