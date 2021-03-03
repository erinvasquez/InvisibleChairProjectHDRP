using UnityEngine;

/// <summary>
/// From some YouTube video? find it and credit them
/// </summary>
public class AudioPeer : MonoBehaviour {

    /// <summary>
    /// 
    /// </summary>
    AudioSource musicSource;

    /// <summary>
    /// 
    /// </summary>
    AudioClip audioClip;

    /// <summary>
    /// 512 sample array of averaged left and right stereo values
    /// into one mono audio array
    /// </summary>
    public static float[] monoSamples = new float[512];

    /// <summary>
    /// 512 sample array of channel 1, left side audio
    /// </summary>
    float[] leftSamples = new float[512];

    /// <summary>
    /// 512 sample array of averaged left and right stereo values
    /// </summary>
    float[] rightSamples = new float[512];

    /// <summary>
    /// Samples 0 to 169
    /// </summary>
    public float[] lowSamples = new float[170]; // turns out to be 170.6, so it'll give us 170 samples
    /// <summary>
    /// Samples 170 to 340
    /// </summary>
    public float[] midSamples = new float[170];
    /// <summary>
    /// Samples 341 to 511
    /// </summary>
    public float[] highSamples = new float[170];



    public static float[] _freqBand = new float[8];

    [Header("Status")]
    public static int BPM = 0; // Default 0 BPM, must analyze for stuff to work
    public static float trackLength;
    public static float intensity; // average value of all samples

    public static float lowsIntensity;
    public static float midsIntensity;
    public static float highsIntensity;

    public static bool audioPeerReady = false; // True when BPM is analyzed


    /// <summary>
    /// Keep AudioPeer as a Singleton
    /// </summary>
    public static AudioPeer Instance { get; private set; }


    /// <summary>
    /// Find our menu music, analyze our AudioSource's BPM by letting the AudioSource
    /// play for the split second before the first frame
    /// 
    /// </summary>
    private void Awake() {
        Instance = this;

        // Get our [Main Menu] Music
        musicSource = GameObject.Find("Music").GetComponent<AudioSource>();
        audioClip = musicSource.clip;

        // Try to set our BPM
        BPM = UniBpmAnalyzer.AnalyzeBpm(audioClip) / 2;
        trackLength = audioClip.length;

        //Debug.Log("AudioPeer awake, BPM: " + BPM);
    }

    /// <summary>
    /// Called before the first frame update
    /// 
    /// 
    /// We'll change this for generic use later
    /// </summary>
    private void Start() {

        // If by now we haven't successfully anyalyzed BPM, try analyzing
        // If we have analyzed BPM, stop the AudioSource from playing before
        // everyone else (Conductor) is ready to visualize
        if (BPM != 0) {
            //Debug.Log("AudioPeer: BPM already analyzed before Update(), Stopping AudioSource");
            audioPeerReady = true;
            musicSource.Stop();
        } else {
            //Debug.Log("AudioPeer: BPM 0, re-analyzing BPM before Update()");
            BPM = UniBpmAnalyzer.AnalyzeBpm(audioClip) / 2;
        }


    }


    /// <summary>
    /// After waiting for a VisualizerReady status...
    /// 
    /// Update our spectrum data onto our samples
    /// </summary>
    private void Update() {

        // If by now we haven't successfully analyzed BPM, try analyzing
        //    If we HAVE analyzed BPM (in this case that means it's 0),
        //    stop the AudioSource from playing before
        //    the Conductor is ready
        if (!audioPeerReady) {

            if (BPM == 0) {
                // Debug.Log("AudioPeer: BPM 0, Attempting to re-analyze BPM in Update()");
                BPM = UniBpmAnalyzer.AnalyzeBpm(audioClip) / 2;
                return;
            }

            // Debug.Log("AudioPeer wasn't ready, but BPM non 0, Stopping AudioSource in Update()");
            audioPeerReady = true;
            musicSource.Stop();

            //return;
        }


        // Get spectrum data from each channel
        GetSpectrumDataFromStereo(leftSamples, rightSamples, monoSamples);

        // Set our intensity
        SetIntensity(monoSamples);

    }

    /// <summary>
    /// Get our music's spectrum data and pump into left and right
    /// channel sampes, as well as averaging them into _averageSamples[]
    /// </summary>
    /// <param name="leftSamples">Channel 0, or the left side of our audio</param>
    /// <param name="rightSamples">Channel 1, or the right side of our audio</param>
    void GetSpectrumDataFromStereo(float[] leftSamples, float[] rightSamples, float[] averageSamples) {
        musicSource.GetSpectrumData(leftSamples, 0, FFTWindow.Blackman); // 0 is left channel
        musicSource.GetSpectrumData(rightSamples, 1, FFTWindow.Blackman); // 1 is right channel


        // Average left and right samples into one average mono-sample
        for (int a = 0; a < 512; a++) {
            averageSamples[a] = (leftSamples[a] + rightSamples[a]) / 2;

            // Put our low mid and high samples in respective
            if (a < lowSamples.Length) {
                lowSamples[a] = averageSamples[a];
            } else if (a < (lowSamples.Length + midSamples.Length)) {
                midSamples[a - lowSamples.Length] = averageSamples[a];
            } else if (a < (lowSamples.Length + midSamples.Length + highSamples.Length)) {
                highSamples[a - (lowSamples.Length + midSamples.Length)] = averageSamples[a];
            }


        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="samples"></param>
    void GetSpectrumDataFromMono(float[] samples) {

        musicSource.GetSpectrumData(samples, 0, FFTWindow.Blackman); // 0 is left or mono channel

    }

    /// <summary>
    /// Old method used to make frequency bands...?
    /// </summary>
    void MakeFrequencyBands() {
        /* 48000 hz frequency for 1/50 AM
         * 48000/512 = 93.75 Hz per sample
         * 
         * 0 - 2    = 187.5 Hz  =
         * 1 - 4    = 375 Hz    = 188-563
         * 2 - 8    = 750 Hz    = 564-1314
         * 3 - 16   = 1500      = 1315-2815
         * 4 - 32   = 3000      = 2816-5816
         * 5 - 64   = 6000      = 5817-11817
         * 6 - 128  = 12000     = 11818-23818
         * 7 - 256  = 24000     = 23919-47819
         * 510
         * 
         */

        int count = 0;

        for (int i = 0; i < 8; i++) {

            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7) {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++) {
                average += monoSamples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;

        }


    }

    /// <summary>
    /// Get the volume intensity of the current sample set
    /// </summary>
    /// <returns></returns>
    void SetIntensity(float[] samples) {
        // Average all of our current samples
        // output it as our music's "intensity"
        float average = 0f;
        float lowAverage = 0f;
        float midAverage = 0f;
        float highAverage = 0f;

        for (int a = 0; a < samples.Length; a++) {
            average += samples[a];

            if (a <= 160) {
                lowAverage += samples[a];
            } else if (a <= 340) {
                midAverage += samples[a];
            } else if (a <= 511) {
                highAverage += samples[a];
            }

        }

        average /= samples.Length;
        lowAverage /= lowSamples.Length;
        midAverage /= midSamples.Length;
        highAverage /= highSamples.Length;


        intensity = average;
        lowsIntensity = lowAverage;
        midsIntensity = midAverage;
        highsIntensity = highAverage;


    }

    public float GetLowsIntensity() {
        return lowsIntensity;
    }

    public float GetMidsIntensity() {
        return midsIntensity;
    }

    public float GetHighsIntensity() {
        return highsIntensity;
    }

}
