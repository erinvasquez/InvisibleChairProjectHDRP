using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereLight : VisualizerUnit {

    VisualizerSpawner spawner;
    float SphereIntensity;
    int intensitySource;

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

    public void Initialize(VisualizerSpawner visualizer, Vector3 position, int source) {
        spawner = visualizer;
        transform.parent = spawner.transform;
        transform.position = position;
        intensitySource = source;
    }

    /// <summary>
    /// Called in Update(), does our actual visualizer work
    /// </summary>
    public void Visualize() {

        // lets get the intensity and apply it to our scale;
        SetIntensity(intensitySource);

        transform.localScale = new Vector3(SphereIntensity, SphereIntensity, SphereIntensity);
        
    }

    /// <summary>
    /// 0 is lows, 1 is mids, 2 is highs
    /// </summary>
    public void SetIntensity(int source) {

        switch (source) {
            case 0:
                SphereIntensity = Mathf.Log10(AudioPeer.lowsIntensity);
                break;
            case 1:
                SphereIntensity = Mathf.Log10(AudioPeer.midsIntensity);
                break;
            case 2:
                SphereIntensity = Mathf.Log10(AudioPeer.highsIntensity);
                break;
        }

    }

}
