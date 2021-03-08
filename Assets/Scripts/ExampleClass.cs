using UnityEngine;
using System.Collections;

// Copy meshes from children into the parent's Mesh.
// CombineInstance stores the list of meshes.  These are combined
// and assigned to the attached Mesh.

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class ExampleClass : MonoBehaviour {

    MeshFilter[] meshFilters;
    CombineInstance[] combine;

    void Start() {
        
    }

    void CombineMeshes() {
        // Get MeshFilters from children into an array
        meshFilters = GetComponentsInChildren<MeshFilter>();

        // Create a new combine instance array of the same size
        combine = new CombineInstance[meshFilters.Length];

        Debug.Log("Combine instance array length: " + combine.Length);

        // Go through our meshes and put em in our combined mesh array
        int i = 0;
        while (i < meshFilters.Length) {
            // Get the shared mesh from our child and put it in our combined mesh array
            combine[i].mesh = meshFilters[i].sharedMesh;

            // set the transform the same position as it was before
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;

            // De-activate the gameobjects we took meshfilters from
            meshFilters[i].gameObject.SetActive(false);

            i++;
        }

        // Create a new mesh for this object
        transform.GetComponent<MeshFilter>().mesh = new Mesh();


        Debug.Log("Combine instance array length: " + combine.Length);

        //Combine our meshes from the combineinstance array we made
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);


        Debug.Log("Combine instance array length: " + combine.Length);

        // Set this object to active
        transform.gameObject.SetActive(true);
    }

}