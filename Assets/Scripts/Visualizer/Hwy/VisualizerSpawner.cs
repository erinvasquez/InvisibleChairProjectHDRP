using UnityEngine;

/// <summary>
/// Our HwyLight Spawner.
/// Works in conjunction with AudioPeer and Conductor to spawn
/// lights every beat
/// </summary>
public class VisualizerSpawner : MonoBehaviour {

    public static VisualizerSpawner instance;
    public AudioSource musicSource;

    // Sender and Receiver transforms for 2 GameObjects in the
    // spawner prefab that can be set in the editor
    public Transform sender;
    public Transform receiver;

    [Header("Prefabs")]
    public GameObject HwyLightPrefab;
    public GameObject SphereLightPrefab;


    public int beat = 1;
    public float speed = 0f;

    HwyLight[] HwyLights; // An array of HwyLights we use to visualize
    int currentHwyLight = 0; // Current HwyLight

    /// <summary>
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;

        sender = GameObject.Find("Sender").transform;
        receiver = GameObject.Find("Receiver").transform;
        musicSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>();

    }

    /// <summary>
    /// Called before first frame update
    /// 
    /// Find our music and main camera
    /// Instantiate the lights we'll use
    /// If Conductor is ready, then AudioPeer is ready, then Visualize
    /// HwyLights, our maze
    /// 
    /// </summary>
    void Start() {

        // Instantiate a queue of HwyLights (beats per measure + 1)
        HwyLights = new HwyLight[Conductor.beatsPerMeasure + 1];
        for (int a = 0; a < HwyLights.Length; a++) {
            HwyLights[a] = Object.Instantiate(HwyLightPrefab, Vector3.zero, Quaternion.identity).GetComponent<HwyLight>();
            HwyLights[a].Initialize(this, sender.transform.position, receiver.transform.position);
        }

    }

    /// <summary>
    /// Called once per frame, calculate speed once conductor ready,
    /// 
    /// Trigger moving flags on our lights every beat
    /// </summary>
    void Update() {

        // SET-UP --------------------------------------------------------------------------
        // If our conductor isn't ready to visualize, don't do anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // POST SET-UP ----------------------------------------------------------------------
        if (speed == 0f) {
            // Speed calculated as our distance traveled over time
            // (distance between sender and receiver, time spent is one beat
            float distance = (receiver.position.z - sender.position.z);
            speed = distance / ((float) Conductor.secondsPerBeat);
            Debug.Log("Distance: " + distance);
        }

        // If we're in the next beat
        if (beat <= (int)Conductor.songPositionInBeats) {
            // Trigger moving flags for ALL our lights
            SendLights();
        }

    }

    /// <summary>
    /// Activate our light so that it will move into position on the spawner, and set it in motion
    /// 
    /// The light will move until it passes behind the player, moving back into position at the spawner
    /// </summary>
    void SendLights() {
        // Increment our beat by 1
        beat++;

        // Increment and loop our light indeces
        currentHwyLight++;

        if (currentHwyLight >= HwyLights.Length) {
            currentHwyLight = 0;
        }

        // Send each type of light
        SendHwyLights();
    }

    /// <summary>
    /// Set "moving" flags on all HwyLights
    /// </summary>
    void SendHwyLights() {
        // Debug.Log("Sending HwyLight " + currentHwyLight);
        HwyLights[currentHwyLight].GetComponent<HwyLight>().moving = true;
    }

}
