﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Functions the same as real conductor. Reads and Analyzes the music the game is playing,
/// and keeps track of time and visualizer interactions.
/// 
/// AudioPeer and any Visualizer objects should be scripted to interact with the conductor as a [Musician]
/// part of our [band].
/// </summary>
public class Conductor : MonoBehaviour {

    /// <summary>
    /// Singleton Conductor
    /// </summary>
    public static Conductor instance;

    /// <summary>
    /// Scene's AudioSource
    /// </summary>
    public static AudioSource musicSource;

    // Our static variables that other scripts have access to.
    // Most of these are timing related values

    /// <summary>
    /// Calculated in Update()
    /// </summary>
    public static float secondsPerBeat;
    /// <summary>
    /// Calculated in Update()
    /// </summary>
    public static float BeatsPerLoop;

    /// <summary>
    /// Unity Audio system time when audio playback begins
    /// </summary>
    public static float dspSongTime;
    /// <summary>
    /// Total play time in seconds
    /// </summary>
    public static float songPositionInSeconds;
    /// <summary>
    /// Total play time in beats
    /// </summary>
    public static int songPositionInBeats;
    /// <summary>
    /// Song time in beats for the current loop
    /// </summary>
    public static float loopPositionInBeats;
    /// <summary>
    /// Song time in seconds for the current loop
    /// </summary>
    public static float loopPositionInAnalog;
    /// <summary>
    /// Completed loops for the current song.
    /// </summary>
    public static int completedLoops = 0;

    [SerializeField, Range(0.1f, 2f)]
    public float delay = 0.1f;

    // BEGIN INPUT BPM DETECTION
    public float timeSinceLastBeatInput;

    /// <summary>
    /// Time in seconds from beginning of music file to first actual beat.
    /// Depends on the song, and is manual so far.
    /// </summary>
    [Header("Custom Variables")]
    public float firstBeatOffset = 0;// 0.026f; // 0.026f for 150 AM, 1.296 for humble
    /// <summary>
    /// The number of beats before starting music and visualizer interactions
    /// Default 1 measure/bar.
    /// </summary>
    public int countdownMeasures = 0; // The number of measures before starting music

    /// <summary>
    /// Uses the old conductor update loop, and plays music from an audio source
    /// </summary>
    public bool getBPMFromMusic = false;
    /// <summary>
    /// Uses the new conductor update loop, which gets BPM from an 
    /// </summary>
    public bool getBPMFromInput = true;

    public static bool conductorReady = false;
    public static bool visualizerReady = false;

    private void Awake() {
        instance = this;


        if (getBPMFromMusic) {
            // Figure out best way to optimize this
            // Make some more stuff static?
            musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        } else if (getBPMFromInput) {

            Debug.Log("Get BPMFromInput true");

        }

    }

    // Start is called before the first frame update
    void Start() {

        dspSongTime = (float)AudioSettings.dspTime;

    }

    // Update is called once per frame
    void Update() {

        if (getBPMFromMusic) {

            // Since we can't get the BPM without letting it run for a split second, I've put this set up code here
            // Find the best place to run all of this without ruining any timing or optimization.
            if (!visualizerReady) {

                MusicVisualizerSetUp();

                return;
            }

            // Update our song position variables
            UpdateMusicVariables();

        } else if (getBPMFromInput) {
            // Use our new system that gets BPM from the user pressing a button
            // one or more times

        }



    }

    /// <summary>
    /// Set up the visualizer before doing the rest of Update()
    /// </summary>
    private void MusicVisualizerSetUp() {

        if (AudioPeer.audioPeerReady) {
            secondsPerBeat = 60f / AudioPeer.BPM;
            BeatsPerLoop = AudioPeer.trackLength / secondsPerBeat;

            conductorReady = true;
        }

        if (conductorReady && AudioPeer.audioPeerReady && !musicSource.isPlaying) {
            //Debug.Log("Conductor and AudioPeer ready, playing");
            dspSongTime = (float)AudioSettings.dspTime;
            visualizerReady = true;
            musicSource.Play(); // Set a button to press this later
        }

    }

    /// <summary>
    /// As our song plays, update our timing variables
    /// 
    /// 
    /// </summary>
    private void UpdateMusicVariables() {
        // Current Unity Audio time - Unity AudioTime
        songPositionInSeconds = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = (int)(songPositionInSeconds / secondsPerBeat);


        // Take care of any loops that might happen (like in our main menu music)
        if (songPositionInBeats >= (completedLoops + 1) * BeatsPerLoop) {
            completedLoops++;
        }

        loopPositionInBeats = songPositionInBeats - (completedLoops * BeatsPerLoop);
        loopPositionInAnalog = loopPositionInBeats / BeatsPerLoop;
        //dspSongTime = (float)AudioSettings.dspTime;
        //Debug.Log("AudioSystemTime: " + AudioSettings.dspTime +"\nDSP Audio Time: " + dspSongTime + "\nSongPositionInSeconds: " + songPositionInSeconds);

    }

}
