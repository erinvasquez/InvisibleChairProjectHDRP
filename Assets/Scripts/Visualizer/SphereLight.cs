using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLight : VisualizerUnit {

    VisualizerSpawner spawner;
    float SphereIntensity;

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

    public override void Initialize(VisualizerSpawner visualizer, Vector3 position) {
        spawner = visualizer;
        transform.parent = visualizer.transform;
        transform.position = position;
    }

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public void Visualize() {

        // lets get the intensity and apply it to our scale;
        SphereIntensity = spawner.SphereIntensityMultiplier * AudioPeer.intensity;

        transform.localScale = new Vector3(SphereIntensity, SphereIntensity, SphereIntensity);
        
    }



}
