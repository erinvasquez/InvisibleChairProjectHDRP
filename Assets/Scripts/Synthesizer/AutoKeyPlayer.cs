using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Oscillator))]
public class AutoKeyPlayer : MonoBehaviour {

    [SerializeField]
    public MajorScale currentScale;
    Oscillator oscillator;
    [SerializeField]
    public MusicNote currentNote;

    [Range (0f, 1f)]
    public float volume = 0.2f;

    /// <summary>
    /// Time in seconds between notes changing
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
            oscillator.SetVolume(volume);
            oscillator.StartPlay();

            yield return new WaitForSeconds(waitTime);
        }

        
    }
}
