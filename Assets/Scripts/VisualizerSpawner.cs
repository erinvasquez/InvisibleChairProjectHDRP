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

    [Header("Prefab")]
    public GameObject HwyLightPrefab;
    public GameObject SphereLightPrefab;
    public GameObject VisualizerMazePrefab;
    public GameObject ClockPrefab;

    [Header("Highway Lights")]
    public bool UseHwyLight = true;
    [Range(1, 16)]
    public int HwyLightsPerBeat = 1;
    int HwyLightIndex = 0; // Current HwyLight
    GameObject[] HwyLights;
    float lastTimeHwyLightSent = 0f; // the last time in seconds that we sent a HwyLight


    [Header("Sphere Light")]
    public bool UseSphere = true;
    public Vector3 SphereLightPosition = new Vector3(-15f, 5f, 20f);
    [Range(1, 1000)]
    public int SphereIntensityMultiplier = 1000;
    SphereLight sphereLight;


    [Header("Maze")]
    /// <summary>
    /// True when we want to initialize our maze
    /// </summary>
    public bool UseMaze = true;
    /// <summary>
    /// An array of mazes (when we use it)
    /// </summary>
    GameObject[] mazes;
    private VisualizerMaze mazeInstance;
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
    public Vector3 mazeStartPosition = new Vector3(0f, 10f, 50f);
    /// <summary>
    /// Default maze rotation
    /// </summary>
    public Vector3 mazeStartRotation = new Vector3(30f, 0f, 0f);


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
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;
    }

    /// <summary>
    /// Called when script is loaded and when an editor value has been changed
    /// </summary>
    private void OnValidate() {
        mazeSize = new IntVector2(mazeX, mazeZ);
    }

    /// <summary>
    /// Called once per frame, calculate speed once conductor ready,
    /// 
    /// Trigger moving flags on our lights every beat
    /// </summary>
    void Update() {

        // SET-UP ---------------------------------------------------------------------------------
        // If our conductor isn't ready to visualize, don't do anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // Now that the Visualizer is ready, 
        // add all of our visualizer units
        if (!unitsAdded) {
            AddVisualizerUnits();
            unitsAdded = true;

            return;
        }

        // If we've passed enough time to send a HwyLight, SEND IT
        if (Conductor.songPositionInSeconds - lastTimeHwyLightSent >= (Conductor.secondsPerBeat / HwyLightsPerBeat)) {
            //Debug.Log("Send light?");

            // Keep track of the last time we sent a HwyLight
            lastTimeHwyLightSent = Conductor.songPositionInSeconds;

            SendHwyLight();
        }

        if (mazeInstance.MazeGenerated) {
            RestartMazeGeneration();
        }

    }



    /// <summary>
    /// Instantiate all of the Visualizer units that we'll be using
    /// </summary>
    public void AddVisualizerUnits() {

        // Instantiate all of our Visualizer Units
        // Get our Mazes ready
        if (UseMaze) {
            Debug.Log("Creating maze");

            mazeSize = new IntVector2(mazeX, mazeZ);
            CreateMaze(mazeSize);
        }

        // Get our Sphere Light ready
        if (UseSphere) {
            Debug.Log("Creating Sphere");

            sphereLight = Instantiate(SphereLightPrefab.GetComponent<SphereLight>()) as SphereLight;
            sphereLight.Initialize(this, SphereLightPosition);

        }

        // Get our HwyLights ready
        if (UseHwyLight) {

            //Debug.Log("Creating " + ((HwyLightsPerBeat * 4) + 1) + " HwyLights");
            // Make 17 HwyLights (16th notes + 1 extra), since we might change the send rate for these
            // lights at any moment, and we'll need them all instantiated beforehand
            HwyLights = new GameObject[17];

            for (int b = 0; b < HwyLights.Length; b++) {
                HwyLights[b] = Instantiate(HwyLightPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                HwyLights[b].GetComponent<HwyLight>().Initialize(this, sender.position, receiver.position, Vector3.forward);
            }

        }

        // Get our Clock Ready
        if (UseClock) {
            Debug.Log("Creating Clock");

            clock = Object.Instantiate(ClockPrefab, ClockPosition, Quaternion.identity) as GameObject;
            clock.transform.parent = transform;

        }


    }

    /// <summary>
    /// Called by Visualizer
    /// Set "moving" flags on all HwyLights
    /// </summary>
    void SendHwyLight() {
        // Debug.Log("Sending HwyLight " + currentHwyLight);

        if (HwyLightIndex >= HwyLights.Length - 1) {
            HwyLightIndex = 0;
        }

        HwyLights[HwyLightIndex].GetComponent<HwyLight>().moving = true;
        HwyLightIndex++;

    }

    /// <summary>
    /// Instantiate and Initialize our maze(s)
    /// </summary>
    private void CreateMaze(IntVector2 mazeSize) {

        // If we already have a mazeInstance, get rid of it and any coroutines it started
        if (mazeInstance) {
            StopAllCoroutines();
            Destroy(mazeInstance.gameObject);
        }

        mazeInstance = Instantiate(VisualizerMazePrefab.GetComponent<VisualizerMaze>()) as VisualizerMaze;

        mazeInstance.transform.parent = transform;
        mazeInstance.transform.position = mazeStartPosition;
        mazeInstance.transform.Rotate(new Vector3(30f, 0f, 0f));
        mazeInstance.size = mazeSize;


        // This would work if the number of steps needed to generate a maze was dependent on its size,
        // but it's not considering the algorithm is pretty random
        // Time to generate a maze is Conductor.secondsPerBeat * 16 beats
        // Divide that by the amount of cells, and you get the amount of time to make each one of those cells

        mazeInstance.generationStepDelay = (Conductor.secondsPerBeat * 16) / (mazeSize.x * mazeSize.z);

        StartCoroutine(mazeInstance.Generate(mazeSize));

    }

    /// <summary>
    /// Restart our Maze generation
    /// 
    /// Stop any coroutines, destroy our maze instance,
    /// and create a new maze instance
    /// </summary>
    private void RestartMazeGeneration() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        CreateMaze(mazeSize);
    }

}
