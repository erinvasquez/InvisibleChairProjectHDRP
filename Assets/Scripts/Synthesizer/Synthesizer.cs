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
    /// The amount of time it takes for our oscillator to reach
    /// the desired gain
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    public float attackTime = 0.1f;

    /// <summary>
    /// The amount of time it takes for our oscillator to reach
    /// from initial attack to a "SAT sustain level" (i guess our regular gain);
    /// as in go to sustain level gain immediately or after some time
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    public float decayTime = 0.1f;

    /// <summary>
    /// Controls the level os sound that's held until the key is released.
    /// No sustain creates plucks and percussive noises, while peak sustain
    /// should give you pad sounds. TODO: Modulating this paramter can create
    /// some interesting sound textures.
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    public float sustainTime = 0.1f;

    /// <summary>
    /// Controls time it takes the initial amplitude of the sound to decay
    /// from the sustain level after the key is being released. Setting to 
    /// 0 will make the sound stop immediately after releasing the key, while
    /// higher values leave the sound playing for a time. Infinite releases
    /// are also an option, find out how to make that
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    public float releaseTime = 0.1f;

    //Time.realtimeSinceStartup is a useful tool

    /// <summary>
    /// A list representing keyboard input for the synth, which is the letter keys and some extra ;',./[] in there
    /// </summary>
    List<KeyboardKeysFromA4> myKeys;

    /// <summary>
    /// Index for the next empty oscillator
    /// </summary>
    public int oscIndex = 0;

    public int OscillatorCount = 10;

    private void Start() {

        feedback = GameObject.Find("FeedbackText").GetComponent<InputKeyboardFeedback>();
        myKeys = feedback.myKeys;



        for (int a = 0; a < OscillatorCount; a++) {

            GameObject temp = Instantiate(oscillatorPrefab, Vector3.zero, Quaternion.identity, transform);

            TenOscillators.Add(temp.GetComponent<Oscillator>());

        }

    }

    void Update() {

        // Play all the keys in myKeys, then turn off all other oscillators
        int keyIndex = 0;

        for (int a = 0; a < TenOscillators.Count; a++) {

            // If we have a key to play,
            if (keyIndex < myKeys.Count) {
                // Play the next available key and go to the next oscillator and key.

                TenOscillators[a].SetDesiredGain(gain);
                TenOscillators[a].RequestStartPlay(GetETFrequencyFromPianoKey((PianoKeys)myKeys[a]));

                keyIndex++;

            } else {
                // Otherwise, turn off this and any other remaining oscillators
                TenOscillators[a].RequestEndPlay();
            }


            /*
            // The key isn't empty, and isn't currently playing
            if (myKeys[a] != (KeyboardKeys) (-1) && !TenOscillators[a].isCurrentlyPlaying) {
                // Set an oscillator the play this key
                TenOscillators[a].SetDesiredGain(gain);
                TenOscillators[a].RequestStartPlay( GetETFrequencyFromPianoKey((PianoKeys) myKeys[a]) );

            } else if (myKeys[a] != (KeyboardKeys)(-1) && TenOscillators[a].isCurrentlyPlaying) {
                // The key isn't empty, and the oscillator is playing

                //TenOscillators[a].RequestStartPlay(GetETFrequencyFromPianoKey((PianoKeys)myKeys[a]));

            } else if (myKeys[a] == (KeyboardKeys)(-1)) {
                // The key is empty

                Debug.Log("The key is empty");
                TenOscillators[a].RequestEndPlay();
            }
            */

        }

    }

    /// <summary>
    /// Add an oscillator to play this key
    /// </summary>
    /// <param name="keyboardKey"></param>
    public void KeyPerformed(string keyboardKey) {

        KeyboardKeysFromA4 theKey = (KeyboardKeysFromA4)Enum.Parse(typeof(KeyboardKeysFromA4), keyboardKey);

        TenOscillators[oscIndex].RequestStartPlay(GetETFrequencyFromPianoKey((PianoKeys)theKey));
        oscIndex++;

    }

    public void KeyCanceled(string key) {

        KeyboardKeysFromA4 theKey = (KeyboardKeysFromA4)Enum.Parse(typeof(KeyboardKeysFromA4), key);


        // Find the oscillator playing our key and end it
        for (int a = 0; a < TenOscillators.Count; a++) {

            if ((double)TenOscillators[a].currentPitch.equalTemperamentfrequency == (double)GetETFrequencyFromPianoKey((PianoKeys)theKey)) {

                TenOscillators[a].RequestEndPlay();

            }

        }

        oscIndex--;

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
