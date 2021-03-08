using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Waveforms {
    SinWave,
    SquareWave,
    TriangleWave
};

public enum PianoKeys {
    C0 = -57,
    C0Sharp = -56,
    D0 = -55,
    D0Sharp = -54,
    E0 = -53,
    F0 = -52,
    F0Sharp = -51,
    G0 = -50,
    G0Sharp = -49,
    A0 = -48,
    A0Sharp = -47,
    B0 = -46,
    C1 = -45,
    C1Sharp = -44,
    D1 = -43,
    D1Sharp = -42,
    E1 = -41,
    F1 = -40,
    F1Sharp = -39,
    G1 = -38,
    G1Sharp = -37,
    A1 = -36,
    A1Sharp = -35,
    B1 = -34,
    C2 = -33,
    C2Sharp = -32,
    D2 = -31,
    D2Sharp = -30,
    E2 = -29,
    F2 = -28,
    F2Sharp = -27,
    G2 = -26,
    G2Sharp = -25,
    A2 = -24,
    A2Shar = -23,
    B2 = -22,
    C3 = -21,
    C3Sharp = -20,
    D3 = -19,
    D3Sharp = -18,
    E3 = -17,
    F3 = -16,
    F3Sharp = -15,
    G3 = -14,
    G3Sharp = -13,
    A3 = -12,
    A3Sharp = -11,
    B3 = -10,
    C4 = -9,
    C4Sharp = -8,
    D4 = -7,
    D4Sharp = -6,
    E4 = -5,
    F4 = -4,
    F4Sharp = -3,
    G4 = -2,
    G4Sharp = -1,
    A4 = 0,
    A4Sharp,
    B4,
    C5,
    C5Sharp,
    D5,
    D5Sharp,
    E5,
    F5,
    F5Sharp,
    G5,
    G5Sharp,
    A5,
    A5Sharp,
    B5,
    C6,
    C6Sharp,
    D6,
    D6Sharp,
    E6,
    F6,
    F6Sharp,
    G6,
    G6Sharp,
    A6,
    A6Sharp,
    B6,
    C7,
    C7Sharp,
    D7,
    D7Sharp,
    E7,
    F7,
    F7Sharp,
    G7,
    G7Sharp,
    A7,
    A7Sharp,
    B7,
    C8,
    C8Sharp,
    D8,
    D8Sharp,
    E8,
    F8,
    F8Sharp,
    G8,
    G8Sharp,
    A8,
    A8Sharp,
    B8
};



/// <summary>
/// Things to do:
/// - add, divide, multiply, average different waveforms together
/// - Add sliders/knobs to control multipliers to frequency/mix ratios
///   of different waveforms
/// - use rng to select new frequencies after certain periods of time
/// - make more sounds
/// </summary>
public class Oscillator : MonoBehaviour {
    
    public float[] equalTempramentFrequencies;



    /// <summary>
    /// In Hz, the note made by this sine
    /// wave oscillator
    /// </summary>
    [Range(16.35f, 7902.13f)]
    public double frequency = 440.0;

    /// <summary>
    /// 
    /// </summary>
    private double increment;

    /// <summary>
    /// Used as a float value sometimes
    /// </summary>
    private double phase;

    /// <summary>
    /// Default 48,000 in unity
    /// </summary>
    private double sampling_frequency = 48000.0;

    /// <summary>
    /// How loud it is basically, but not really
    /// </summary>
    [Range(0f, 2f)]
    public float gain;

    public float volume = 0.1f;

    public KeyCode playKey = KeyCode.Space;

    private int currentFreq;

    private void Start() {


        equalTempramentFrequencies = InitializeEqualTempramentFrequencies();


    }

    private void Update() {

        if (Input.GetKeyDown(playKey)) {

            gain = volume;
            frequency = equalTempramentFrequencies[currentFreq];

            currentFreq += 1;
            currentFreq %= equalTempramentFrequencies.Length;

        }

        if (Input.GetKeyUp(playKey)) {

            gain = 0;

        }


    }

    /// <summary>
    /// Called on the audio thread, (not the main one)
    /// Unity uses this to "insert a custom filter
    /// into the audio DSP chain". I'm guessing that means
    /// we make audio happen.
    /// 
    /// "Called every time a chunk of audio is sent to the filter
    /// (~20ms or so depending on sample rate and platform), an array of floats
    /// from [-1f,1f]" "Can procedurally generate audio"
    /// </summary>
    /// <param name="data"></param>
    /// <param name="channels"></param>
    private void OnAudioFilterRead(float[] data, int channels) {

        increment = frequency * 2 * Mathf.PI / sampling_frequency;

        for (int a = 0; a < data.Length; a += channels) {

            phase += increment;

            // This right here is where our Sin Wave function makes our sound data
            // Replace this with whatever wave type you can find
            // data[a] = GetSinWave();
            data[a] = GetSquareWaveform();



            // if we have two channels...
            if (channels == 2) {
                data[a + 1] = data[a];
            }

            // Set phase to 0 when...
            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }

        }

    }

    /// <summary>
    /// Gets a sinewave
    /// </summary>
    /// <returns></returns>
    float GetSinWaveform() {
        return (float)(gain * Mathf.Sin((float)phase));
    }

    /// <summary>
    /// "Sounds like ann old nintendo"
    /// </summary>
    /// <returns></returns>
    float GetSquareWaveform() {

        if (gain * GetSinWaveform() >= 0 * gain) {
            return (float)gain * 0.6f;
        } else {
            return (-(float)gain) * 0.6f;
        }

    }

    /// <summary>
    /// "Squeaky and Quacky"
    /// Gets a triangle wave
    /// </summary>
    /// <returns></returns>
    float GetTriangleWaveform() {

        return (float)(gain * (double)Mathf.PingPong((float)phase, 1.0f));

    }

    /// <summary>
    /// Initializes an array of frequencies
    /// in A-440 Equal Temprement tuning
    /// </summary>
    /// <returns></returns>
    float[] InitializeEqualTempramentFrequencies() {
        float[] frequencies = new float[88]; // 88 key piano, 52 white, 36 black
        int currentNote = (int) PianoKeys.C0;

        for (int a = 0; a < frequencies.Length; a++) {

            frequencies[a] = GetEqualTempramentFrequency(currentNote);

            currentNote++;

        }


        return frequencies;
    }

    /// <summary>
    /// Optimize this
    /// </summary>
    /// <param name="note"></param>
    /// <returns></returns>
    float GetEqualTempramentFrequency(int note) {
        // formula if fn = f0 * (a)^n
        int aForForty = 440;

        // Number of half steps (or piano keys) away from our fixed note frequency (A-440Hz here)
        // Higher notes positive, lower notes are negative
        int n = note;
        float a = Mathf.Pow(2f, (float)(1f / 12f)); // approx 1.059463094359...

        //Debug.Log("a is " + a.ToString("0.00000000") + "\n. Shoud be approx 1.059463094...");

        // Wavelength of a sound is Wn = c/fn
        int c = 345; // speed of sound in m/s


        return (float) (aForForty * Mathf.Pow(a, (float)n) );
    }

}
