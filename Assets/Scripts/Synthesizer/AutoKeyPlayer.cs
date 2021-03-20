using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Oscillator))]
public class AutoKeyPlayer : MonoBehaviour {

    MajorScale currentScale;
    Oscillator oscillator;
    public MusicNote currentNote;



    public float waitTime;


    public double BPM = 85.0;
    
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

        
        //StartCoroutine(myCoroutine);

    }


    private IEnumerator NoteCoroutine() {

        while(true) {

            Debug.Log("NoteCoroutine: " + (int) Time.time);

            currentNote = currentScale.GetRandomNote();
            oscillator.StartPlay(currentNote.GetETFrequency(), 1f);

            yield return new WaitForSeconds(waitTime);
        }

        
    }
}
