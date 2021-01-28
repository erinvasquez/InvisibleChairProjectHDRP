using UnityEngine;
using TMPro;

/// <summary>
/// Thanks to a tutorial on catlikecoding.com/unity/tutorials/basics
/// </summary>
public class MusicDisplay : MonoBehaviour {

    [SerializeField]
    TextMeshProUGUI display = default;

    public static float[] averageSamples = AudioPeer._averageSamples;

    float intensity, minIntensity = float.MaxValue, maxIntensity = 0f;
    

    // Update is called once per frame
    void Update() {

        // Get the current average samples from AudioPeer
        averageSamples = AudioPeer._averageSamples;

        intensity = getIntensity();

        if (intensity < minIntensity) {
            minIntensity = intensity;
        }

        if (intensity > maxIntensity) {
            maxIntensity = intensity;
        }


        display.SetText("Intensity\n{0}\n{1}\n{2}", intensity * 10000f, maxIntensity * 10000f, minIntensity * 10000f);

    }

    private float getIntensity() {
        // Average all of our current samples
        // and output it as our music's "intensity"

        float average = 0f;

        for (int a = 0; a < averageSamples.Length; a++) {
            average += averageSamples[a];
        }

        average /= averageSamples.Length; 



        return average;
    }
}
