using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Video settings part of settings menu
/// </summary>
public class VideoSettingsMenu : SimpleMenu<VideoSettingsMenu> {

    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown frameRateLimitDropdown;
    public Slider resolutionScaleSlider;
    public Toggle vSyncToggle;
    public Slider verticalFOVSlider;
    public TMP_Dropdown aaTypeDropdown;
    public TMP_Dropdown aaQualityDropdown;
    public TMP_Dropdown bloomQualityDropdown;

    public MenuManager menuManager;

    /// <summary>
    /// Default resolution is provided automatically as
    /// the last in our Screen.resolutions[]
    /// </summary>
    public Resolution resolution;

    public int ResolutionScale;
    public static int DefaultResolutionScale = 100;

    /// <summary>
    /// Default value automtically set in PreferenceData,
    /// it's just the max refresh rate of the monitor anyways
    /// </summary>
    public int FrameRateLimit;

    /// <summary>
    /// God awful frame rame limit cap technology for those inclined to
    /// have input lag and 60 FPS
    /// </summary>
    public bool VSync;
    public static bool defaultVSync = false;

    /// <summary>
    /// Ranges from 60 to 90+
    /// </summary>
    public int VerticalFieldOfView;
    public static int defaultVerticalFieldofView = 60;

    /// <summary>
    /// In HDRP at least, this can range from:
    /// FXAA, TAA, and SMAA
    /// </summary>
    public int AntiAliasingType;
    public static int DefaultAntiAliasingType = 0;

    /// <summary>
    /// Depends on the type of AntiAliasing,
    /// but usually a Low, Medium, High structure is used
    /// anwways
    /// </summary>
    public int AntiAliasingQuality;
    public static int DefaultAntiAliasingQuality = 0;

    /// <summary>
    /// This can be baked in if Baked Global Illumination is enabled
    /// in a scene.
    /// 
    /// Realtime Ambient Occlusion exists as a post-processing effect
    /// </summary>
    public bool AmbientOcclusion;
    public static bool DefaultAmbientOcclusion = false;

    /// <summary>
    /// Ranges from 0 to 16 apparently, powers of 2
    /// It's also a per texture thing so don't worry about it for now
    /// </summary>
    public int AnisotropicFiltering;
    public static int DefaultAnisotropicFiltering = 0;

    /// <summary>
    /// Off, or Low, Medium and High
    /// 
    /// For our visualizer, we NEED bloom
    /// Maybe turning it off can't be a feature here
    /// </summary>
    public int BloomQuality;
    public static int DefaultBloomQuality = 0;

    protected override void Awake() {

        menuManager = transform.parent.GetComponent<MenuManager>();

        resolutionDropdown = GameObject.Find("Resolution Dropdown").GetComponent<TMP_Dropdown>();
        frameRateLimitDropdown = GameObject.Find("FrameRateLimit Dropdown").GetComponent<TMP_Dropdown>();
        resolutionScaleSlider = GameObject.Find("ResolutionScale Slider").GetComponent<Slider>();
        vSyncToggle = GameObject.Find("VSync Toggle").GetComponent<Toggle>();
        verticalFOVSlider = GameObject.Find("FOV Slider").GetComponent<Slider>();
        aaTypeDropdown = GameObject.Find("AA Type Dropdown").GetComponent<TMP_Dropdown>();
        aaQualityDropdown = GameObject.Find("AA Quality Dropdown").GetComponent<TMP_Dropdown>();
        bloomQualityDropdown = GameObject.Find("Bloom Quality Dropdown").GetComponent<TMP_Dropdown>();

        resolutionDropdown.value = 0;
        resolutionScaleSlider.value = DefaultResolutionScale;
        frameRateLimitDropdown.value = 0;
        vSyncToggle.isOn = defaultVSync;
        verticalFOVSlider.value = defaultVerticalFieldofView;
        aaTypeDropdown.value = DefaultAntiAliasingType;
        aaQualityDropdown.value = DefaultAntiAliasingQuality;
        bloomQualityDropdown.value = DefaultBloomQuality;



    }


    private void Start() {

        Resolution[] resolutions = Screen.resolutions;


        for (int a = 0; a < resolutions.Length; a++) {

            Debug.Log(resolutions[a]);

        }

        

    }

    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {
        MenuManager.Instance.CloseMenu();
    }

}
