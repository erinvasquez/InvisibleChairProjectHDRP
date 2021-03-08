using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {

    public AudioSource hit;


    private void OnTriggerEnter(Collider other) {

        //Debug.Log("Hit");
        
        //hit.Play();
    }
}
