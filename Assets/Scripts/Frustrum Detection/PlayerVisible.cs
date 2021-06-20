using UnityEngine;

public class PlayerVisible: MonoBehaviour {
    // Detects manually if obj is being seen by the main camera

    GameObject obj;
    Collider objCollider;

    Camera cam;
    Plane[] planes;

    bool isVisible = false;

    void Start() {
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        objCollider = GetComponent<CapsuleCollider>(); // used to be just <Collider>

    }

    void Update() {

        // update our frustrum planes
        planes = GeometryUtility.CalculateFrustumPlanes(cam);




        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && !isVisible) {

            isVisible = true;
            Debug.Log(this.gameObject.name + " is visible from the camera");

            GetComponent<Renderer>().material.color = new Color(0, 204, 102);


        } else if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds) && isVisible) {

            // Do nothing

        } else {

            isVisible = false;

            GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f);
        }
    }
}