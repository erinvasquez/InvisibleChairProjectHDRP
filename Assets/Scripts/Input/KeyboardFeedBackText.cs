using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyboardFeedBackText : MonoBehaviour {

    // Our TMP text object
    public TextMeshProUGUI feedbackText;
    [SerializeField]
    string text = "";

    private void Awake() {

        // get our text ready
        feedbackText = GetComponent<TextMeshProUGUI>();

    }

    private void Start() {
        feedbackText.text = "CURRENT INPUT ";
    }

    public void SetInput(string name) {
        feedbackText.text = "CURRENT INPUT: " + name;
    }


}
