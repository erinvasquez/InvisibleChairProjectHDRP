using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Gives our MainMenu buttons their functionality
/// 
/// </summary>
public class MainMenu : SimpleMenu<MainMenu> {
    public TMP_InputField usernameField;

    public void Start() {

        usernameField = GameObject.Find("UsernameField").GetComponent<TMP_InputField>();

    }

    public void OnPlayPressed() {
        // GameMenu.Show()
        Debug.Log("play pressed, attempting to load new scene");

        GameSelectionMenu.Show();

    }

    public void OnSettingsPressed() {
        SettingsMenu.Show();
    }

    public void OnConnectPressed() {

        // "startmenu.SetActive(false);
        //usernameField.interactable = false;
        //Client;
        Debug.Log("Connect Pressed, try connecting then");

    }

    public override void OnBackPressed() {
        Application.Quit();
    }



}
