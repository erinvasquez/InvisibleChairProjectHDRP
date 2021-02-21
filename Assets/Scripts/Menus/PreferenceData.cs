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
    public int Resolution; // Make this a resolution class/struct/something idk
    public float ResolutionScale;
    public int FrameRateLimit;
    public bool Vsync;
    public int VerticalFieldOfView;
    public int AntiAliasing;
    public int AmbientOcclusion;
    public int Bloom;

    /// <summary>
    /// A default constructor for our PreferenceData
    /// </summary>
    public PreferenceData() {

        // Get our default AudioSettings from script
        MasterVolume = AudioSettingsMenu.defaultMasterVolume;
        MusicVolume = AudioSettingsMenu.defaultMusicVolume;
        MuteOnLoseFocus = AudioSettingsMenu.defaultMuteOnLoseFocus;

        // Get our default VideoSettings from script

        // Get our default GameSettings from script


    }
    
}
