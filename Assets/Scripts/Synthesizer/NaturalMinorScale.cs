using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalMinorScale : Scale {

    public NaturalMinorScale(MusicNote note) : base(note) {
        // starts on the 6th
        // degree of the relative major scale, creating a
        // "relative minor", moves up W - H - W - W - H - W - W
        //tonic = note;
        //noteMask |= note.ToNoteMask();
    }


    public override string ToString() {
        return tonic + "Natural Minor Scale";
    }

}
