using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerascriptt : MonoBehaviour {
    public Vector3 leftT;
    public Vector3 rightT;
    public Vector3 bothT;
    public Vector3 leftR;
    public Vector3 rightR;
    public Vector3 bothR;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            transform.position = leftT;
            transform.rotation = Quaternion.Euler(leftR);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = rightT;
            transform.rotation = Quaternion.Euler(rightR);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = bothT;
            transform.rotation = Quaternion.Euler(bothR);
        }
    }
}
