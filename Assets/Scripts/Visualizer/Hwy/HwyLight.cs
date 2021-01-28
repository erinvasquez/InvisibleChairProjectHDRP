using UnityEditor;
using UnityEngine;
public class HwyLight : MonoBehaviour {


    HwyLightSpawner spawner;


    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;
    public bool moving = false;



    /// <summary>
    /// Cache our game objects references
    /// </summary>
    private void Start() {
    }


    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {

        // Make sure the visualizer is ready to run before doing anything
        if (!Conductor.visualizerReady) {
            return;
        }

        // If this is the first time we update, make sure we're in the right spot first
        // DO THIS HERE

        // If we've passed the player,
        // Place our light at the back of the line again, and stop it from moving
        if (transform.position.z >= endPos.z) {
            StopLight();
        }
        
        // If the light is supposed to be moving...
        if (moving) {
            // Move it forward enough for 1 update
            MoveLight();
        }

        
    }


    public void Initialize() {
        spawner = GameObject.Find("HwyLightSpawner").GetComponent<HwyLightSpawner>();

        startPos = spawner.sender.transform.position;
        endPos = spawner.sender.transform.position; 
        direction = Vector3.forward;

        transform.parent = spawner.transform;
        transform.position = spawner.transform.position;
    }

    public void Initialize(HwyLightSpawner hwyLightSpawner) {
        spawner = hwyLightSpawner;

        startPos = spawner.transform.position + new Vector3(0,0,-30);
        endPos = new Vector3(0, 0, 530);
        direction = Vector3.forward;

        transform.parent = spawner.transform;
        transform.position = startPos;
    }

    public void Initialize(HwyLightSpawner hwyLightSpawner, Vector3 startPosition, Vector3 endPosition, Vector3 movementVector) {
        spawner = hwyLightSpawner;

        startPos = startPosition;
        endPos = endPosition;
        direction = movementVector;

        transform.parent = hwyLightSpawner.transform;
        transform.position = startPos;
    }

    /// <summary>
    /// Called during update, moves the light forward
    /// </summary>
    public void MoveLight() {
        transform.Translate(direction * spawner.speed * Time.deltaTime);
    }

    /// <summary>
    /// Called in update to stop moving light forward during update
    /// </summary>
    public void StopLight() {
        moving = false;
        transform.position = startPos;
        //transform.Translate(Vector3.zero);
    }
}
