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
    //GameObject[] MainOscillators = new GameObject[10];
    List<Oscillator> TenOscillators = new List<Oscillator>();


    public GameObject oscillatorPrefab;

    InputKeyboardFeedback feedback;

    /// <summary>
    /// How loud it is basically, but not really
    /// moves between -1 and 1?
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float gain = 0.19f;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float sustainLevel = 0.15f;

    /// <summary>
    /// The amount of time it takes for our oscillator to reach
    /// the desired gain
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    public float attackTime = 0.1f;

    /// <summary>
    /// The amount of time it takes for our oscillator to reach
    /// from initial attack to a "SAT sustain level" (i guess our regular gain);
    /// as in go to sustain level gain immediately or after some time
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    public float decayTime = 1f;

    /// <summary>
    /// Controls time it takes the initial amplitude of the sound to decay
    /// from the sustain level after the key is being released. Setting to 
    /// 0 will make the sound stop immediately after releasing the key, while
    /// higher values leave the sound playing for a time. Infinite releases
    /// are also an option, find out how to make that
    /// </summary>
    [SerializeField, Range(0f, 10f)]
    public float releaseTime = 1f;

    //Time.realtimeSinceStartup is a useful tool

    /// <summary>
    /// A list representing keyboard input for the synth, which is the letter keys and some extra ;',./[] in there
    /// </summary>
    List<KeyboardKeysFromA4> myKeys;

    /// <summary>
    /// How many half steps up or down we want our key to play, normally with Z being A0.
    /// 88 Key range, considering that's how long typical keyboards are
    /// </summary>
    [SerializeField, Range(-88, 88)]
    public int keyOffset = 0;

    [SerializeField, Range(-440f, 440f)]
    public float frequencyOffset = 0f;

    /// <summary>
    /// We have 10 fingers, so the max oscillators we want is probably 10
    /// I might be able to optimize this later on and use a single oscillator to output all 10
    /// finger's signals, but that'll come later
    /// </summary>
    private readonly int OscillatorCount = 10;

    /// <summary>
    /// The current waveform the oscillator uses
    /// </summary>
    public Waveforms currentWaveform = Waveforms.CustomADSRWave;

    /// <summary>
    /// 
    /// </summary>
    void Start() {

        feedback = GameObject.Find("FeedbackText").GetComponent<InputKeyboardFeedback>();
        myKeys = feedback.myKeys;



        for (int a = 0; a < OscillatorCount; a++) {

            GameObject temp = Instantiate(oscillatorPrefab, Vector3.zero, Quaternion.identity, transform);

            TenOscillators.Add(temp.GetComponent<Oscillator>());

        }




    }

    /// <summary>
    /// 
    /// </summary>
    void Update() {

        // Play all the keys in myKeys, then turn off all other oscillators
        int keyIndex = 0;

        for (int a = 0; a < TenOscillators.Count; a++) {

            // If we have a key to play,
            if (keyIndex < myKeys.Count) {
                // Play the next available key and go to the next oscillator and key.

                
                TenOscillators[a].SetDesiredGain(gain);
                TenOscillators[a].SetSustainGain(sustainLevel);
                TenOscillators[a].SetWaveform(currentWaveform);
                TenOscillators[a].SetAttackTime(attackTime);
                TenOscillators[a].SetDecayTime(decayTime);
                TenOscillators[a].SetReleaseTime(releaseTime);


                TenOscillators[a].SetFrequency(GetETFrequencyFromPianoKey((PianoKeys)myKeys[a] + keyOffset) + frequencyOffset);
                TenOscillators[a].RequestStartPlay(GetETFrequencyFromPianoKey((PianoKeys) myKeys[a] + keyOffset) + frequencyOffset);

                keyIndex++;

            } else {
                // Otherwise, turn off this and any other remaining oscillators
                TenOscillators[a].RequestEndPlay();
            }

        }

    }

    /// <summary>
    /// 
    /// </summary>
    public void IncrementFrequencyOffset() {
        //Debug.Log("incrementing offset");
        frequencyOffset += .5f;
    }

    /// <summary>
    /// 
    /// </summary>
    public void DecrementFrequencyOffset() {
        //Debug.Log("Decrementing offset");
        frequencyOffset -= .5f;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="offset"></param>
    public void AddToFrequencyOffset(float offset) {

        frequencyOffset += offset;

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
