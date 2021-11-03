using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 
/// </summary>
public class NoteChooser : MonoBehaviour {

    ThereminPlayer theremin;
    Oscillator oscillator;
    
    TMP_Dropdown maxNameDropdown;
    TMP_Dropdown minNameDropdown;
    TMP_Dropdown maxOctaveDropdown;
    TMP_Dropdown minOctaveDropdown;

    int maxNoteName;
    int minNoteName;
    int maxNoteOctave;
    int minNoteOctave;

    public MusicNote maxNote;
    public MusicNote minNote;

    private void Start() {


        theremin = GetComponent<ThereminPlayer>();
        oscillator = GetComponent<Oscillator>();

        maxNameDropdown = GameObject.Find("MaxNoteName Dropdown").GetComponent<TMP_Dropdown>();
        minNameDropdown = GameObject.Find("MinNoteName Dropdown").GetComponent<TMP_Dropdown>();
        maxOctaveDropdown = GameObject.Find("MaxNoteOctave Dropdown").GetComponent<TMP_Dropdown>();
        minOctaveDropdown = GameObject.Find("MinNoteOctave Dropdown").GetComponent<TMP_Dropdown>();

        maxNameDropdown.value = (int) theremin.GetHighNote().noteName;
        minNameDropdown.value = (int)theremin.GetLowNote().noteName;
        maxNoteName = (int) theremin.GetHighNote().noteName;
        minNoteName = (int) theremin.GetLowNote().noteName;


        maxOctaveDropdown.value = theremin.GetHighNote().octave;
        minOctaveDropdown.value = theremin.GetLowNote().octave;
        maxNoteOctave = theremin.GetHighNote().octave;
        minNoteOctave = theremin.GetLowNote().octave;


    }

    /// <summary>
    /// Called by our dropdown, gets the 
    /// int that we chose
    /// </summary>
    /// <param name="name"></param>
    public void SetMaxNoteName(int name) {
        maxNoteName = name;

    }

    /// <summary>
    /// Called by our dropdown, gets the int that we chose
    /// </summary>
    /// <param name="octave"></param>
    public void SetMaxNoteOctave(int octave) {
        maxNoteOctave = octave;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void SetMinNoteName(int name) {
        minNoteName = name;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="octave"></param>
    public void SetMinNoteOctave(int octave) {
        minNoteOctave = octave;
    }

    public void SetNotes() {
        maxNote = new MusicNote((SharpNotes)maxNoteName, maxNoteOctave);
        minNote = new MusicNote((SharpNotes)minNoteName, minNoteOctave);

        /*
        if (maxNoteOctave < minNoteOctave) {
            Debug.LogError("Max octave " + maxNoteOctave + "is smaller than miinimum octave" + minNoteOctave);
            return;
        } else if (maxNoteName == minNoteName && maxNoteOctave == minNoteOctave) {
            Debug.LogError("Max note " + maxNoteName + maxNoteOctave + " is the same as our minimum note " + minNoteName + minNoteOctave);
            return;
        } else if (maxNoteName <= minNoteName && maxNoteOctave <= minNoteOctave) {
            Debug.LogError("Max note isn't the larger fequency");
            return;
        }*/

        if (maxNote.equalTemperamentfrequency <= minNote.equalTemperamentfrequency) {
            Debug.LogError(maxNote + " isn't larger than " + minNote);
            return;
        }

        /*
        if (minNote.IsGreaterThan(maxNote)) {
            Debug.LogError("Max note isn't the larger fequency");
            return;
        }*/


        theremin.SetHighNote(maxNote);
        theremin.SetLowNote(minNote);

        Debug.Log("Set notes to " + maxNote + " and " + minNote);

    }


}
