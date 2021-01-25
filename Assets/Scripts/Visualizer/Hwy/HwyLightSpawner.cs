using UnityEngine;

/// <summary>
/// Our HwyLight Spawner.
/// Works in conjunction with AudioPeer and Conductor to spawn
/// lights every beat
/// </summary>
public class HwyLightSpawner : MonoBehaviour {

    public static HwyLightSpawner instance;
    public AudioSource musicSource;
    public GameObject sender;
    public GameObject receiver;

    [Header("Prefabs")]
    public GameObject hwyLightPrefab;

    [Header("Factors")]
    public float speed = 0f;
    public float distance = 0f;


    public int beat = 1;

    public HwyLight[] lightQueue; // An array of HwyLights we use to visualize
    public int currentLight = 0; // Current HwyLight

    /// <summary>
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;
    }

    /// <summary>
    /// Called before first frame update
    /// 
    /// Find our music and main camera
    /// Instantiate enough HWYLights to loop queue round
    /// If Conductor is ready, then AudioPeer is ready, then Visualize
    /// HwyLights
    /// </summary>
    void Start() {
        musicSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>();

        // Instantiate a queue of HwyLights (beats per measure + 1)
        lightQueue = new HwyLight[Conductor.beatsPerMeasure + 1];

        // Our actual spawning algorithm
        // Start by spawning one more than necessary (in most cases beatsPerMeasure + 1)
        // That way we can just initialize [5], have them "spawn in" and move back around
        // the queue to be teleported to the beginning again
        for (int a = 0; a < lightQueue.Length; a++) {
            lightQueue[a] = Object.Instantiate(hwyLightPrefab, new Vector3(0, a * 3, (a * 3) - 30), Quaternion.identity).GetComponent<HwyLight>();
            lightQueue[a].Initialize(this);
        }

    }

    /// <summary>
    /// Called once per frame, calculate speed once conductor ready,
    /// send lights every beat
    /// </summary>
    void Update() {

        // If our conductor isn't ready to visualize, don't do anything
        if (!Conductor.visualizerReady) {
            return;
        }


        // Since Conductor is ready and has analyzed the song,
        // we can calculate speed
        if (speed == 0f) {
            // distance = transform.position.z - mainCamera.transform.position.z;
            distance = 530; // used to be transform.position.z (I reversed it) // fix this to use our "exit" position that we have set up
            Debug.Log("Visualizer ready, calculating speed... ");
            speed = distance / (Conductor.secondsPerBeat * Conductor.beatsPerMeasure);

            Debug.Log("HwyLight speed calculated: " + speed);

            return;
        }


        
        if (beat <= (int)Conductor.songPositionInBeats) {
            SendLight();

            beat++;
            currentLight++;
            if (currentLight >= lightQueue.Length) {
                currentLight = 0;
            }

        }

    }

    /// <summary>
    /// Activate our light so that it will move into position on the spawner, and set it in motion
    /// 
    /// The light will move until it passes behind the player, moving back into position at the spawner
    /// </summary>
    void SendLight() {
        Debug.Log("Sending light " + currentLight);
        lightQueue[currentLight].GetComponent<HwyLight>().moving = true;
    }

}
