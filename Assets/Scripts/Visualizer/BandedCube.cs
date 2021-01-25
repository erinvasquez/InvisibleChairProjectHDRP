using UnityEngine;

public class BandedCube : MonoBehaviour {
    AudioSource audioSource;

    public int _band;
    public float _startScale, _scaleMultiplier;
    public int currentBeats = 0;

    Renderer cubeRenderer;
    Color[] colors = new Color[4];
    int count;


    private void Start() {
        _startScale = 2;
        _scaleMultiplier = 25f;

        audioSource = GameObject.Find("Main Menu Music").GetComponent<AudioSource>();
        cubeRenderer = gameObject.GetComponent<Renderer>();

        count = 0;
        colors[0] = Color.red;
        colors[1] = Color.white;
        colors[2] = Color.yellow;
        colors[3] = Color.blue;
    }

    private void Update() {
        transform.localScale = new Vector3(transform.localScale.x,
            (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale,
            transform.localScale.z);

        // Change color every 4 measures/16 beats
        // X Beats/Minute * (1 minute / 60 seconds) * current seconds = beats
        if (currentBeats != 0 && currentBeats != AudioPeer.BPM / 60 * (int)audioSource.time) {


            if (currentBeats % 4 == 0) {
                ColorChange();
            }

        }

        currentBeats = AudioPeer.BPM / 60 * (int)audioSource.time;

    }

    void ColorChange() {

        if (count > 3) {
            count = 0;
        }

        cubeRenderer.material.SetColor("_EmissionColor", colors[count]);
        count++;
    }




}
