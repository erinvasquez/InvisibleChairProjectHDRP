using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrequencyDisplay : MonoBehaviour {
    TMP_Text text;
    Oscillator oscillator;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TMP_Text>();
        oscillator = GameObject.Find("Oscillator").GetComponent<Oscillator>();
    }

    private void Update() {
        text.text = "Frequency: " + oscillator.getFrequency().ToString("0.00") + " Hz";
    }

}