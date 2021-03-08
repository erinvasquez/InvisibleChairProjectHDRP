using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAround : MonoBehaviour {

    public GameObject target;
    public float speedMod = 1f;
    public Vector3 point;

    private void Start() {
        point = target.transform.position;

        // Makes the camera look at our point
        transform.LookAt(point);
    }

    private void Update() {
        
        if (!Conductor.visualizerReady) {
            return;
        }

        // if our conductor is ready


        // Rotate around point coordinate, rotating the [] axis, 45 degrees per second
        transform.RotateAround(point, new Vector3(0f, 1f, 0f), 45 * Time.deltaTime * Conductor.secondsPerBeat / 4);
    }

}
