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
    Transform sender;
    [SerializeField]
    Transform receiver;

    [Header("Prefabs")]
    public GameObject hwyLightPrefab;
    public GameObject sphereLightPrefab;
    public GameObject vMazePrefab;
    public GameObject vMazeManagerPrefab;
    public GameObject clockPrefab;

    [Header("Highway Lights")]
    public bool useHwyLight = true;
    [Range(1, 16)]
    public int hwyLightsPerBeat = 1;
    int hwyLightIndex = 0; // Current HwyLight
    GameObject[] hwyLights;
    float lastTimeHwyLightSent = 0f; // the last time in seconds that we sent a HwyLight


    [Header("Sphere Light")]
    public bool useSphere = true;
    public Vector3 sphereLightPosition = new Vector3(-15f, 5f, 20f);
    [Range(1, 1000)]
    public int sphereIntensityMultiplier = 1000;
    SphereLight lowSphereLight;
    SphereLight midSphereLight;
    SphereLight highSphereLight;


    [Header("Maze")]
    /// <summary>
    /// True when we want to initialize our maze
    /// </summary>
    public bool useMaze = true;
    /// <summary>
    /// Editor value for maze X size
    /// </summary>
    [Range(1, 100)]
    public int mazeX = 20;
    /// <summary>
    /// Editor value for maze Z size
    /// </summary>
    [Range(1, 100)]
    public int mazeZ = 20;
    /// <summary>
    /// Actually IntVector2 that we use to generate our maze
    /// </summary>
    IntVector2 mazeSize;
    /// <summary>
    /// Maze Spawning position
    /// </summary>
    public Vector3 mazeStartPosition = new Vector3(0f, 10f, 0f);
    /// <summary>
    /// 
    /// </summary>
    public Vector3 mazeEndPosition = new Vector3(0f, 10f, 200f);
    /// <summary>
    /// Default maze rotation
    /// </summary>
    public Vector3 mazeStartRotation = new Vector3(30f, 0f, 0f);
    /// <summary>
    /// Maze regeneration rate
    /// </summary>
    [Range(1, 128)]
    public int mazeRegenerationRate = 32;
    /// <summary>
    /// Number of Mazes per Beat
    /// </summary>
    [Range(1, 16)]
    public int mazesPerBeat = 1;
    /// <summary>
    /// Last time in seconds we sent a maze
    /// </summary>
    float lastTimeMazeSent = 0f;
    GameObject mazeManager;
    public int mazeIndex = 0;


    [Header("Clock")]
    public bool UseClock = true;
    GameObject clock;
    public Vector3 ClockPosition = new Vector3(0f, 10f, 100f);


    [Header("Visualizer Stuff")]
    /// <summary>
    /// Starting at 1, the amount of quarterNotes in we are
    /// </summary>
    public int beatIndex = 1;

    // do we really need this to be public static or can I just make a method for it
    public static bool unitsAdded = false;

    /// <summary>
    /// A list of all objects that we'll try "sending", that way we can handle things a little better
    /// </summary>
    //GameObject[] units = new GameObject[30];


    public bool moving = false;


    /// <summary>
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;
    }

    /// <summary>
    /// Called when script is loaded and when an editor value has been changed
    /// </summary>
    private void OnValidate() {


        // update our hwy light settings


        // update our sphere light settings
        if (lowSphereLight) {
            lowSphereLight.transform.position = sphereLightPosition;
        }


        // Update our clock settings
        if (clock) {
            clock.transform.position = ClockPosition;
        }


        // Update our Maze generation size
        mazeSize = new IntVector2(mazeX, mazeZ);

        if (mazeManager) {
            mazeManager.GetComponent<VisualizerMazeManager>().mazeSize = mazeSize;
        }


    }

    /// <summary>
    /// Called once per frame, calculate speed once conductor ready,
    /// 
    /// Trigger moving flags on our lights every beat
    /// </summary>
    void Update() {

        // SET-UP ---------------------------------------------------------------------------------
        // If our conductor isn't ready to visualize, don't do anything
        if (!Conductor.musicSourceReadyForVisualizing) {
            return;
        }

        // Now that the Visualizer is ready, 
        // add all of our visualizer units
        if (!unitsAdded) {
            AddVisualizerUnits();
            unitsAdded = true;

            return;
        }

        // ----------------------------------------------------------------------------------------

        // If we don't have any mazes generating, start our maze generation coroutine
        if (useMaze && !mazeManager.GetComponent<VisualizerMazeManager>().mazesGenerating) {
            mazeManager.GetComponent<VisualizerMazeManager>().StartMazeGenerationCoroutine();
        }



        // If we've passed enough time to send a HwyLight, SEND IT
        if (useHwyLight && Conductor.songPositionInSeconds - lastTimeHwyLightSent >= (Conductor.secondsPerBeat / hwyLightsPerBeat)) {
            //Debug.Log("Send light?");

            // Keep track of the last time we sent a HwyLight
            lastTimeHwyLightSent = Conductor.songPositionInSeconds;

            SendHwyLight();
        }

        // If we've passed enough time to send a Maze, SEND IT
        if (useMaze && Conductor.songPositionInSeconds - lastTimeMazeSent >= (Conductor.secondsPerBeat / mazesPerBeat)) {
            //Debug.Log("Send maze");

            // Keep track of the last time we sent a HwyLight
            lastTimeMazeSent = Conductor.songPositionInSeconds;

            SendMaze();
        }

    }



    /// <summary>
    /// Instantiate all of the Visualizer units that we'll be using
    /// </summary>
    public void AddVisualizerUnits() {

        // Get our Mazes ready
        if (useMaze) {
            //Debug.Log("Creating mazes");

            mazeManager = Instantiate(vMazeManagerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            mazeManager.transform.parent = transform;

            mazeManager.GetComponent<VisualizerMazeManager>().Initialize(mazeSize, mazeRegenerationRate);
            mazeManager.GetComponent<VisualizerMazeManager>().InitializeMazes(vMazePrefab, mazeStartPosition, mazeEndPosition, Vector3.forward, mazeStartRotation, mazeSize);


        }

        // Get our Sphere Light ready
        if (useSphere) {
            Debug.Log("Creating Spheres");

            // Lets create three spheres

            lowSphereLight = Instantiate(sphereLightPrefab.GetComponent<SphereLight>()) as SphereLight;
            lowSphereLight.Initialize(this, sphereLightPosition, 0);

            midSphereLight = Instantiate(sphereLightPrefab.GetComponent<SphereLight>()) as SphereLight;
            midSphereLight.Initialize(this, sphereLightPosition + new Vector3(0f, 10f, 0f), 1);

            highSphereLight = Instantiate(sphereLightPrefab.GetComponent<SphereLight>()) as SphereLight;
            highSphereLight.Initialize(this, sphereLightPosition + new Vector3(0f, 20f, 0f), 2);


        }

        // Get our HwyLights ready
        if (useHwyLight) {

            //Debug.Log("Creating " + ((hwyLightsPerBeat * 4) + 1) + " HwyLights");

            // Make 17 HwyLights (16th notes + 1 extra), since we might change the send rate for these
            // lights at any moment, and we'll need them all instantiated beforehand
            hwyLights = new GameObject[(hwyLightsPerBeat * 4) + 1];

            for (int b = 0; b < hwyLights.Length; b++) {
                hwyLights[b] = Instantiate(hwyLightPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                hwyLights[b].GetComponent<HwyLight>().Initialize(this, sender.position, receiver.position, Vector3.back);
                hwyLights[b].transform.parent = transform;


            }

        }

        // Get our Clock Ready
        if (UseClock) {
            //Debug.Log("Creating Clock");

            clock = Object.Instantiate(clockPrefab, ClockPosition, Quaternion.identity) as GameObject;
            clock.transform.parent = transform;

        }


    }

    /// <summary>
    /// Called by Visualizer
    /// Set "moving" flags on all HwyLights
    /// </summary>
    void SendHwyLight() {
        // Debug.Log("Sending HwyLight " + currentHwyLight);

        if (hwyLightIndex >= hwyLights.Length - 1) {
            hwyLightIndex = 0;
        }

        hwyLights[hwyLightIndex].GetComponent<HwyLight>().moving = true;
        hwyLightIndex++;

    }

    /// <summary>
    /// Set "moving" flags on our mazes
    /// </summary>
    void SendMaze() {

        if (mazeIndex >= mazeManager.GetComponent<VisualizerMazeManager>().mazeObjects.Length - 1) {
            mazeIndex = 0;
        }

        mazeManager.GetComponent<VisualizerMazeManager>().SetMazeMoving(mazeIndex);

    }

}
