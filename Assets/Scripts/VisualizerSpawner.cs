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

    [Header("Prefab")]
    public GameObject HwyLightPrefab;
    public GameObject SphereLightPrefab;
    public GameObject VisualizerMazePrefab;
    public GameObject ClockPrefab;

    [Header("Highway Lights")]
    public bool UseHwyLight = true;
    [Range(1, 16)]
    public int HwyLightsPerBeat = 1;
    public int HwyLightIndex = 0; // Current HwyLight
    GameObject[] HwyLights;


    [Header("Sphere Light")]
    public bool UseSphere = true;
    public Vector3 SphereLightPosition = new Vector3(-15f, 5f, 20f);
    [Range(1, 1000)]
    public int SphereIntensityMultiplier = 1000;
    SphereLight sphereLight;


    [Header("Maze")]
    public bool UseMaze = true;
    [Range(2, 100)]
    public int size = 20;
    GameObject[] mazes;
    private VisualizerMaze mazeInstance;
    IntVector2 mazeSize;


    [Header("Clock")]
    public bool UseClock = true;
    GameObject clock;
    public Vector3 ClockPosition = new Vector3(0f, 10f, 100f);


    [Header("Visualizer Stuff")]
    public int beat = 1;



    bool unitsAdded = false;


    /// <summary>
    /// Called once script instance is loaded
    /// </summary>
    private void Awake() {
        instance = this;

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

        // ----------------------------------------------------------------------------------------

        // If we're in the next beat
        if (beat <= (int)Conductor.songPositionInBeats) {
            // Trigger moving flags for ALL our lights
            SendLights();
        }

    }

    /// <summary>
    /// Instantiate all of the Visualizer units that we'll be using
    /// </summary>
    public void AddVisualizerUnits() {

        // Instantiate all of our Visualizer Units
        
        // Get our Mazes ready
        if(UseMaze) {
            Debug.Log("Creating maze");

            CreateMaze(new IntVector2(size, size));
        }

        // Get our Sphere Light ready
        if (UseSphere) {
            Debug.Log("Creating Sphere");

            sphereLight = Instantiate(SphereLightPrefab.GetComponent<SphereLight>()) as SphereLight;
            sphereLight.Initialize(this, SphereLightPosition);

        }

        
        if (UseHwyLight) {
            Debug.Log("Creating" + (Conductor.beatsPerMeasure + 1 ) + " HwyLights");

            HwyLights = new GameObject[Conductor.beatsPerMeasure + 1];

            for (int b = 0; b < HwyLights.Length; b++) {
                HwyLights[b] = Instantiate(HwyLightPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                HwyLights[b].GetComponent<HwyLight>().Initialize(this, sender.position, receiver.position, Vector3.forward);
            }

        }


        if (UseClock) {
            Debug.Log("Creating Clock");

            clock = Object.Instantiate(ClockPrefab, ClockPosition, Quaternion.identity) as GameObject;
            clock.transform.parent = transform;

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

        // Send our lights
        SendHwyLights();

    }




    /// <summary>
    /// Set "moving" flags on all HwyLights
    /// </summary>
    void SendHwyLights() {
        // Debug.Log("Sending HwyLight " + currentHwyLight);
        HwyLights[HwyLightIndex].GetComponent<HwyLight>().moving = true;
    }

    /// <summary>
    /// Instantiate and Initialize our maze(s)
    /// </summary>
    private void CreateMaze(IntVector2 mazeSize) {

        mazeInstance = Instantiate(VisualizerMazePrefab.GetComponent<VisualizerMaze>()) as VisualizerMaze;

        mazeInstance.transform.parent = transform;
        mazeInstance.transform.position = new Vector3(0f, 10f, 30f);
        mazeInstance.transform.Rotate(new Vector3(30f, 0f, 0f));

        StartCoroutine(mazeInstance.Generate(mazeSize));

    }

    /// <summary>
    /// Stop any coroutines
    /// </summary>
    private void RestartGeneration() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        CreateMaze(mazeSize);
    }

}
