using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonicMinorScale : Scale {

    public HarmonicMinorScale(MusicNote note) : base(note) {
        // tonic = note
        // Make an array of MusicNotes
        // Get the NaturalMinorScale generated
        // notes, then just put the 7th degree up one semitone
        //tonic = note;
        //noteMask |= note.ToNoteMask();
    }

    public override string ToString() {
        return tonic + " Harmonic Minor Scale";
    }

}
