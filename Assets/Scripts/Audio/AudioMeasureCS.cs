using UnityEngine;


/// <summary>
/// https://answers.unity.com/questions/157940/getoutputdata-and-getspectrumdata-they-represent-t.html
/// </summary>
public class AudioMeasureCS : MonoBehaviour {
    public float RmsValue;
    public float DbValue;
    public float PitchValue;

    private const int QSamples = 1024;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;

    public AudioSource audioSource;

    /// <summary>
    /// http://peabody.sapp.org/class/st2/lab/notehz/
    /// Pitch to frequency mappings in equal temperament,
    /// based on A4 - 440 Hz to the nearest Hz
    /// </summary>
    public enum Pitches {
        C0 = 16, C0Sharp = 17,
        D0 = 18, D0Sharp = 20,
        E0 = 21,
        F0 = 22, F0Sharp = 23,
        G0 = 25, G0Sharp = 26,
        A0 = 28, A0Sharp = 29,
        B0 = 31,
        C1 = 33, C1Sharp = 35,
        D1 = 37, D1Sharp = 39,
        E1 = 41,
        F1 = 44, F1Sharp = 46,
        G1 = 49, G1Sharp = 52,
        A1 = 55, A1Sharp = 58,
        B1 = 62,
        C2 = 65, C2Sharp = 69,
        D2 = 73, D2Sharp = 78,
        E2 = 82,
        F2 = 87, F2Sharp = 93,
        G2 = 98, G2Sharp = 104,
        A2 = 110, A2Sharp = 117,
        B2 = 124,
        C3 = 131, C3Sharp = 139,
        D3 = 147, D3Sharp = 156,
        E3 = 165,
        F3 = 175, F3Sharp = 185,
        G3 = 196, G3Sharp = 208,
        A3 = 220, A3Sharp = 233,
        B3 = 247,
        C4 = 262, C4Sharp = 278,
        D4 = 294, D4Sharp = 311,
        E4 = 330,
        F4 = 349, F4Sharp = 370,
        G4 = 392, G4Sharp = 415,
        A4 = 440, A4Sharp = 466,
        B4 = 494,
        C5 = 523, C5Sharp = 554,
        D5 = 587, D5Sharp = 622,
        E5 = 659,
        F5 = 699, F5Sharp = 740,
        G5 = 784, G5Sharp = 831,
        A5 = 880, A5Sharp = 932,
        B5 = 988,
        C6 = 1047, C6Sharp = 1109,
        D6 = 1175, D6Sharp = 1245,
        E6 = 1319,
        F6 = 1397, F6Sharp = 1475,
        G6 = 1568, G6Sharp = 1661,
        A6 = 1760, A6Sharp = 1865,
        B6 = 1976,
        C7 = 2093, C7Sharp = 2218,
        D7 = 2349, D7Sharp = 2489,
        E7 = 2637,
        F7 = 2794, F7Sharp = 2960,
        G7 = 3136, G7Sharp = 3322,
        A7 = 3520, A7Sharp = 3729,
        B7 = 3951,
        C8 = 4186, C8Sharp = 4435,
        D8 = 4699, D8Sharp = 4978,
        E8 = 5274,
        F8 = 5588, F8Sharp = 5920,
        G8 = 6272, G8Sharp = 6645,
        A8 = 7040, A8Sharp = 7459,
        B8 = 7902
    };


    void Start() {
        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
    }

    void Update() {
        AnalyzeSound();

    }

    void AnalyzeSound() {

        audioSource.GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples

        int i;
        float sum = 0;

        for (i = 0; i < QSamples; i++) {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }

        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average

        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB

        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min


        // get sound spectrum
        audioSource.GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;

        for (i = 0; i < QSamples; i++) { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }

        float freqN = maxN; // pass the index to a float variable

        if (maxN > 0 && maxN < QSamples - 1) { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];

            freqN += 0.5f * (dR * dR - dL * dL);

        }

        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency

    }

    public float GetRms() {
        return RmsValue;
    }

    public float GetDb() {
        return DbValue;
    }

    public float GetPitch() {
        return PitchValue;
    }

}