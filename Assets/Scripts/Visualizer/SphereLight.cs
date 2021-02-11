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

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public override void Visualize() {

        // lets get the intensity and apply it to our scale;
        SphereIntensity = spawner.SphereIntensityMultiplier * AudioPeer.intensity;

        transform.localScale = new Vector3(SphereIntensity, SphereIntensity, SphereIntensity);
        
    }

}
