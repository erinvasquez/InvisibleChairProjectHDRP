using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Our Road spawner
/// 
/// Spawns a road (with or without hwy lights attached?) that scrolls
/// at our [vehicle's speed]
/// </summary>
public class RoadSpawner : MonoBehaviour {

    public static RoadSpawner instance;
    public AudioSource musicSource;
    
    [Header("Prefabs")]
    public GameObject roadPrefab;


    private GameObject scaleableRoad;

    public float speed = 0f;
    public float distance = 0f;
    public int roadLength = 0;

    public int beat = 1;
    public Road[] roadQueue; // array of the road objects that we have
    public int currentRoad = 0;

    /// <summary>
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;
    }

    /// <summary>
    /// Called before first frame update
    /// </summary>
    void Start() {
        musicSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>(); // should we make our music source static?
        

        // Our actual sending algorithm
        // Start by spawning one more than necessary (in most cases beatsPerMeasure + 1)
        // That way we can just initialize [5], have them "spawn in" and move back around
        // the queue to be teleported to the beginning again
        // Instantiate a queue of Roads (bpm + 1 for now, I don't have any better ideas this is honestly just 5 most of the time)
        roadQueue = new Road[Conductor.beatsPerMeasure + 1];

        for (int a = 0; a < roadQueue.Length; a++) {
            roadQueue[a] = Object.Instantiate(roadPrefab, new Vector3((float)transform.position.x, (float)a, (float)transform.position.z), Quaternion.identity).GetComponent<Road>();
            roadQueue[a].Initialize(this);
        }
    }

    /// <summary>
    /// Called once per frame, calculates speed once the conductor is ready,
    /// sends
    /// </summary>
    void Update() {

        // If our AudioPeer isn't ready, conductor isn't ready to play music, don't do anything
        if (!AudioPeer.audioPeerReady || !Conductor.conductorReady) {
            return;
        }

        // Since Conductor is ready and has analyzed the song,
        // we can calculate speed (if we want use it in our calculations)
        if (speed == 0f) {
            // distance = transform.position.z - mainCamera.transform.position.z;
            distance = transform.position.z - 0; // fix this to use our "exit" position that we have set up
            Debug.Log("Conductor ready, calculating speed... ");
            speed = distance / (Conductor.secondsPerBeat * Conductor.beatsPerMeasure);

            Debug.Log("Road speed calculated: " + speed);

            // Use our distance to determine the length of each of our roads
            // It should just be distance / bpm
            roadLength = (int)(distance / Conductor.beatsPerMeasure + 1);

            return;
        }

        
        // Pre Set-Up
        // ------------------------
        // Post Set-up Update Loop

        if (beat <= (int)Conductor.songPositionInBeats) {
            SendRoad();

            beat++;
            currentRoad++;
            if (currentRoad >= roadQueue.Length) {
                currentRoad = 0;
            }

        }

    }

    /// <summary>
    /// Activate our light so that it will move into position on the spawner, and set it in motion
    /// 
    /// The light will move until it passes behind the player, moving back into position at the spawner
    /// </summary>
    void SendRoad() {
        Debug.Log("Sending road " + currentRoad);

        roadQueue[currentRoad].GetComponent<Road>().moving = true;

    }

    public float getSpeed() {
        return speed;
    }

    public float getDistance() {
        return distance;
    }

}
