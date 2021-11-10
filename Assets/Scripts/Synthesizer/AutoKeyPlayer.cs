using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Script that uses an attached Oscillator
/// to play a random not from a random major scale
/// </summary>
[RequireComponent(typeof(Oscillator))]
public class AutoKeyPlayer : MonoBehaviour {

    /// <summary>
    /// The current 7 scale or key signature we're using
    /// </summary>
    [SerializeField]
    public MajorScale currentScale;

    /// <summary>
    /// Our oscillator object
    /// </summary>
    Oscillator oscillator;

    /// <summary>
    /// The current note we want our oscillator to play
    /// </summary>
    [SerializeField]
    public MusicNote currentNote;

    /// <summary>
    /// The current volume we want our oscillator to play
    /// </summary>
    [Range (0f, 1f)]
    public float currentVolume = 0.2f;

    /// <summary>
    /// Time in seconds between notes changing
    /// 
    /// Basically just secondsPerBeat if we treat this as a metronome
    /// </summary>
    private float waitTime;

    /// <summary>
    /// Beats Per Minute, or Tempo
    /// 
    /// Controls our wait time
    /// </summary>
    public double BPM = 120.0;
    
    private IEnumerator myCoroutine;

    // Start is called before the first frame update
    void Start() {
        currentScale = new MajorScale(new MusicNote(SharpNotes.D, 2));
        oscillator = GetComponent<Oscillator>();
        currentNote = currentScale.tonic;

        //oscillator.StartPlay(currentNote.GetETFrequency(), 1f);

        Debug.Log(currentScale);

        waitTime = (float) (60f / (BPM));
        
        //myCoroutine = NoteCoroutine();
        StartCoroutine(NoteCoroutine());
    }

    // Update is called once per frame
    void Update() {

    }


    private IEnumerator NoteCoroutine() {

        while(true) {

            //Debug.Log("NoteCoroutine: " + (int) Time.time);

            currentNote = currentScale.GetRandomNote();
            //oscillator.StartPlay(currentNote.GetETFrequency(), 1f);

            oscillator.SetFrequency(currentNote.GetETFrequency());
            oscillator.SetDesiredGain(currentVolume);
            oscillator.RequestStartPlay();

            yield return new WaitForSeconds(waitTime);
        }

        
    }
}
