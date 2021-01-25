
using UnityEngine;

public class Road : MonoBehaviour {

    RoadSpawner spawner;

    GameObject scaleableRoad;

    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;

    public bool moving = false;
    public float intensity = 0.1f;



    private void Awake() {
        //scaleableRoad = gameObject.transform.Find("ScaleableRoad").gameObject;
    }

    /// <summary>
    /// Cache game object references
    /// </summary>
    private void Start() {
        scaleableRoad = gameObject.transform.Find("ScaleableRoad").gameObject;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // If the conductor isn't ready, our song isn't analyzed, and this thing
        // shouldn't even exist to the user
        if (!AudioPeer.audioPeerReady || !Conductor.conductorReady) {
            return;
        }

        // If the road is supposed to be moving...
        if (moving) {
            // Move it forward enough for 1 update
            MoveRoad();
        }

        // If we've passed the player,
        // Place our road at the back of the line again, and stop it from moving
        if (transform.position.z + 20 <= endPos.z) {
            StopRoad();
        }

    }

    /// <summary>
    /// Initialize Road without parameters
    /// Staring postition set to spawner's position
    /// Ending position set to (0,0,)
    /// Direction set to "back" (works assuming spawner is in FRONT of ending position)
    /// </summary>
    public void Initialize() {
        spawner = GameObject.Find("RoadSpawner").GetComponent<RoadSpawner>();

        startPos = spawner.transform.position;
        endPos = Vector3.zero;
        direction = Vector3.back;

        transform.parent = spawner.transform;
        transform.position = spawner.transform.position;
    }

    /// <summary>
    /// Initialize with RoadSpawner parameter
    /// 
    /// Staring postition set to spawner's position
    /// Ending position set to (0,0,)
    /// Direction set to "back" (works assuming spawner is in FRONT of ending position)
    /// 
    /// </summary>
    /// <param name="roadSpawner">Parent GameObject</param>
    public void Initialize(RoadSpawner roadSpawner) {
        spawner = roadSpawner;

        startPos = spawner.transform.position;
        endPos = Vector3.zero;
        direction = Vector3.back;

        transform.parent = spawner.transform;
        transform.position = startPos;
    }

    /// <summary>
    /// Initialize with Parameters
    /// </summary>
    /// <param name="roadSpawner">Parent Game Object</param>
    /// <param name="startPosition">Starting Position of each road</param>
    /// <param name="endPosition">Ending Position of each road</param>
    /// <param name="movementVector">Direction set to "back", assuming spawner is in front of end position</param>
    public void Initialize(RoadSpawner roadSpawner, Vector3 startPosition, Vector3 endPosition, Vector3 movementVector) {
        spawner = roadSpawner;

        startPos = startPosition;
        endPos = endPosition;
        direction = movementVector;

        transform.parent = roadSpawner.transform;
        transform.position = startPosition;
    }

    /// <summary>
    /// Called during update, moves the light forward
    /// </summary>
    public void MoveRoad() {

        if (!scaleableRoad) {
            scaleableRoad = gameObject.transform.Find("ScaleableRoad").gameObject;
            scaleableRoad.gameObject.transform.localScale = new Vector3(1, 1, (int)spawner.getDistance());
        }

        transform.Translate(direction * spawner.speed * Time.deltaTime);
    }

    /// <summary>
    /// Called in update to stop moving light forward during update
    /// </summary>
    public void StopRoad() {
        moving = false;
        transform.position = startPos;
        transform.Translate(Vector3.zero);
    }

}
