using UnityEngine;

public class VisualizerSpawner : MonoBehaviour {

    public GameObject _sampleCubePrefab;

    GameObject[] _sampleCube = new GameObject[512];
    AudioPeer audioPeer;



    public float _maxScale = 10000f;
    public float _startingScale = .5f;
    public float _scaleMultiplier = 10000f;

    /// <summary>
    /// Instantiate the cubes and put them in position
    /// </summary>
    private void Start() {

        //audioPeer = GameObject.Find("AudioPeer").GetComponent<AudioPeer>();

        // Instantiate 512 cubes (since we have 512 samples)
        // position them and give them a parent and name,
        // add them to our array for future reference
        for (int i = 0; i < 512; i++) {
            GameObject _instanceSampleCube = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCube.transform.position = this.transform.position;
            _instanceSampleCube.transform.parent = this.transform;
            _instanceSampleCube.name = "SampleCube " + i;
            this.transform.eulerAngles = new Vector3(0, -0.703125f * i, 0);
            _instanceSampleCube.transform.position = Vector3.forward * 50;
            _sampleCube[i] = _instanceSampleCube;
        }

    }

    private void Update() {

        for (int i = 0; i < 512; i++) {

            if (_sampleCube != null) {

                float currentSample = AudioPeer._averageSamples[i];


                float output = Mathf.Clamp(currentSample * _scaleMultiplier, _startingScale, _maxScale);

                _sampleCube[i].transform.localScale = new Vector3(.5f, output, .5f);
            }

        }

    }

}
