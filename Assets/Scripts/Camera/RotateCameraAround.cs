using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraAround : MonoBehaviour {

    public GameObject target;
    public float speedMod = 1f;
    public int degrees = 90;
    public Vector3 point;

    public bool moving = false;

    private void Start() {
        point = target.transform.position;

        // Makes the camera look at our point
        transform.LookAt(point, Vector3.left);
    }

    private void Update() {

        if (!Conductor.musicSourceReadyForVisualizing) {
            return;
        }

        // if our conductor is ready


        // Rotate around point coordinate, rotating the [] axis, X degrees per second
        transform.RotateAround(point, new Vector3(1f, 0f, 0f), degrees * Time.deltaTime * Conductor.secondsPerBeat / 4);
    }

}
