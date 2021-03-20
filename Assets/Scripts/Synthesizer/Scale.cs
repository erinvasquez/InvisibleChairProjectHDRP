using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scale {

    // Define a base note
    // internal means we can access it from
    // inheriting classes
    internal MusicNote tonic;
    /// <summary>
    /// A binary "note mask" that
    /// tells us which notes on the 12-note
    /// chromatic scale we want to include in our scale
    /// </summary>
    internal int noteMask = 0; // all flags empty, no notes in our scale

    /*
     * Major Scale (from Wikipedia) ---
     * Called Ionian mode, is a diatonic scale
     * made of seven ntoes, the eigth duplicating
     * the first at double its frequency
     * 
     * Base note of a scale is called a "tonic"
     * 
     * "two identical tetrachords separated by a whole
     * tone. Each tetrachord consists of two whole tones
     * follwed by a semitone (Whole-Whole-Half pattern)"
     * 
     * Scale degrees are
     * 1st - tonic
     * 2nd - Supertonic
     * 3rd - Mediant
     * 4th - Subdominant
     * 5th - Doiminant
     * 6th - Submediant
     * 7th - Leading tone
     * 8th - Tonic
     * 
     * Triads can be built using the above scale degrees.
     * 1st - major triad (i)
     * 2nd - minor triad (ii)
     * 3rd - minor triad (iii)
     * 4th - Major triad (IV)
     * 5th - major triad (V)
     * 6th - minor triad (VI)
     * 7th - diminished triad (vii0) 
     * 
     * Circle of Fifths
     * Major: C, F, Bb, Eb, Ab, Db, Gb, Cb, Fb
     *        C, G, D, A, E, B, F#, C#, G#
     * Minor: C, D, D, A, E, B, F#, C#, G#
     *        a, e, b, f#, c#, g#, d#, a#, e#
     * 
     * 
     * Diatonic notes are notes considered to be in the scale used,
     * while chromatic notes are notes that AREN'T in the scale used
     * 7 pitches will be diatonic, and 5 others will be chromatic
     * 
     * A key signature will generally reflact the accidentals in
     * the corresponding major scale" 
     * 
     * Harmonic major scale has a minor sixth, differs from harmonic
     * minor scale by only raising the third degree
     * 
     * There are TWO Melodic major scales:
     * 1 - the fifth mode of the Jazz Minor Scale, which
     *      is the major scale (ionian mode) with a lowered
     *      sixth and seventh degree, OR the natural minor scale
     *      (Aeolian mode) with a raised third
     * 2 - the combined scale that goes as ionian ascending and as
     * the previous melodic major descending. It differes from 
     * melodic minor scale only by raising the third degree to a
     * major third.
     * 
     * The double harmonic major scale has a minor second and a minor
     * sixth. It's the fifth mode of the Hungarian minor scale
     * 
     * Other scalse include:
     * - chromatic scale, twelve notes
     * - the whole-tone scale, six notes
     * - pentatonic scale (five notes)
     * - octatonic/diminished scales (eight notes)
     * - Phrygian dominant scale (a mode of the harmonic minor scale)
     * - octatonic/diminished scales (eight notes)
     * - Arabic scalse
     * - Hungarian minor Scale
     * - Byzantine music scales (echoi)
     * - The persian scale
     * 
     * Scales may also be identified by using a binary system
     * of twelve 0s or 1s to represent each of the twelve notes
     * on a chromatic scale
     * 2048 different possible species, but only 351 unique scales
     * containing 1 to 12 notes
     * 
     * Blue notes are in the middle of two semitones, and can only
     * be done on a trombone, theremin, etc
     * 
     * Minor Scales -----------------------
     * - Natural Minor Scale (Aeolian mode) - starts on the 6th
     *      degree of the relative major scale, creating a
     *      "relative minor", moves up W-H-W-W-H-W-W
     * - Harmonic Minor Scale - a natural minor scale with a raised
     *      7th degree (so it starts on the 6th of the rel. M scale too).
     *      Cam also be made by lowering the 3rd and 6th degress of the
     *      parallel major scale one semitone, intervals W-H-W-W-H-a7-H
     * - Melodic Minor Scale
     *      Is actually two scales, one ascending and the other descending
     *      Ascending melodic minor scale (heptatonia seconda, jazz minor scale,
     *      or Ionian b3)
     *      Descending melodic minor scale is just the natural minor scale8
     * 
     */

    public Scale(MusicNote tonicNote) {
        tonic = tonicNote;
        noteMask |= tonic.ToNoteMask();
    }

    public MusicNote GetTonic() {
        return tonic;
    }

    public int ToNoteMask() {
        return noteMask;
    }

}
