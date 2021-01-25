using UnityEditor;
using UnityEngine;
public class HwyLight : MonoBehaviour {


    HwyLightSpawner spawner;

    GameObject leftCylinderLight;
    GameObject rightCylinderLight;
    GameObject leftPointLight;
    GameObject rightPointLight;


    Vector3 startPos;
    Vector3 endPos;
    Vector3 direction;
    public bool moving = false;
    public float intensity = 0.1f;

    int sampleCount;
    Color hwyLightColor;
    Material cylinderMaterial;



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

        // Control our light's intensity
        // Get an average of all samples to figure out how intense we gotta be
        /*
        sampleCount = AudioPeer._samples.Length;
        intensity = 0;

        for (int a = 0; a < sampleCount; a++) {
            intensity += AudioPeer._samples[a];
        }

        intensity /= sampleCount;
        intensity = Mathf.Clamp(intensity * 100000, 1f, 100f);
        */

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

        startPos = spawner.transform.position;
        endPos = new Vector3(0,0,500);
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
