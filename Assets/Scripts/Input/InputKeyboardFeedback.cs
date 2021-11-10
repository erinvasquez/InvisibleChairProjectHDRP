using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

#region Extension class
///<summary>
/// https://forum.unity.com/threads/new-input-system-check-if-a-key-was-pressed.952571/
/// Useful extension class
/// needs press and release interaction in the action settings
/// This might already be implemented, idk, im just copying this for later
///</summary>
public static class InputActionExtensions {

    public static bool IsPressed(this InputAction inputAction) {
        return inputAction.ReadValue<float>() > 0f;
    }

    public static bool WasPressedThisFrame(this InputAction inputAction) {
        return inputAction.triggered && inputAction.ReadValue<float>() > 0f;
    }

    public static bool WasReleasedThisFrame(this InputAction inputAction) {
        return inputAction.triggered && inputAction.ReadValue<float>() == 0f;
    }


}

#endregion

/// <summary>
/// Works with Unity input system to produce typing feedback
/// and provide a structure for further input processing for
/// things like keybinds
/// </summary>
public class InputKeyboardFeedback : MonoBehaviour {

    // our text objects
    TextMeshProUGUI feedbackText;
    TextMeshProUGUI currentInputText;
    TextMeshProUGUI mousePosText;

    /// <summary>
    /// Our scroll input
    /// </summary>
    private float scroll = 0f;

    // our currently held down keys (max 10 considering 10 fingers, although I know you could press more than 10)
    /// <summary>
    /// An enum list representing the keys the user is currently holding down,
    /// with -1b representing an unpressed key at the end of the array
    /// </summary>
    public List<KeyboardKeys> myKeys = new List<KeyboardKeys>();

    /// <summary>
    /// Our Synthesizer component on a Synthesizer gameobject
    /// </summary>
    public Synthesizer synth;

    private void Start() {

        // Get our text game objects
        feedbackText = GameObject.Find("FeedbackText").GetComponent<TextMeshProUGUI>();
        currentInputText = GameObject.Find("CurrentInputText").GetComponent<TextMeshProUGUI>();
        mousePosText = GameObject.Find("MousePositionText").GetComponent<TextMeshProUGUI>();
        synth = GameObject.Find("Synthesizer").GetComponent<Synthesizer>();

        // Set some default text
        feedbackText.text = ">: ";
        currentInputText.text = "LAST INPUT: ";
        mousePosText.text = "MOUSE POS: ";

    }

    /// <summary>
    /// Keep track of our scroll wheel input here, since it's not handled by Unity's new Input System
    /// </summary>
    private void Update() {

        // Handle our scroll input here, since Unity's Input Manager can't handle the scroll wheel
        scroll = Mouse.current.scroll.ReadValue().y;

        // In the future, replace "ProcessKey(...)" with whatever function handles input later on
        if (scroll < 0f) {
            //KeyPerformed("ScrollDown");
        } else if (scroll > 0f) {
            //KeyPerformed("ScrollUp");
        }

        mousePosText.text = "MOUSE POS: " + Mouse.current.position.ReadValue();

    }

    #region Test Script
    /// <summary>
    /// From https://forum.unity.com/threads/player-input-component-triggering-events-multiple-times.851959/
    /// An example of how "each action goes through a started, performed, canceled cycle with callbacks
    /// triggered for each one, supplied by info in the context object"
    /// </summary>
    /// <param name="context"></param>
    public void OnMyAction(InputAction.CallbackContext context) {
        if (context.started)
            Debug.Log("Action was started");
        else if (context.performed)
            Debug.Log("Action was performed");
        else if (context.canceled)
            Debug.Log("Action was cancelled");
    }

    #endregion


    /// <summary>
    /// Adds text input from keyboard to a text object in our UI
    /// </summary>
    /// <param name="text"></param>
    void AddFeedbackText(string text) {
        feedbackText.text += text;
    }

    /// <summary>
    /// Sets the text variable for a UI object to show our last
    /// keytboard input
    /// </summary>
    /// <param name="text"></param>
    void SetLastInputText(string text) {
        currentInputText.text = "LAST INPUT: " + text;
    }

    #region Key Handling functions

    /// <summary>
    /// Takes a string from our keyboard input and adds it to our input text log when first
    /// pressed, and displays the last known key pressed
    /// </summary>
    /// <para name="context"></param>
    public void KeyPerformed(string key) {
        AddFeedbackText(key);
        SetLastInputText(key);
        
        Debug.Log("Performing " + key + " " + PrintKeys());
        
        // NOTE: Not sure if this should be done when action first started instead, but it's here for now
        //AddToCurrentKeys((KeyboardKeys)Enum.Parse(typeof(KeyboardKeys), key));
        myKeys.Add( (KeyboardKeys)Enum.Parse(typeof(KeyboardKeys), key) );

        
        Debug.Log(key + " added to " + PrintKeys() + "? ");
        //synth.KeyPerformed(key);
    }

    /// <summary>
    /// Called when a key action has been "canceled"
    /// </summary>
    /// <param name="key"></param>
    public void KeyCanceled(string key) {
        //RemoveFromCurrentKeys((KeyboardKeys)Enum.Parse(typeof(KeyboardKeys), key));
        Debug.Log("Canceling " + key + " from " + PrintKeys());
        myKeys.Remove((KeyboardKeys)Enum.Parse(typeof(KeyboardKeys), key));
        Debug.Log(PrintKeys());
        //synth.KeyCanceled(key);

    }

    public String PrintKeys() {
        string result = "[";


        for(int a = 0; a < myKeys.Count; a++) {
            result += myKeys[a].ToString() + " ";
        }

        return result + "]";
    }

    #endregion

    ///<summary>
    /// An entire region of hand-written handler functions set up for later use
    ///</summary>
    #region On press "X"

    /// <summary>
    /// Called by the Input System as a Unity Event when ESC is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressESC(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("ESC");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF1(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F1");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF2(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F2");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F3 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF3(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F3");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF4(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F4");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF5(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F5");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F6 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF6(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F6");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F7 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF7(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F7");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F8 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF8(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F8");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F9 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF9(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F9");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F10 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF10(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F10");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F11 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF11(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("F11");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F12 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF12(InputAction.CallbackContext context) {

           if (context.performed) {
            //KeyPerformed("F12");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Tilde is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTilde(InputAction.CallbackContext context) {

            if (context.performed) {
            //KeyPerformed("`");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress1(InputAction.CallbackContext context) {

            if (context.performed) {
            //KeyPerformed("1");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress2(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("2");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 3 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress3(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("3");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress4(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("4");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress5(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("5");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 6 is firs pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress6(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("6");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 7 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress7(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("7");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 8 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress8(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("8");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// 
    /// Called by the Input System as a Unity Event when 9 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress9(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("9");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }


    /// <summary>
    /// Called by the Input System as a Unity Event when 0 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress0(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("0");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Minus is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMinus(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("-");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Equals is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEquals(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("=");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Backspace is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackspace(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Backspace");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Tab is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTab(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Tab");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Q is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressQ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Q");
        }

        if (context.canceled) {
            KeyCanceled("Q");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when W is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressW(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("W");
        }

        if (context.canceled) {
            KeyCanceled("W");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when E is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressE(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("E");
        }

        if (context.canceled) {
            KeyCanceled("E");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when R is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressR(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("R");
        }

        if (context.canceled) {
            KeyCanceled("R");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when T is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressT(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("T");
        }

        if (context.canceled) {
            KeyCanceled("T");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Y is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressY(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Y");
        }

        if (context.canceled) {
            KeyCanceled("Y");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when U is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressU(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("U");
        }

        if (context.canceled) {
            KeyCanceled("U");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when i is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressI(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("I");
        }

        if (context.canceled) {
            KeyCanceled("I");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when O is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressO(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("O");
        }

        if (context.canceled) {
            KeyCanceled("O");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when P is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressP(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("P");
        }

        if (context.canceled) {
            KeyCanceled("P");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftBracket is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Leftbracket");
        }

        if (context.canceled) {
            KeyCanceled("Leftbracket");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightBracket is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Rightbracket");
        }

        if (context.canceled) {
            KeyCanceled("Rightbracket");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Backslash is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackslash(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("\\");
        } // not sure if this works correctly, let's see

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when CapsLock is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressCapsLock(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("CapsLock");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when A is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressA(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("A");
        }

        if (context.canceled) {
            KeyCanceled("A");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when S is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressS(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("S");
        }

        if (context.canceled) {
            KeyCanceled("S");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when D is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressD(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("D");
        }

        if (context.canceled) {
            KeyCanceled("D");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("F");
        }

        if (context.canceled) {
            KeyCanceled("F");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when G is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressG(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("G");
        }

        if (context.canceled) {
            KeyCanceled("G");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when H is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressH(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("H");
        }

        if (context.canceled) {
            KeyCanceled("H");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when J is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressJ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("J");
        }

        if (context.canceled) {
            KeyCanceled("J");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when K is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressK(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("K");
        }

        if (context.canceled) {
            KeyCanceled("K");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when L is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressL(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("L");
        }

        if (context.canceled) {
            KeyCanceled("L");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Semicolon is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSemicolon(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Semicolon");
        }

        if (context.canceled) {
            KeyCanceled("Semicolon");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Apostrophe is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressApostrophe(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Apostrophe");
        }

        if (context.canceled) {
            KeyCanceled("Apostrophe");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Enter is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEnter(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("ENTER");
        } // use this differently for submitting, etc

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftShift is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLShift(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("LeftShift");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Z is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressZ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Z");
        }

        if (context.canceled) {
            KeyCanceled("Z");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when X is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressX(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("X");
        }

        if (context.canceled) {
            KeyCanceled("X");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when C is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressC(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("C");
        }

        if (context.canceled) {
            KeyCanceled("C");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when V is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressV(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("V");
        }

        if (context.canceled) {
            KeyCanceled("V");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when B is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressB(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("B");
        }

        if (context.canceled) {
            KeyCanceled("B");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when N is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressN(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("N");
        }

        if (context.canceled) {
            KeyCanceled("N");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when M is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressM(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("M");
        }

        if (context.canceled) {
            KeyCanceled("M");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Comma is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressComma(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Comma");
        }

        if (context.canceled) {
            KeyCanceled("Comma");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Period is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressPeriod(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed("Period");
        }

        if (context.canceled) {
            KeyCanceled("Period");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when ForwardSlash is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressForwardslash(InputAction.CallbackContext context) {
        if (context.performed) {
            //KeyPerformed("/");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightShift is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRShift(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("RightShift");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftControl is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLControl(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("LeftControl");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftAlt is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("LeftAlt");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Spacebar is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSpacebar(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("_");
        } // shows as "_" for now, so its not empty empty

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightAlt is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("RightAlt");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightControl is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRControl(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("RightControl");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftArrow is First Pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("LeftArrow");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when UpArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressUpArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("UpArrow");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("RightArrow");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when DownArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressDownArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("DownArrow");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse1(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Mouse1");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse2(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Mouse2");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse3 is first pressed
    /// /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse3(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Mouse3");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse4(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Mouse4");
        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse5(InputAction.CallbackContext context) {

        if (context.performed) {
            //KeyPerformed("Mouse5");

        }

        if (context.canceled) {
            //KeyCanceled("");
        }

    }

    #endregion

}