using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

/// <summary>
/// Takes care of our Main and Music Volumes
/// </summary>
public class AudioMaster : MonoBehaviour {

    public AudioMixer mixer;

    public static AudioMaster Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        // Nothing yet
    }


    /// <summary>
    /// Sets our Master volume from the audio settings panel
    /// </summary>
    /// <param name="sliderValue">Our master volume, from 0.0001 to 1</param>
    public void SetMasterVolume(float sliderValue) {

        // Convert to logarithmic curve, otherwise our volume control won't work properly
        float newMasterVolume = Mathf.Log10(sliderValue) * 20;

        mixer.SetFloat("MasterVolume", newMasterVolume);
        PlayerPrefs.SetFloat("MasterVolume", newMasterVolume); // save it for our settings to

        AudioSettingsMenu.Instance.setMasterInputFieldValue(sliderValue);

    }

    /// <summary>
    /// Sets our music volume from the audio settings panel
    /// </summary>
    /// <param name="sliderValue">Our music volume, from 0.0001 to 1</param>
    public void SetMusicVolume(float sliderValue) {

        float newMusicVolume = Mathf.Log10(sliderValue) * 20;

        mixer.SetFloat("MasterMusic", newMusicVolume);
        PlayerPrefs.SetFloat("MasterMusic", newMusicVolume);

    }

}
