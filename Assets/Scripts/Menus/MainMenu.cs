using UnityEngine;

/// <summary>
/// Gives our MainMenu buttons their functionality
/// 
/// </summary>
public class MainMenu : SimpleMenu<MainMenu> {


    public void OnPlayPressed() {
        // GameMenu.Show()
        Debug.Log("play pressed");
    }

    public void OnSettingsPressed() {
        SettingsMenu.Show();
    }

    public override void OnBackPressed() {
        Application.Quit();
    }

}
