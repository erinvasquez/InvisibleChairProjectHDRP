using UnityEngine;

/// <summary>
/// Our HwyLight Spawner.
/// Works in conjunction with AudioPeer and Conductor to spawn
/// lights every beat
/// </summary>
public class VisualizerSpawner : MonoBehaviour {

    public static VisualizerSpawner instance;

    // Sender and Receiver transforms for 2 GameObjects in the
    // spawner prefab that can be set in the editor
    [SerializeField]
    public Transform sender;
    [SerializeField]
    public Transform receiver;


    public GameObject[] VisualizerUnitPrefabs = new GameObject[4];

    [Header("Highway Lights")]
    public bool UseHwyLight = true;
    [Range(1, 16)]
    public int HwyLightsPerBeat = 2;
    [SerializeField]
    int HwyLightIndex = 0; // Current HwyLight
    private Transform[] HwyLights;
    

    [Header("Sphere Light")]
    public bool UseSphere = true;
    [Range(1, 1000)]
    public int SphereIntensityMultiplier = 1000;
    private SphereLight sphereLight;

    [Header("Maze")]
    public bool UseMaze = true;
    [Range(2, 20)]
    int mazeSize = 20;
    private VisualizerMaze[] mazes;
    private VisualizerMaze maze;

    [Header ("Clock")]
    public bool UseClock = true;
    private Clock clock;


    [Header("Visualizer Stuff")]
    public int beat = 1;
    public float speed = 0f;

    

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
    /// Instantiate the lights we'll use
    /// If Conductor is ready, then AudioPeer is ready, then Visualize
    /// HwyLights, our maze
    /// 
    /// </summary>
    void Start() {

        // Instantiate a queue of HwyLights (beats per measure + 1)
        /*HwyLights = new HwyLight[Conductor.beatsPerMeasure + 1];
        for (int a = 0; a < HwyLights.Length; a++) {
            HwyLights[a] = Object.Instantiate(HwyLightPrefab, Vector3.zero, Quaternion.identity).GetComponent<HwyLight>();
            HwyLights[a].Initialize(this, senderPosition, receiverPosition, Vector3.forward);
        }*/

        // Instantiate all of our Visualizer Units
        for (int a = 0; a < VisualizerUnitPrefabs.Length; a++) {


            if (VisualizerUnitPrefabs[a].GetType() == typeof(VisualizerMaze)) {

                //maze = Instantiate(maze, Vector3.zero, Quaternion.identity) as VisualizerMaze;

            } else if (VisualizerUnitPrefabs[a].GetType() == typeof(SphereLight)) {

                //sphereLight = Instantiate(sphereLight, Vector3.zero, Quaternion.identity) as SphereLight;


            } else if (VisualizerUnitPrefabs[a].GetType() == typeof(HwyLight)) {

                HwyLights = new Transform[Conductor.beatsPerMeasure + 1];

                for (int b = 0; b < HwyLights.Length; b++) {
                    HwyLights[b] = Instantiate(VisualizerUnitPrefabs[a], Vector3.zero, Quaternion.identity).transform;
                    HwyLights[b].GetComponent<HwyLight>().Initialize(this, sender.position, receiver.position, Vector3.forward);
                }


            } else if (VisualizerUnitPrefabs[a].GetType() == typeof(Clock)) {

                //clock = Instantiate(clock, Vector3.zero, Quaternion.identity) as Clock;

            }


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
        HwyLightIndex++;

        if (HwyLightIndex >= HwyLights.Length) {
            HwyLightIndex = 0;
        }

        // Send each type of light
        SendHwyLights();
    }

    /// <summary>
    /// Set "moving" flags on all HwyLights
    /// </summary>
    void SendHwyLights() {
        // Debug.Log("Sending HwyLight " + currentHwyLight);
        HwyLights[HwyLightIndex].GetComponent<HwyLight>().moving = true;
    }

}
