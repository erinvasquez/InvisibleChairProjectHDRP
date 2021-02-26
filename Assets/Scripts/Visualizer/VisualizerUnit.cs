using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Visualizer Unit abstract class that other visualizer units
/// will inherit
/// </summary>
public abstract class VisualizerUnit : MonoBehaviour {
    Vector3 origin;

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Spawner">Our Visualizer Spawning GameObject</param>
    /// <param name="startPosition"></param>
    public virtual void Initialize(VisualizerSpawner visualizer, Vector3 startPosition) {
        origin = startPosition;

        transform.position = origin;
        transform.parent = visualizer.transform;
    }



}
