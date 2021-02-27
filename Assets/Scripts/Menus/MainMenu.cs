using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gives our MainMenu buttons their functionality
/// 
/// </summary>
public class MainMenu : SimpleMenu<MainMenu> {


    public void OnPlayPressed() {
        // GameMenu.Show()
        Debug.Log("play pressed, attempting to load new scene");

        SceneManager.LoadScene("GAME_01");

    }

    public void OnSettingsPressed() {
        SettingsMenu.Show();
    }

    public override void OnBackPressed() {
        Application.Quit();
    }

}
