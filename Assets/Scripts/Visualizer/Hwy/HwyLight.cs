using UnityEditor;
using UnityEngine;

/// <summary>
/// A type of Visualizer Unit
/// 
/// Instantiated and Initialized by our Visualizer Spawner
/// </summary>
public class HwyLight : VisualizerUnit {
    Vector3 startPosition;
    Vector3 endPosition;
    Vector3 direction;
    
    public bool moving = false;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Spawner">Our Visualizer Spawning GameObject</param>
    /// <param name="startPosition"></param>
    public void Initialize(VisualizerSpawner visualizer, Vector3 startPos, Vector3 endPos, Vector3 dir) {
        startPosition = startPos;
        endPosition = endPos;
        direction = dir;


        transform.position = startPosition;
        transform.parent = visualizer.transform;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // Do our visualizing
        Visualize();

    }

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public override void Visualize() {
        // If we've passed our endpoint,
        // Place our light at the back of the line again, and stop it from moving
        if (transform.position.z >= endPosition.z) {
            StopLight();
        }

        // If the light is supposed to be moving...
        if (moving) {
            // Move it forward enough for 1 update
            MoveLight();
        }

    }

    /// <summary>
    /// Called by Visualize, moves the light forward [one frame?]
    /// 
    /// CHANGE THIS to calculate speed inside here, rather than on the visualizer
    /// </summary>
    public void MoveLight() {

        float speed = Vector3.Distance(startPosition, endPosition) / (float)Conductor.secondsPerBeat;

        transform.Translate(direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// Called by Visualize to stop moving light forward during update
    /// </summary>
    public void StopLight() {
        moving = false;
        transform.position = startPosition;
    }
}
