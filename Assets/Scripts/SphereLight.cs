using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLight : MonoBehaviour {

    VisualizerSpawner spawner;
    Vector3 startPos;

    [SerializeField, Range(0.1f,1f)]
    public float intensity = .5f;

    

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.visualizerReady) {
            return;
        }

        intensity = 0f;

        // Do our visualizing
        //Visualize();

    }

    public void Initialize() {
        spawner = GameObject.Find("VisualizerSpawner").GetComponent<VisualizerSpawner>();

        startPos = spawner.sender.transform.position;

        transform.parent = spawner.transform;
        transform.position = spawner.transform.position;
    }

    public void Initialize(VisualizerSpawner Spawner) {
        spawner = Spawner;

        startPos = spawner.transform.position;

        transform.parent = spawner.transform;
        transform.position = startPos;
    }

    public void Initialize(VisualizerSpawner Spawner, Vector3 Start) {
        spawner = Spawner;

        startPos = Start;

        transform.parent = Spawner.transform;
        transform.position = startPos;
    }

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public void Visualize() {
    }

}
