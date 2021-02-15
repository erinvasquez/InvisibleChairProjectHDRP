using System.Collections;
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

    /// <summary>
    /// Time in seconds from beginning of music file to first actual beat.
    /// Depends on the song, and is manual so far.
    /// </summary>
    [Header("Custom Variables")]
    public float firstBeatOffset = 0;//0.026f; // 0.026f for 150 AM, 1.296 for humble
    /// <summary>
    /// The number of beats before starting music and visualizer interactions
    /// Default 1 measure/bar.
    /// </summary>
    public int countdownMeasures = 0; // The number of measures before starting music


    public static bool conductorReady = false;
    public static bool visualizerReady = false;

    private void Awake() {
        instance = this;

        // Figure out best way to optimize this
        // Make some more stuff static?
        musicSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start() {

        dspSongTime = (float)AudioSettings.dspTime;

    }

    // Update is called once per frame
    void Update() {

        // Since we can't get the BPM without letting it run for a split second, I've put this set up code here
        // Find the best place to run all of this without ruining any timing or optimization.
        if (!visualizerReady) {

            VisualizerSetUp();

            return;
        }

        // Update our song position variables
        UpdateVariables();

    }

    /// <summary>
    /// Set up the visualizer before doing the rest of Update()
    /// </summary>
    private void VisualizerSetUp() {

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
    private void UpdateVariables() {
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
