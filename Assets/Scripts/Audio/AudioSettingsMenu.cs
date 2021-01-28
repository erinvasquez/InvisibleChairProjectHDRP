using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Audio settings part of our settings menu
/// </summary>
public class AudioSettingsMenu : SimpleMenu<AudioSettingsMenu> {
    public static Slider masterVolumeSlider;
    public static Slider musicVolumeSlider;
    public static InputField masterVolumeInputField;
    public static InputField musicVolumeInputField;

    public static float defaultMasterVolume = 0.65f;
    public static float defaultMusicVolume = 0.65f;

    protected override void Awake() {
        base.Awake();

        masterVolumeSlider = GameObject.Find("MasterVolume Slider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.Find("MusicVolume Slider").GetComponent<Slider>();

        masterVolumeInputField = GameObject.Find("MasterVolume InputField").GetComponent<InputField>();
        musicVolumeInputField = GameObject.Find("MusicVolume InputField").GetComponent<InputField>();

    }

    private void Start() {
        //masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume); // Default value is set here, how else can we?
        //musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", defaultMusicVolume); // This won't work in awake apparently
    }

    public void SetMasterSliderValue(string value) {
        float x = float.Parse(value);

        Mathf.Clamp(x, 0.0001f, 1f);

        masterVolumeSlider.value = x;

    }

    /// <summary>
    /// Set value of our Input Field
    /// </summary>
    public void SetMasterInputFieldValue(float sliderValue) {

        //float newMasterVolume = Mathf.Log10(sliderValue) * 20;
        //masterVolumeInputField.text = "" + (newMasterVolume * 100);

    }

    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {

        // Currently, this only closes the top menu
        // I want to be able to close the entire settings menu, not just the submenu
        // Either close two menus or find another workaround

        MenuManager.Instance.CloseMenu();
    }

    

}
