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
    public bool playedSound = false;
    public float speed = 0f;

    //float timeAtLastHit = 0f;

    //public AudioSource hitmarker;

    private void Start() {
        //hitmarker = GameObject.Find("Hit").GetComponent<AudioSource>();
    }

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
        speed = Vector3.Distance(startPosition, endPosition) / (float)Conductor.secondsPerBeat;

    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.musicSourceReadyForVisualizing) {
            return;
        }

        if (transform.position.z <= endPosition.z) {
            // If we've passed our endpoint,
            // Place our light at the start again, and stop it from moving
            // use >= when moving forward instead of backwards

            ResetLight();
        }

        // If the light is supposed to be moving...
        if (moving) {
            // Move it forward enough for 1 update
            MoveLight();
        }

    }

    

    private void FixedUpdate() {


        /*
        if (transform.position.z <= 0 && !playedSound) {

            Debug.Log("Hit");

            hitmarker.Play();
            playedSound = true;

            Debug.Log("Time since last hit: " + (Time.time - timeAtLastHit));

            timeAtLastHit = Time.time;

        }
        */

    }

    /// <summary>
    /// Called by Update(), moves the light forward one frame
    /// 
    /// CHANGE THIS to calculate speed inside here, rather than on the visualizer
    /// </summary>
    public void MoveLight() {

        speed = Vector3.Distance(startPosition, endPosition) / (float)Conductor.secondsPerBeat;

        transform.Translate(direction * speed * Time.deltaTime);


        //GetComponent<Rigidbody>().MovePosition(direction * speed * Time.deltaTime);

    }

    /// <summary>
    /// Called by Update() to stop moving light forward
    /// </summary>
    public void ResetLight() {
        moving = false;
        playedSound = false;
        transform.position = startPosition;
    }

}
