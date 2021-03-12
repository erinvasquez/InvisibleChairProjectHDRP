using System.Collections;
using UnityEngine;

/// <summary>
/// Things to do:
/// - add, divide, multiply, average different waveforms together
/// - Add sliders/knobs to control multipliers to frequency/mix ratios
///   of different waveforms
/// - use rng to select new frequencies after certain periods of time
/// - make more sounds
/// </summary>
public class Oscillator : MonoBehaviour {

    /// <summary>
    /// A frequency array for 88 piano keys
    /// </summary>
    private float[] equalTempramentFrequencies;

    /// <summary>
    /// In Hz, the note made by this sine
    /// wave oscillator
    /// </summary>
    [SerializeField]
    double frequency = 440.0;

    /// <summary>
    /// 
    /// </summary>
    private double increment;

    /// <summary>
    /// Stays between 0 and 2pi
    /// 
    /// Used as a float value sometimes
    /// </summary>
    private double phase;

    /// <summary>
    /// Unity defaults to 48,000 
    /// </summary>
    private double sampling_frequency = 48000.0;

    /// <summary>
    /// How loud it is basically, but not really
    /// </summary>
    float gain;
    float volume;

    [Range(0f, 120f)]
    public float lerpDurationSlider = 10f;
    public float lerpDuration;

    [SerializeField]
    public Waveforms waveform = Waveforms.SinWave;
    Waveforms currentWaveform;

    private float t = 0;

    private void Start() {

        // Get our frequency array calculated
        equalTempramentFrequencies = InitializeEqualTempramentFrequencies();
        currentWaveform = waveform;
        lerpDuration = lerpDurationSlider;

    }

    private void OnValidate() {

        // Get our frequency array calculated
        equalTempramentFrequencies = InitializeEqualTempramentFrequencies();
        currentWaveform = waveform;
        lerpDuration = lerpDurationSlider;

    }

    public void StartPlay(float freq, float vol) {
        frequency = freq;
        gain = vol;
    }

    public void StartPlay(HalfStepsFromA4 key, float vol) {
        SetFrequency(key);
        gain = vol;
    }

    public void EndPlay() {
        gain = 0;
    }

    public void SetFrequency(HalfStepsFromA4 key) {
        SetFrequency(equalTempramentFrequencies[(int)key]);
    }

    public void SetFrequency(float f) {
        frequency = f;
    }

    public double getFrequency() {
        return frequency;
    }

    /// <summary>
    /// A coroutine to change our frequency to a certain note
    /// 
    /// </summary>
    /// <param name="noteA"></param>
    /// <param name="noteB"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator LerpToNote(int noteA, int noteB, float time) {

        if (frequency == equalTempramentFrequencies[noteB]) {
            t = 0;
            frequency = equalTempramentFrequencies[0];
        }

        // Using time.deltaTime and our own t value gives us more control of the interpolation length, since
        // its clamped by [0,1]
        t += Time.deltaTime / time; // divide t, our interpolator by duration to make it last our duration
        frequency = Mathf.Lerp(equalTempramentFrequencies[noteA], equalTempramentFrequencies[noteB], t);

        // this line signals "the point at which execution will pause and be resumed the following frame"
        yield return null;
    }

    /// <summary>
    /// Called in update (or a coroutine?)
    /// </summary>
    IEnumerator SweepPianoFrequencies() {

        if (frequency == equalTempramentFrequencies[equalTempramentFrequencies.Length - 1]) {
            t = 0;
            frequency = equalTempramentFrequencies[0];
        }

        // Using time.deltaTime and our own t value gives us more control of the interpolation length, since
        // its clamped by [0,1]
        t += Time.deltaTime / lerpDurationSlider; // divide t, our interpolator by duration to make it last our duration
        frequency = Mathf.Lerp(equalTempramentFrequencies[0], equalTempramentFrequencies[equalTempramentFrequencies.Length - 1], t);

        yield return null;
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

        // how much to increment the phase
        increment = frequency * 2 * Mathf.PI / sampling_frequency;

        for (int a = 0; a < data.Length; a += channels) {

            phase += increment;

            // This right here is where our Sin Wave function makes our sound data
            // Replace this with whatever wave type you can find

            switch (currentWaveform) {
                case Waveforms.SinWave:
                    data[a] = GetSinWaveform();
                    break;

                case Waveforms.SquareWave:
                    data[a] = GetSquareWaveform();
                    break;

                case Waveforms.TriangleWave:
                    data[a] = GetTriangleWaveform();
                    break;
                case Waveforms.SawtoothWave:
                    data[a] = GetSawWaveform();
                    break;
                case Waveforms.HarshSawtoothWave:
                    data[a] = GetHarshSawWaveform();
                    break;
                default:
                    data[a] = 0;
                    break;
            }

            // if we have two channels...
            if (channels == 2) {
                data[a + 1] = data[a];
            }

            // Set phase to 0 when its 2 * pi
            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }

        }

    }

    // Converts our frequency to angular velocity w (or omega)
    double AngularVelocity(double hertz) {
        return hertz * 2.0 * Mathf.PI;
    }

    /// <summary>
    /// Gets a sinewave
    /// </summary>
    /// <returns></returns>
    float GetSinWaveform() {
        return (float)(gain * Mathf.Sin((float) phase));
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

    float GetSawWaveform() {
        // y = 2A/pi * (sigma n = 1 to s (-sin((n) * f * 2 * pi * x)) / n)
        // where S is the number of sin waves
        // A is amplitude
        // f is the frequency
        // OR

        double dOutput = 0.0;

        for (double n = 1.0; n < 100.0; n++) {
            dOutput += (GetSinWaveform() * frequency) / n;
        }

        return (float) dOutput * (2.0f / Mathf.PI);
    }

    float GetHarshSawWaveform() {
        // y= 2A/pi * (f * pi mod(1.0/f) - pi/2)


        return (2.0f / Mathf.PI) * ((float) frequency * (float) Mathf.PI * ((float) phase % (1.0f / (float) frequency) - ((float) Mathf.PI / 2.0f)));

        

    }

    /// <summary>
    /// Initializes an array of frequencies
    /// in A-440 Equal Temprement tuning
    /// </summary>
    /// <returns></returns>
    float[] InitializeEqualTempramentFrequencies() {
        float[] frequencies = new float[88]; // 88 key piano, 52 white, 36 black
        int currentNote = (int)HalfStepsFromA4.C0;

        for (int a = 0; a < frequencies.Length; a++) {

            frequencies[a] = GetEqualTempramentFrequency(currentNote);

            currentNote++;

        }


        return frequencies;
    }

    /// <summary>
    /// Optimize this later
    /// </summary>
    /// <param name="note">Number of half steps away from A4-440hz,
    /// with higher notes being positive and lower notes being negative</param>
    /// <returns></returns>
    public float GetEqualTempramentFrequency(int note) {
        // formula is fn = f0 * (a)^n
        // where fn is our note's frequency
        // f0 is our fixed note frequency (A4-440Hz)
        // a is the ratio 1/12
        int aForForty = 440;

        // Number of half steps (or piano keys) away from our fixed note frequency (A-440Hz here)
        // Higher notes positive, lower notes are negative
        float a = Mathf.Pow(2f, (float)(1f / 12f)); // approx 1.059463094359...

        //Debug.Log("a is " + a.ToString("0.00000000") + "\n. Shoud be approx 1.059463094...");

        // Wavelength of a sound is Wn = c/fn
        //int c = 345; // speed of sound in m/s


        return ((float) aForForty * Mathf.Pow(a, (float)note));
    }

}
