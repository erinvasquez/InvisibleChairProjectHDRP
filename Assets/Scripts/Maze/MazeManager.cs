using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

/// <summary>
/// Manages our Maze's functions
/// </summary>
public class MazeManager : MonoBehaviour {
    [SerializeField]
    Vector3 position = new Vector3(0f, 0.5f, 0f);
    [SerializeField]
    Vector3 rotation = new Vector3(0f, 0f, 0f);
    [SerializeField]
    public Vector3 scale = new Vector3(5f, 5f, 5f);
    [SerializeField]
    public IntVector2 mazeSize = new IntVector2(20, 20);
    [SerializeField]
    float genStepDelay;
    [SerializeField, Range(0f, 1f)]
    float doorProbability;

    public GameObject mazePrefab;
    private GameObject maze;

    MeshFilter[] meshFilters;
    CombineInstance[] combine;
    bool meshesCombined = false;
    public Material mazeMaterial;


    /// <summary>
    /// Called before Update()
    /// </summary>
    void Start() {

        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        GenerateMaze();
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {


        if (maze.GetComponent<Maze>().mazeGenerated && !meshesCombined) {
            CombineMeshes();
            meshesCombined = true;
        }

    }

    /// <summary>
    /// Generates our maze via coroutines
    /// </summary>
    private void GenerateMaze() {

        maze = Instantiate(mazePrefab) as GameObject;

        maze.GetComponent<Maze>().Initialize(position, rotation, scale, genStepDelay, doorProbability);
        maze.transform.parent = transform;

        StartCoroutine(maze.GetComponent<Maze>().Generate(mazeSize));
    }

    /// <summary>
    /// Set up our environment to re-generate our maze
    /// </summary>
    private void RestartGeneration() {
        StopAllCoroutines();
        Destroy(maze);
        GenerateMaze();
    }

    void CombineMeshes() {
        // Get MeshFilters from children ONLY into an array
        //meshFilters = GetComponentsInChildren<MeshFilter>();
        meshFilters = new MeshFilter[0];

        // By going through our children explicitly, we avoid getting the mesh filter
        // from this parent gameobject
        for (int a = 0; a < transform.childCount; a++) {
            MeshFilter[] filters = transform.GetChild(a).GetComponentsInChildren<MeshFilter>();

            meshFilters = meshFilters.Concat(filters).ToArray();

        }
        

        // Create a new combine instance array of the same size
        combine = new CombineInstance[meshFilters.Length];

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

        //Combine our meshes from the combineinstance array we made
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        //Debug.Log("Combine instance array length: " + combine.Length);

        // Set this object to active
        transform.gameObject.SetActive(true);

        // Give it a collider too for our player movement to detect
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        MeshRenderer meshr = GetComponent<MeshRenderer>();

        meshc.sharedMesh = transform.GetComponent<MeshFilter>().mesh;
        meshr.material = mazeMaterial;

        // Unity usually does this automatically, but we generated this mesh
        transform.GetComponent<MeshFilter>().mesh.Optimize();

    }

}
