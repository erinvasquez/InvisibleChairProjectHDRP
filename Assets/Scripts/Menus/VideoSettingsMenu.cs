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
    //public Resolution resolution;
    public int resolution;

    public int resolutionScale;
    public static int defaultResolutionScale = 100;

    /// <summary>
    /// Default value automtically set in PreferenceData,
    /// it's just the max refresh rate of the monitor anyways
    /// </summary>
    public int frameRateLimit;

    /// <summary>
    /// God awful frame rame limit cap technology for those inclined to
    /// have input lag and 60 FPS
    /// </summary>
    public bool vSync;
    public static bool defaultVSync = false;

    /// <summary>
    /// Ranges from 60 to 90+
    /// </summary>
    public int verticalFieldOfView;
    public static int defaultVerticalFieldofView = 60;

    /// <summary>
    /// In HDRP at least, this can range from:
    /// FXAA, TAA, and SMAA
    /// </summary>
    public int antiAliasingType;
    public static int defaultAntiAliasingType = 0;

    /// <summary>
    /// Depends on the type of AntiAliasing,
    /// but usually a Low, Medium, High structure is used
    /// anwways
    /// </summary>
    public int antiAliasingQuality;
    public static int defaultAntiAliasingQuality = 0;

    /// <summary>
    /// This can be baked in if Baked Global Illumination is enabled
    /// in a scene.
    /// 
    /// Realtime Ambient Occlusion exists as a post-processing effect
    /// </summary>
    public bool ambientOcclusion;
    public static bool defaultAmbientOcclusion = false;

    /// <summary>
    /// Ranges from 0 to 16 apparently, powers of 2
    /// It's also a per texture thing so don't worry about it for now
    /// </summary>
    public int anisotropicFiltering;
    public static int defaultAnisotropicFiltering = 0;

    /// <summary>
    /// Off, or Low, Medium and High
    /// 
    /// For our visualizer, we NEED bloom
    /// Maybe turning it off can't be a feature here
    /// </summary>
    public int bloomQuality;
    public static int defaultBloomQuality = 0;

    protected override void Awake() {
        // You MUST have this otherwise problems will appear
        base.Awake();

        // is this even used
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
        resolutionScaleSlider.value = defaultResolutionScale;
        frameRateLimitDropdown.value = 0;
        vSyncToggle.isOn = defaultVSync;
        verticalFOVSlider.value = defaultVerticalFieldofView;
        aaTypeDropdown.value = defaultAntiAliasingType;
        aaQualityDropdown.value = defaultAntiAliasingQuality;
        bloomQualityDropdown.value = defaultBloomQuality;



    }


    private void Start() {


    }

    public int GetResolution => resolutionDropdown.value;

    public void SetResolution(int res) {
        resolution = res;
        //Screen.SetResolution(res.width, res.height, (FullScreenMode.FullScreenWindow)); // We need a fullscreen, windowed, etc handling here
    }

    public float GetResolutionScale => resolutionScaleSlider.value;

    public void SetResolutionScale(int scale) {
        resolutionScale = scale;
        //resolutionScaleSlider.value = scale;

    }

    public int GetFrameRateLimit => frameRateLimitDropdown.value;

    public void SetFrameRameLimit(int limit) {
        frameRateLimit = limit;
        //frameRateLimitDropdown.value = limit;
    }

    public bool GetVSyncToggle => vSyncToggle.isOn;

    public void SetVSyncToggle(bool val) {
        vSync = val;
        //vSyncToggle.isOn = val;
    }

    public int GetVerticalFOV => (int) verticalFOVSlider.value;

    public void SetVerticalFov(int fov) {
        verticalFieldOfView = fov;
        //verticalFOVSlider.value = fov;
    }

    public int GetAAType => aaTypeDropdown.value;

    public void SetAAType(int type) {
        antiAliasingType = type;
        //aaTypeDropdown.value = type;
    }

    public int GetAAQuality => aaQualityDropdown.value;

    public void SetAAQuality(int quality) {
        antiAliasingQuality = quality;
        //aaQualityDropdown.value = quality;
    }

    public int GetBloomQuality => bloomQualityDropdown.value;

    public void SetBloomQuality(int quality) {
        bloomQuality = quality;
        //bloomQualityDropdown.value = quality;
    }

    /// <summary>
    /// I'm assuming we'll call this if the user pressed ESC/BACK while on this menu in particular?
    /// </summary>
    public override void OnBackPressed() {
        //menuManager.CloseMenu();
        MenuManager.Instance.CloseMenu();
    }

}
