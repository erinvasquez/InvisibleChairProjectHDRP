using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DeveloperConsole : MonoBehaviour {

    UnityEngine.GameObject DevCanvas;
    UnityEngine.GameObject DevPanel;

    [SerializeField]
    TextMeshProUGUI display = default;

    [SerializeField]
    KeyCode consoleKey = KeyCode.BackQuote;

    string newText;

    bool toggleDesired;
    bool isShowing;

    /// <summary>
    /// Cache our variables
    /// </summary>
    void Awake() {
        DevCanvas = UnityEngine.GameObject.Find("ConsoleCanvas");
        DevPanel = UnityEngine.GameObject.Find("ConsolePanel");
        CloseConsole();

    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update() {
        newText = "";

        toggleDesired |= Input.GetKeyDown(consoleKey);

        if (toggleDesired) {

            Debug.Log("Console Key pressed");

            toggleDesired = false;
            ToggleConsole();
        }


        // Add any new text to the console
        display.SetText(display.text + newText);
    }

    /// <summary>
    /// Open our console.
    /// </summary>
    void OpenConsole() {
        isShowing = true;
        DevCanvas.SetActive(isShowing);
    }

    /// <summary>
    /// 
    /// </summary>
    void CloseConsole() {
        isShowing = false;
        DevCanvas.SetActive(isShowing);
    }

    /// <summary>
    /// 
    /// </summary>
    void ToggleConsole() {
        // toggle our showing state
        isShowing = !isShowing;

        DevCanvas.SetActive(isShowing);

    }


}
