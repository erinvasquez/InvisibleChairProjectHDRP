using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Controls a Canvas TMP_Text component
/// that shows our the played note's frequency
/// </summary>
public class FrequencyDisplay : MonoBehaviour {
    TMP_Text text;
    Oscillator oscillator;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TMP_Text>();
        oscillator = GameObject.Find("LocalTheremin").GetComponent<Oscillator>();
    }

    private void Update() {
        text.text = "Frequency: " + oscillator.GetFrequency().ToString("0.00") + " Hz";
    }

}