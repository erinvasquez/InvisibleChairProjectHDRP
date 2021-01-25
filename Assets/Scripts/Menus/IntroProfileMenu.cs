using UnityEngine;
using UnityEngine.UI;

public class IntroProfileMenu : Menu<IntroProfileMenu> {

    private InputField profileNameField;

    private void Start() {
        profileNameField = GetComponent<InputField>();
    }

    private void Update() {

        // If instead of hitting the submit button, we want to press ENTER/RETURN instead
        if (profileNameField.isFocused && profileNameField.text != "" && Input.GetKey(KeyCode.Return)) {
            SubmitProfileName();
        }

    }

    /// <summary>
    /// The same process as in Update() to submit, but with a button in Unity UI
    /// </summary>
    public void SubmitProfileName() {

        if (profileNameField.text != "") {
            // Set up our user's profile from here with PlayerState, PlayerInfo, and PlayerPrefs
            // and go to the main menu

            Debug.Log("Sucess, the name field isn't empty and you pressed the button");
        }

    }

    public static void Hide() {
        Close();
    }

}
