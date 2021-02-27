using UnityEngine;
/// <summary>
/// A struct that includes all of our preferences in one
/// 
/// Figure out how to handle all of these properly for our MenuManager
/// </summary>
[System.Serializable]
public class PreferenceData {

    // Game Settings

    // Audio Settings
    public float MasterVolume;
    public float MusicVolume;
    public bool MuteOnLoseFocus;

    // Video Settings
    public Resolution resolution; // Make this a resolution class/struct/something idk
    public float ResolutionScale;
    public int FrameRateLimit;
    public bool VSync;
    public int VerticalFieldOfView;
    public int AntiAliasingType;
    public int AntiAliasingQuality;
    public bool AmbientOcclusion;
    public int AnisotropicFiltering;
    public int BloomQuality;

    /// <summary>
    /// A default constructor for our PreferenceData
    /// </summary>
    public PreferenceData() {

        // Get our default AudioSettings from script
        MasterVolume = AudioSettingsMenu.defaultMasterVolume;
        MusicVolume = AudioSettingsMenu.defaultMusicVolume;
        MuteOnLoseFocus = AudioSettingsMenu.defaultMuteOnLoseFocus;

        // Get our default VideoSettings from script
        resolution = Screen.resolutions[Screen.resolutions.Length];
        FrameRateLimit = resolution.refreshRate;
        VSync = VideoSettingsMenu.defaultVSync;
        VerticalFieldOfView = VideoSettingsMenu.defaultAntiAliasingQuality;
        AntiAliasingType = VideoSettingsMenu.defaultAntiAliasingType;
        AntiAliasingQuality = VideoSettingsMenu.defaultAntiAliasingQuality;
        AmbientOcclusion = VideoSettingsMenu.defaultAmbientOcclusion;
        BloomQuality = VideoSettingsMenu.defaultAnisotropicFiltering;
        BloomQuality = VideoSettingsMenu.defaultBloomQuality;

        // Get our default GameSettings from script
        // Let's include our visualizer settings here too


    }
    
}
