using System.Collections;
using UnityEngine;

/// <summary>
/// Non-VR Oscillator
/// 
/// Plays a wave and outputs to an
/// AudioSource component on the GameObject its
/// attached to. Can function along side other 
/// GameObjects with their own AudioSource and script attached.
/// 
/// 
/// 
/// </summary>

///<summary>
/// Looking at Sebastian Lague's "...AI's Video Game Idea" video, he has this on his synth
/// Gain, Wave Type w/ a dropdown, wave frequency, attack float, release float
/// 
/// Then an info section with:
/// sample rate, num channels, buffer length, num buffers, batches per scond, num ticks, and audio time
/// 
///</summary>
///

public enum WaveformType {
    SinWave,
    TriangleWave,
    SawtoothWave,
    SinTriangleWave
};

/// Things to do:
/// - add, divide, multiply, average different waveforms together
/// - Add sliders/knobs to control multipliers to frequency/mix ratios
///   of different waveforms
/// - use rng to select new frequencies after certain periods of time
/// - make more sounds
[RequireComponent(typeof(AudioSource))]
public class Oscillator : MonoBehaviour {

    /// <summary>
    /// In Hz, the current note being produced
    /// 
    /// Kept as a double, since we dont want anything past 1 (2 would be okay, try later) decimals honestly
    /// </summary>
    [SerializeField, Range(1,1000)]
    double frequency = 440.0;

    /// <summary>
    /// Phase increment based on sampling frequency
    /// </summary>
    private double phase_increment;

    /// <summary>
    /// Stays between 0 and 2 PI,
    /// phase for our sin wave.
    /// 
    /// Is incremented every sample, at a certain frequency
    /// to create our audio data.
    /// 
    /// Used as a float value sometimes.
    /// </summary>
    private double phase;

    /// <summary>
    /// Unity defaults to 48,000 
    /// </summary>
    private readonly double sampling_frequency = 48_000.0;

    /// <summary>
    /// Gain desired by the user when playing oscillator
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    private float desired_gain = 0.2f;

    /// <summary>
    /// Actual gain value used in calculation, will be set to 0
    /// when not playing, and return to desired_gain when playing
    /// </summary>
    [SerializeField, Range(0f, 1f)]
    public float actual_gain;

    

    /// <summary>
    /// The current waveform the oscillator uses
    /// </summary>
    public Waveforms currentWaveform = Waveforms.SinWave;

    /// <summary>
    /// Default music note oscillator is set to
    /// </summary>
    public MusicNote currentPitch;

    /// <summary>
    /// True if the oscilator is playing a sound (non zero gain I guess)
    /// </summary>
    public bool isCurrentlyPlaying = false;

    /// <summary>
    /// True if the oscillator SHOULD be making a sound
    /// </summary>
    public bool shouldBePlaying = false;


    private void Start() {

        // Get our frequency array calculated
        //currentWaveform; = Waveforms.SinWave;
        currentPitch = new MusicNote(SharpNotes.D, 4); // I like D4 as our default note, just cause
        frequency = currentPitch.GetETFrequency();

        //amplitude = 0f;

        //Debug.Log("Starting frequency: " + currentPitch.noteName.ToString() + currentPitch.octave + " " + currentPitch.frequency + "Hz");

    }

    #region Instrument controls

    public void RequestStartPlay(float f, float g) {
        SetFrequency(f);
        SetDesiredGain(g);

        shouldBePlaying = true;

        Debug.Log("Requesting start play");

    }

    /// <summary>
    /// Called when we click and hold,
    /// assures that we have the right frequency and gain(/volume?)
    /// 
    /// TO-DO: Clamp our frequency and gain values to different things
    /// </summary>
    /// <param name="freq">A float value that indicates the pitch in Hz that our oscillator plays</param>
    /// <param name="g">Main synthesizer's gain value that is our amplitude</param>
    public void RequestStartPlay(float freq) {
        SetFrequency(freq);
        //SetActualGain(desired_gain);

        shouldBePlaying = true;

        Debug.Log("Requesting start play");

    }

    /// <summary>
    /// Called when we press play.
    /// Sets is Playing to true while using the previous
    /// frequency and gain
    /// </summary>
    public void RequestStartPlay() {
        //SetActualGain(desired_gain);

        shouldBePlaying = true;

        Debug.Log("Requesting start play");

    }

    /// <summary>
    /// Called when we've released our play button,
    /// sets isPlaying to false to stop our oscillator.
    /// 
    /// Also called when our synthesizer's input
    /// </summary>
    public void RequestEndPlay() {
        //SetActualGain(0f);

        shouldBePlaying = false;

        Debug.Log("Requesting end play");

    }

    #endregion

    #region oscillator getter/setters

    /// <summary>
    /// Set this oscillator's frequency
    /// </summary>
    /// <param name="f"></param>
    public void SetFrequency(float f) {
        frequency = f;
    }

    /// <summary>
    /// Return our oscillator's current frequency
    /// </summary>
    /// <returns></returns>
    public double GetFrequency() {
        return frequency;
    }

    /// <summary>
    /// Set our oscillator's desired gain level
    /// </summary>
    /// <param name="g"></param>
    public void SetDesiredGain(float g) {
        desired_gain = g;
    }

    /// <summary>
    /// Returns our oscillator's gain
    /// </summary>
    /// <returns></returns>
    public float GetDesiredGain() {
        return desired_gain;
    }
    
    /// <summary>
    /// Set the oscillator's actual gain
    /// </summary>
    /// <param name="g"></param>
    public void SetActualGain(float g) {
        actual_gain = g;
    }

    /// <summary>
    /// Get our gain in decibels
    /// </summary>
    /// <returns></returns>
    public float GetdB() {
        return 20f * Mathf.Log10((float) actual_gain);
    }

    /// <summary>
    /// Set the waveform to be used by our oscillator
    /// </summary>
    /// <param name="form"></param>
    public void SetWaveform(int form) {
        SetWaveform((Waveforms) form);
    }

    /// <summary>
    /// Set the waveform to be used by our oscillator
    /// </summary>
    /// <param name="form"></param>
    public void SetWaveform(Waveforms form) {
        currentWaveform = form;
    }

    /// <summary>
    /// Get the waveform type this oscillator
    /// currently uses.
    /// </summary>
    /// <returns></returns>
    public Waveforms GetWaveform() {
        return currentWaveform;
    }
    #endregion

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
    /// <param name="data">An array of float values that make up the current chunk of audio</param>
    /// <param name="channels">Mono (1) or Audio (2 Channel) </param>
    private void OnAudioFilterRead(float[] data, int channels) {

        // full volume or nothing, that's what we're doing
        if (shouldBePlaying && !isCurrentlyPlaying) {

            SetActualGain(desired_gain);
            isCurrentlyPlaying = true;

        } else if (shouldBePlaying && isCurrentlyPlaying) {

            SetActualGain(desired_gain);

        } else if (!shouldBePlaying && isCurrentlyPlaying) {

            SetActualGain(0f);
            isCurrentlyPlaying = false;

        } else if (!shouldBePlaying && !isCurrentlyPlaying) {

            SetActualGain(0f);

        }

        // how much to increment the phase,
        // Increment phase just enough to move at the rate of our sampling frequency,
        // frequency/sampling frequency * 2pi
        phase_increment = PhaseIncrement(frequency);


        for (int a = 0; a < data.Length; a += channels) {

            phase += phase_increment;

            // Apply our frequency and phase to a certain waveform equation
            data[a] = currentWaveform switch {
                Waveforms.SinWave => GetSinWaveformData(),
                Waveforms.SquareWave => GetSquareWaveformData(),
                Waveforms.TriangleWave => GetTriangleWaveformData(),
                Waveforms.SawtoothWave => GetSawWaveformData(),
                Waveforms.HarshSawtoothWave => GetHarshSawWaveformData(),
                _ => 0,
            };

            // some testing done here
            // Two waves added to each other does fun stuff
            //data[a] = AddWaves(GetSinWaveform(), GetTriangleWaveform());


            // if we have two channels...
            // copy data onto our second channel?
            if (channels == 2) {
                data[a + 1] = data[a];
            }

            // Reset phase to 0 when it reaches 2 pi
            if (phase > (Mathf.PI * 2)) {
                phase = 0.0;
            }

        }

    }

    /// <summary>
    /// Get the current increment per sample
    /// to the next phase on our sin wave
    /// </summary>
    /// <param name="freq">Double freq</param>
    /// <returns></returns>
    float PhaseIncrement(double freq) {
        return (AngularVelocity(freq) / (float) sampling_frequency);
    }

    /// <summary>
    /// Get our frequency as an angular velocity
    /// 
    /// angularVelocity = Frequency * 2pi
    /// 
    /// </summary>
    /// <param name="freq">Double frequency</param>
    /// <returns></returns>
    float AngularVelocity(double freq) {
        return (float) freq * 2.0f * Mathf.PI;
    }

    #region wavemath

    float AddWaves(float waveA, float waveB) {

        return waveA + waveB;
    }

    /// <summary>
    /// Returns one wave to the power of the other
    /// </summary>
    /// <param name="waveA"></param>
    /// <param name="waveB"></param>
    /// <returns></returns>
    float ExponentWaves(float waveA, float waveB) {


        return Mathf.Pow(waveA, waveB);
    }

    #endregion

    #region waveforms

    /// <summary>
    /// Gets a sinewave as
    /// amplitude * sin(phase)
    /// </summary>
    /// <returns>A float</returns>
    float GetSinWaveformData() {

        // phase is just set to 2 PI * frequency/sampling frequency
        // equation is sample = amplitude * sin(t * i)
        // where    amplitude   = gain
        // and      phase       = sin(t * i), where t is angular freq and i is unit of time
        // sample = gain * sin(phase)

        return actual_gain * Mathf.Sin((float) phase);
    }

    /// <summary>
    /// "Sounds like ann old nintendo"
    /// Sample = Amplitude * sng(sin(t * i)), where sgn gives us the value
    /// of the sign of the sin function (positive, negative, or zero)
    /// </summary>
    /// <returns></returns>
    float GetSquareWaveformData() {

        if (actual_gain * GetSinWaveformData() >= 0 * actual_gain) {
            return actual_gain * 0.5f;
        } else {
            return -actual_gain * 0.5f;
        }

    }

    /// <summary>
    /// "Squeaky and Quacky"
    /// Gets a triangle wave
    /// </summary>
    /// <returns></returns>
    float GetTriangleWaveformData() {

        return actual_gain * Mathf.PingPong((float) phase, 1.0f);

    }

    /// <summary>
    /// Expensive on the CPU, probably not for mobile
    /// purposes. Fix GetHarshSawWaveform() to get an
    /// optimized model
    /// </summary>
    /// <returns></returns>
    float GetSawWaveformData() {
        // y = 2A/pi * (sigma n = 1 to s (-sin((n) * f * 2 * pi * x)) / n)
        // where S is the number of sin waves
        // A is amplitude
        // f is the frequency
        // OR

        double dOutput = 0.0;

        for (double n = 1.0; n < 100.0; n++) {
            dOutput += (GetSinWaveformData() * frequency) / n;
        }

        return (float) dOutput * (2.0f / Mathf.PI);
    }

    /// <summary>
    /// FIX THIS IT DONT WORK D:
    /// </summary>
    /// <returns></returns>
    float GetHarshSawWaveformData() {
        // y= 2A/pi * (f * pi mod(1.0/f) - pi/2)

        return (2f / Mathf.PI) * ((float) frequency * (float) Mathf.PI * ((float) phase % (1f / (float) frequency) - ((float) Mathf.PI / 2f)));

    }

    #endregion

}
