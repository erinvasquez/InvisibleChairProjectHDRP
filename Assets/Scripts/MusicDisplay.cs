using UnityEngine;
using TMPro;

/// <summary>
/// Thanks to a tutorial on catlikecoding.com/unity/tutorials/basics
/// </summary>
public class MusicDisplay : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI display = default;

    public static float[] averageSamples = AudioPeer._AverageMonoSamples;

    float intensity, minIntensity = float.MaxValue, maxIntensity = 0f;
    

    // Update is called once per frame
    void Update() {

        // Get the current intensity from AudioPeer
        intensity = AudioPeer.intensity;

        if (intensity < minIntensity) {
            minIntensity = intensity;
        }

        if (intensity > maxIntensity) {
            maxIntensity = intensity;
        }


        display.SetText("Intensity\n{0}\n{1}\n{2}", intensity, maxIntensity, minIntensity);

    }
}
