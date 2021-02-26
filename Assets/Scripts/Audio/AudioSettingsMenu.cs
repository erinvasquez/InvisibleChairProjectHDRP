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

    public MenuManager menuManager;

    public static float defaultMasterVolume = 0.65f;
    public static float defaultMusicVolume = 0.65f;
    public static bool defaultMuteOnLoseFocus = true;
    
    // Fix the handling of these values
    private float masterVolume;
    private float musicVolume;
    private bool muteOnLoseFocus;

    protected override void Awake() {
        // SUPER IMPORTANT line
        base.Awake();

        menuManager = transform.parent.GetComponent<MenuManager>();

        masterVolumeSlider = GameObject.Find("MasterVolume Slider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.Find("MusicVolume Slider").GetComponent<Slider>();

        masterVolumeInputField = GameObject.Find("MasterVolume InputField").GetComponent<InputField>();
        musicVolumeInputField = GameObject.Find("MusicVolume InputField").GetComponent<InputField>();

        masterVolumeSlider.value = defaultMasterVolume;
        musicVolumeSlider.value = defaultMusicVolume;
        muteOnLoseFocus = defaultMuteOnLoseFocus;

    }


    /// <summary>
    /// Load our Audio PreferenceData before doing anything
    /// 
    /// </summary>
    private void Start() {

        // Handle making a default preferences file so we don't get a "null" error

        menuManager.LoadPreferences();

    }

    public float GetMasterVolume() {
        return masterVolumeSlider.value;
    }

    public float GetMusicVolume() {
        return musicVolumeSlider.value;
    }

    public bool GetMuteOnLoseFocus() {
        return muteOnLoseFocus;
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
    /// 
    /// TODO
    /// Handle whether or not we save the changes made in our audio settings here
    /// </summary>
    public override void OnBackPressed() {

        // Currently, this only closes the top menu
        // I want to be able to close the entire settings menu, not just the submenu
        // Either close two menus or find another workaround

        MenuManager.Instance.CloseMenu();
    }



}
