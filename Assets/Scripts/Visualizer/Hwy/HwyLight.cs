using UnityEditor;
using UnityEngine;
public class HwyLight : MonoBehaviour {

    VisualizerSpawner spawner;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;
    public bool moving = false;


    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // If this is the first time we update, make sure we're in the right spot first
        // DO THIS HERE

        // Do our visualizing
        Visualize();

    }


    public void Initialize() {
        spawner = GameObject.Find("VisualizerSpawner").GetComponent<VisualizerSpawner>();

        startPos = spawner.sender.transform.position;
        endPos = spawner.sender.transform.position;
        direction = Vector3.forward;

        transform.parent = spawner.transform;
        transform.position = spawner.transform.position;
    }

    public void Initialize(VisualizerSpawner Spawner) {
        spawner = Spawner;

        startPos = spawner.transform.position + new Vector3(0, 0, -30);
        endPos = new Vector3(0, 0, 530);
        direction = Vector3.forward;

        transform.parent = spawner.transform;
        transform.position = startPos;
    }

    public void Initialize(VisualizerSpawner Spawner, Vector3 Start, Vector3 End) {
        spawner = Spawner;

        startPos = Start;
        endPos = End;
        direction = Vector3.forward;

        transform.parent = Spawner.transform;
        transform.position = startPos;
    }

    public void Initialize(VisualizerSpawner Spawner, Vector3 Start, Vector3 End, Vector3 Direction) {
        spawner = Spawner;

        startPos = Start;
        endPos = End;
        direction = Direction;

        transform.parent = Spawner.transform;
        transform.position = startPos;
    }

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public void Visualize() {

        // If we've passed our endpoint,
        // Place our light at the back of the line again, and stop it from moving
        if (transform.position.z >= endPos.z) {
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
    /// </summary>
    public void MoveLight() {
        transform.Translate(direction * spawner.speed * Time.deltaTime);
    }

    /// <summary>
    /// Called by Visualize to stop moving light forward during update
    /// </summary>
    public void StopLight() {
        moving = false;
        transform.position = startPos;
    }
}
