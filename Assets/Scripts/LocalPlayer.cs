using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour {

    public bool instantiatePlayerModel;
    public GameObject playerModelPrefab;
    public Vector3 spawnLocation = new Vector3(0,0,0);

    /// <summary>
    /// 
    /// </summary>
    private void Start() {

        // Decide whether or not to spawn our player object (like if we're in the main menu or non-gameplay)
        if (instantiatePlayerModel) {

            GameObject obj = Instantiate(playerModelPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            obj.transform.parent = this.gameObject.transform;

        }

    }
}
