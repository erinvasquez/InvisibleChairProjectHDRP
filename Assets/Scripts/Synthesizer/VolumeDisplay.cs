﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeDisplay : MonoBehaviour {
    TMP_Text text;
    Oscillator oscillator;

    private void Start() {
        text = GetComponent<TMP_Text>();
        oscillator = GameObject.Find("LocalTheremin").GetComponent<Oscillator>();
    }

    private void Update() {
        text.text = "Volume: " + oscillator.GetDesiredGain().ToString("0.00") + "%\n"
            + oscillator.GetdB().ToString("0.0") + " dB";

    }


}
