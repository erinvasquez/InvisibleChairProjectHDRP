using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Let's have 10 Oscillators working on this thing
/// for additive synthesis
/// 
/// for AM synthesis, you need an LFO and a carrier osc
/// 
/// Frequency Modulation Synthesis
/// modulator and carrier, but controls frequency of carrier osc instead of gain
/// 
/// Hard Sync, master osc and slave osc
/// 
/// </summary>

/// <summary>
/// We need to control oscillators based on the keys we press
/// 
/// Z will be the lowest, with P being the highest note on the keyboard
/// If every frame, we check to see what keys are being played, and play the
/// corresponding number of oscs for pressed keys, at the requested frequencies
/// </summary> 


///<summary>
/// A synthesizer with 10 main oscillators,
/// some modulators, carriers
/// 
///</summary>
public class Synthesizer : MonoBehaviour {

    /// <summary>
    /// Since we have 10 fingers, we have 10 main oscillators
    /// </summary>
    GameObject[] MainOscillators = new GameObject[10];
    public GameObject oscillatorPrefab;

    InputKeyboardFeedback feedback;

    public float gain = .25f;

    private void Start() {

        feedback = GameObject.Find("FeedbackText").GetComponent<InputKeyboardFeedback>();

        for (int a = 0; a < MainOscillators.Length; a++) {
            MainOscillators[a] = Instantiate(oscillatorPrefab, Vector3.zero, Quaternion.identity, transform);
        }

    }

    private void Update() {

        KeyboardKeys[] currentKeys = feedback.currentKeys;
        Oscillator currentOscillator;

        // For each one of the keys we're pressing, prepare an oscillator to play the requested frequency
        for (int a = 0; a < currentKeys.Length; a++) {

            currentOscillator = MainOscillators[a].GetComponent<Oscillator>();

            // If it's not an empty key
            if (currentKeys[a] != (KeyboardKeys)(-1)) {

                // Get the correct frequency that we're playing
                // and set the current oscillator to play the requested frequency
                currentOscillator.StartPlay(GetETFrequencyFromPianoKey((PianoKeys)currentKeys[a]), gain);

            } else {
                // Otherwise this key isn't being played and we should end the oscillator at this index
                MainOscillators[a].GetComponent<Oscillator>().EndPlay();
            }


        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public float GetETFrequencyFromPianoKey(PianoKeys key) {
        int aForForty = 440;

        float a = Mathf.Pow(2f, (1f / 12f));


        return (float)aForForty * Mathf.Pow(a, (float)key);
    }

}
