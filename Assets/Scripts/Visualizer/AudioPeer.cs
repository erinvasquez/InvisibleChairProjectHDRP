using UnityEngine;

public class AudioPeer : MonoBehaviour {

    AudioSource musicSource;
    AudioClip audioClip;

    public static float[] _averageSamples = new float[512];
    float[] _leftSamples = new float[512];
    float[] _rightSamples = new float[512];

    public static float[] _freqBand = new float[8];

    [Header("Status")]
    public static int BPM = 0; // Default 0 BPM, must analyze for stuff to work
    public static float trackLength;
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
        musicSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>();
        audioClip = musicSource.clip;

        // Try to set our BPM
        BPM = UniBpmAnalyzer.AnalyzeBpm(audioClip) / 2;
        trackLength = audioClip.length;

        Debug.Log("AudioPeer awake, BPM: " + BPM);
    }

    /// <summary>
    /// Called before the first frame update
    /// 
    /// 
    /// We'll change this for generic use later
    /// </summary>
    private void Start() {

        //
        // If by now we haven't successfully anyalyzed BPM, try analyzing
        // If we have analyzed BPM, stop the AudioSource from playing before
        // everyone else (Conductor) is ready to visualize
        if (BPM != 0) {
            Debug.Log("AudioPeer: BPM already analyzed before Update(), Stopping AudioSource");
            audioPeerReady = true;
            musicSource.Stop();
        } else {
            Debug.Log("AudioPeer: BPM 0, re-analyzing BPM before Update()");
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
        //  If we HAVE analyzed BPM, stop the AudioSource from playing before
        // everyone else (Conductor) is ready to visualize

        if (!audioPeerReady) {
            
            if (BPM == 0) {
                Debug.Log("AudioPeer: BPM 0, Attempting to re-analyze BPM in Update()");
                BPM = UniBpmAnalyzer.AnalyzeBpm(audioClip) / 2;
                return;
            }

            Debug.Log("AudioPeer wasn't ready, but BPM non 0, Stopping AudioSource in Update()");
            audioPeerReady = true;
            musicSource.Stop();

            return;
        }


        // Get spectrum data from each channel
        GetSpectrumAudioSource();
        //MakeFrequencyBands();
    }

    /// <summary>
    /// Pumps FFT spectrum data into our _samples array, which can be accessed publicly
    /// Currently averages both channel's data
    /// </summary>
    void GetSpectrumAudioSource() {
        musicSource.GetSpectrumData(_leftSamples, 0, FFTWindow.Blackman); // 0 is left channel
        musicSource.GetSpectrumData(_rightSamples, 1, FFTWindow.Blackman); // 1 is right channel


        // Average left and right samples into one
        for (int a = 0; a < 512; a++) {
            _averageSamples[a] = (_leftSamples[a] + _rightSamples[a]) / 2;
        }


        //_audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman); // original code, which only got from left channel
    }

    /// <summary>
    /// 
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
                average += _averageSamples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;

        }


    }

}
