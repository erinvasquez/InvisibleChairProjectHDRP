using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Placed on our whatever object our MenuManager is on
/// 
/// Sa
/// </summary>
public static class PreferenceManager {
    static AudioSettingsMenu audioMenu;
    static Menu<GameSettingsMenu> gameMenu;
    static Menu<VideoSettingsMenu> videoMenu;

    static BinaryFormatter formatter;
    static FileStream stream;
    static PreferenceData data;
    static string path;

    /// <summary>
    /// Save our Preferences from our Settings menus
    /// </summary>
    /// <param name="menuManager"></param>
    public static void SavePreferences(MenuManager menuManager) {
        formatter = new BinaryFormatter();
        path = Application.persistentDataPath + "/preferences.file";
        stream = new FileStream(path, FileMode.Create);

        gameMenu = menuManager.GetGameSettingsMenu();
        audioMenu = menuManager.GetAudioSettingsMenu().GetComponent<AudioSettingsMenu>();
        videoMenu = menuManager.GetVideoSettingsMenu();



        // Create data for our prefences
        data = new PreferenceData();

        // Get it from our [Menu that is open?]
        data.MasterVolume = audioMenu.GetMasterVolume();
        data.MusicVolume = audioMenu.GetMusicVolume();
        data.MuteOnLoseFocus = audioMenu.GetMuteOnLoseFocus();




        // Write it to file
        formatter.Serialize(stream, data);
        stream.Close();

    }

    /// <summary>
    /// Overwrites [all settings] to default. (Separate between Game, Audio, and Video later?)
    /// I hate when shit crashes while changing video settings, only to lose all the bindings in our controls
    /// 
    /// </summary>
    public static void OverwriteToDefaultPreferences() {
        formatter = new BinaryFormatter();
        path = Application.persistentDataPath + "/preferences.file";
        stream = new FileStream(path, FileMode.Create);


        // The default data should suffice
        data = new PreferenceData();


        // Write to file
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Called by MenuManager when our game starts, and when Game, Audio, and Video Settings menus are opened
    /// Load our Preferences for our Settings Menus
    /// </summary>
    /// <returns></returns>
    public static PreferenceData LoadPreferences() {

        string path = Application.persistentDataPath + "/preferences.file";

        if (File.Exists(path)) {
            formatter = new BinaryFormatter();
            stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as PreferenceData;
            stream.Close();

            return data;

        } else {
            OverwriteToDefaultPreferences();
            Debug.LogError("Preferences file not found at " + path + ". Writing Default preferences file");
            return null;
        }


    }


}
