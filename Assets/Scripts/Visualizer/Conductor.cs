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
    public static Conductor instance; // declares this a Singleton, our ONLY Conductor

    /// <summary>
    /// Scene's AudioSource
    /// </summary>
    public static AudioSource musicSource;

    /// <summary>
    /// Beats in one measure/bar
    /// Time signature top number.
    /// </summary>
    [Header("Time Signature")]
    public static int beatsPerMeasure = 4;
    /// <summary>
    /// Inverse of this value determines which note is 1 beat.
    /// Time signature bottom number.
    /// Default 4, 1/4 ("quarter") note.
    /// </summary>
    public static int inverseOneBeat = 4;


    public static int beatsPerMinute = 0; // Calculated from AudioPeer.BPM
    public static float secondsPerBeat;    // a calculated ratio
    public static float beatsPerLoop;      // a calculated ratio


    [Header("Playback Position")]
    public static float dspSongTime; // [Set this to] Unity Audio system time
    public static float songPositionInSeconds; // total play time in seconds
    public static float songPositionInBeats;   // total play time in beats
    public static float loopPositionInBeats;   // song time in beats for the current loop
    public static float loopPositionInAnalog;  // song time in seconds for the current loop
    public static int completedLoops = 0; // Starting at 0, the number of times we've completed the song


    /// <summary>
    /// Time in seconds from beginning of music file to first actual beat.
    /// Depends on the song, and is manual so far.
    /// </summary>
    [Header("Custom Variables")]
    public float firstBeatOffset = 0.026f; // 0.026f for 150 AM, 1.296 for humble
    /// <summary>
    /// The number of beats before starting music and visualizer interactions
    /// Default 1 measure/bar.
    /// </summary>
    public int countdownMeasures = 1; // The number of measures before starting music

    [Header("Status")]
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

        /* We need to change this conditional to a "Music Analyzed" being true condition,
         * where the song's loaded, analyzed, BPM and all, and every other script and object dependent
         * on music is ready to play, waiting for THE CONDUCTOR
         * 
         * Once we are, we're going to want to [loop the main menu] music AFTER introducing it after [4] beats.
         * This give us a chance to spawn an entire measure of Bar Lights that will arrive at the player on the first beat.
         * 
         */
        if (!visualizerReady) {
            
            if (AudioPeer.audioPeerReady) {
                beatsPerMinute = AudioPeer.BPM;
                secondsPerBeat = 60f / beatsPerMinute; 
                beatsPerLoop = AudioPeer.trackLength / secondsPerBeat;

                conductorReady = true;
            }

            if (conductorReady && AudioPeer.audioPeerReady && !musicSource.isPlaying) {
                //Debug.Log("Conductor and AudioPeer ready, playing");
                dspSongTime = (float)AudioSettings.dspTime;

                visualizerReady = true;

                musicSource.Play(); // Set a button to press this later
            }

            return;
        }




        // Update our song position
        // Current Unity Audio time - Unity AudioTime
        songPositionInSeconds = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        songPositionInBeats = songPositionInSeconds / secondsPerBeat;


        // Take care of any loops that might happen (like in our main menu music)
        if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop) {
            completedLoops++;
        }

        loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;
        loopPositionInAnalog = loopPositionInBeats / beatsPerLoop;
        //dspSongTime = (float)AudioSettings.dspTime;
        //Debug.Log("AudioSystemTime: " + AudioSettings.dspTime +"\nDSP Audio Time: " + dspSongTime + "\nSongPositionInSeconds: " + songPositionInSeconds);

    }

}
