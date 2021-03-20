using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorScale : Scale {
    /* From wikipedia
     * One of the melodic major scales,
     * the Ionian mode, is a diatonic scale
     * made of seven ntoes, the eigth duplicating
     * the first at double its frequency
     * [for optimization sake, should we cache the 8th note or calculate it?]
     * 
     * "two identical tetrachords separated by a whole
     * tone. Each tetrachord consists of two whole tones
     * follwed by a semitone (Whole-Whole-Half pattern)"
     * 
     * 
     */
    MusicNote[] scale;

    // We need a base note to "define" the scale
    // ex. D
    // This will probably be something defined in the base class
    public MajorScale(MusicNote note) : base(note) {

        //tonic = note; // is this already taken care of?
        //noteMask |= note.ToNoteMask();
        // base.ToString is an example of this constructor use
        scale = new MusicNote[7];
        InitializeScale(note);
        

    }

    public void InitializeScale(MusicNote note) {
        // Major scale intervals
        // W-W-H-W-W-W-H
        scale[0] = note;
        scale[1] = scale[0].GetWholeStepUp();
        scale[2] = scale[1].GetWholeStepUp();
        scale[3] = scale[2].GetHalfStepUp();
        scale[4] = scale[3].GetWholeStepUp();
        scale[5] = scale[4].GetWholeStepUp();
        scale[6] = scale[5].GetWholeStepUp();
    }
    
    /// <summary>
    /// Get our music note array for this scale
    /// </summary>
    /// <returns></returns>
    public MusicNote[] GetScaleNotes() {
        return scale;
    }

    /// <summary>
    /// Return a random note in our scale
    /// </summary>
    /// <returns></returns>
    public MusicNote GetRandomNote() {
        return scale[Random.Range(0, scale.Length)];
    }

    public MusicNote[] GetMajorTriad() {

        MusicNote[] triad = new MusicNote[3];

        triad[0] = scale[0];
        triad[1] = scale[2];
        triad[2] = scale[4];

        return triad;
    }

    public override string ToString() {

        string result = tonic + " Major Scale: ";


        for (int a = 0; a < scale.Length; a++) {
            result += scale[a] + "-";
        }

        return result;
    }

}
