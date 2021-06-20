using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Gives our MainMenu buttons their functionality
/// 
/// </summary>
public class MainMenu : SimpleMenu<MainMenu> {
    //public TMP_InputField usernameField;
    public GameObject fractal1;
    public GameObject fractal2;
    public Camera camera1;

    public void Start() {

        //usernameField = GameObject.Find("UsernameField").GetComponent<TMP_InputField>();
        fractal1 = GameObject.Find("Fractal 1");
        fractal2 = GameObject.Find("Fractal 2");
        camera1 = Camera.main;

    }

    public void OnPlayPressed() {
        // GameMenu.Show()
        Debug.Log("play pressed, attempting to load new scene");

        // Commented out for the music video
        GameSelectionMenu.Show();

        /*
        // Start the camera moving, start the fractals appearing
        Conductor.musicSource.Play();
        fractal1.GetComponent<Fractal>().moving = true;
        fractal2.GetComponent<Fractal>().moving = true;
        camera1.gameObject.GetComponent<RotateCameraAround>().moving = true;
        VisualizerSpawner.instance.moving = true;
        */


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
