using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// "A set of all pitches that a whole
/// number of octaves apart"
/// </summary>
[SerializeField]
public class MusicNote {

    /// <summary>
    /// Our note's letter name, ex.
    /// "DSharp" or "F". We're keeping everything to sharps for now,
    /// since using flats later can be confusing
    /// </summary>
    public SharpNotes noteName;

    /// <summary>
    /// Current octave for this note
    /// </summary>
    public int octave;
    
    /// <summary>
    /// The frequency for this pitch at the current octave
    /// </summary>
    public float equalTemperamentfrequency;

    public MusicNote(SharpNotes name, int octaveNumber) {
        noteName = name;
        octave = octaveNumber;

        equalTemperamentfrequency = GetETFrequency();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pitch"></param>
    /// <returns></returns>
    public bool IsGreaterThan(MusicNote pitch) {

        // if our octave is greater, the frequency is greater
        if (octave > pitch.octave) {
            return true;
        } else if (octave == pitch.octave) {
            // else if the octave is the same,
            // we're only greater if our note name is

            if (noteName > pitch.noteName) {
                return true;
            }

        }

        // If our octave is less than our observed one, we're definitely not greater

        return false;
    }

    /// <summary>
    /// to calculate our Equal Temperament frequency,
    /// we need to ifnd how many half steps from A4
    /// we are
    /// </summary>
    /// <returns></returns>
    public HalfStepsFromA4 GetHalfStepsFromA4() {

        return (HalfStepsFromA4) System.Enum.Parse(typeof (HalfStepsFromA4), noteName.ToString().ToUpper() + octave.ToString());

    }

    /// <summary>
    /// Get the music note 1 Half Step
    /// (1 semitone) up
    /// </summary>
    /// <returns></returns>
    public MusicNote GetHalfStepUp() {
        int note = (int) noteName;
        int oct = octave;

        // If this note is SharpNotes.GSHARP, loop back to A in the next octabe
        if (note == (int) SharpNotes.GSHARP) {
            
            note = (int)SharpNotes.A;
            oct++;
        } else {
            // Just up our note and keep the octave
            note++;
        }

        return new MusicNote((SharpNotes) note, oct);
    }

    /// <summary>
    /// Get the music note 1 whole step,
    /// or 2 Half steps (semitones) up
    /// </summary>
    /// <returns></returns>
    public MusicNote GetWholeStepUp() {
        int note = (int)noteName;
        int oct = octave;

        // If this note is SharpNotes.GSHARP, up the octave
        if (noteName == SharpNotes.GSHARP) {
            note = (int)SharpNotes.ASHARP;
            oct++;
        } else if (noteName == SharpNotes.G) {
            // G too, since we move two half steps
            note = (int)SharpNotes.A;
            oct ++;

        } else {
            // upping this note by 2 wont affect octaves of notes under G and GSharp
            note += 2;
        }

        return new MusicNote((SharpNotes)note, oct);
    }

    /// <summary>
    /// Gets equal temprament frequency for this pitch
    /// with an octave of 0
    /// </summary>
    /// <param name="steps">Number of half steps away from A4</param>
    /// <returns></returns>
    public float GetETFrequency() {
        int aForForty = 440;

        float a = Mathf.Pow(2f, (1f / 12f));


        return (float) aForForty * Mathf.Pow(a, (float) GetHalfStepsFromA4());
    }

    /// <summary>
    /// Get the MIDI note number
    /// </summary>
    /// <returns></returns>
    public float GetP() {
        return 9 + (12 * Mathf.Log(equalTemperamentfrequency, 2f) / 440f);
    }

    public string GetName() {
        return noteName.ToString();
    }

    public override string ToString() {


        return noteName.ToString();
    }

    /// <summary>
    /// Get this note as a SHARP note mask
    /// </summary>
    /// <returns></returns>
    public int ToNoteMask() {
        // Get the bit to represent this note
        // A is 0, so should represent
        // 0b100000000000
        int result = (int) System.Enum.Parse(typeof(SharpNoteBits), noteName.ToString());

        return result;
    }

}
