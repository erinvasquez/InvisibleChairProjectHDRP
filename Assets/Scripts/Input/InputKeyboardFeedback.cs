using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

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

    // A script component on this empty gameobject somewhere, moving its code here
    //KeyboardFeedBackText feedbackText;

    // our text objects
    TextMeshProUGUI feedbackText;
    TextMeshProUGUI currentInputText;
    TextMeshProUGUI mousePosText;

    // Our input action set, currently set to our feedback text actions
    InputActionAsset keyboardMouseTestActions;
    Button okButton;

    // our scroll input
    float scroll = 0f;


    private void Start() {

        // Get our text game objects
        feedbackText = GameObject.Find("FeedbackText").GetComponent<TextMeshProUGUI>();
        currentInputText = GameObject.Find("CurrentInputText").GetComponent<TextMeshProUGUI>();
        mousePosText = GameObject.Find("MousePositionText").GetComponent<TextMeshProUGUI>();

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
            ProcessKey("ScrollDown");
        } else if (scroll > 0f) {
            ProcessKey("ScrollUp");
        }

        Debug.Log(Mouse.current.position.ReadValue());
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

    #region Getters/Setters

    void setFeedbackText(string text) {
        feedbackText.text = text;
    }

    void addFeedbackText(string text) {
        feedbackText.text += text;
    }

    void setLastInputText(string text) {
        currentInputText.text = "LAST INPUT: " + text;
    }

    #endregion


    /// <summary>
    /// Takes a string from our keyboard input and adds it to our input text log when first
    /// pressed, and displays the last known key pressed
    /// </summary>
    /// <para name="context"></param>
    public void ProcessKey(string key) {
            addFeedbackText(key);
            setLastInputText(key);
    }

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
            ProcessKey("ESC");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF1(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F1");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF2(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F2");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F3 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF3(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F3");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF4(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F4");
            }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF5(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F5");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F6 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF6(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F6");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F7 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF7(InputAction.CallbackContext context) {

            if (context.performed) {
                ProcessKey("F7");
            }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F8 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF8(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("F8");
            }
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F9 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF9(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("F9");
            }
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F10 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF10(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("F10");
            }
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F11 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF11(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("F11");
            }
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F12 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF12(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("F12");
            }
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Tilde is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTilde(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("`");
            }
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress1(InputAction.CallbackContext context) {

        if (context.performed) {
            if (context.performed) {
                ProcessKey("1");
            }
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress2(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("2");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 3 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress3(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("3");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress4(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("4");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress5(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("5");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 6 is firs pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress6(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("6");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 7 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress7(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("7");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when 8 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress8(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("8");
        }

    }

    /// <summary>
    /// 
    /// Called by the Input System as a Unity Event when 9 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress9(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("9");
        }

    }


    /// <summary>
    /// Called by the Input System as a Unity Event when 0 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPress0(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("0");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Minus is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMinus(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("-");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Equals is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEquals(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("=");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Backspace is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackspace(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Backspace");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Tab is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTab(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Tab");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Q is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressQ(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Q");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when W is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressW(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("W");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when E is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressE(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("E");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when R is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressR(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("R");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when T is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressT(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("T");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Y is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressY(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Y");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when U is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressU(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("U");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when i is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressI(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("I");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when O is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressO(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("O");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when P is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressP(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("P");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftBracket is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("[");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightBracket is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("]");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Backslash is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackslash(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("\\");
        } // not sure if this works correctly, let's see

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when CapsLock is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressCapsLock(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("CapsLock");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when A is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressA(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("A");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when S is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressS(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("S");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when D is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressD(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("D");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("F");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when G is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressG(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("G");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when H is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressH(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("H");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when J is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressJ(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("J");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when K is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressK(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("K");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when L is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressL(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("L");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Semicolon is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSemicolon(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey(";");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Apostrophe is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressApostrophe(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("'");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Enter is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEnter(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("ENTER");
        } // use this differently for submitting, etc

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftShift is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLShift(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("LeftShift");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Z is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressZ(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Z");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when X is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressX(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("X");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when C is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressC(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("C");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when V is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressV(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("V");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when B is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressB(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("B");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when N is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressN(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("N");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when M is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressM(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("M");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Comma is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressComma(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey(",");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Period is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressPeriod(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey(".");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when ForwardSlash is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressForwardslash(InputAction.CallbackContext context) {
        if (context.performed) {
            ProcessKey("/");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightShift is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRShift(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("RightShift");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftControl is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLControl(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("LeftControl");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftAlt is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("LeftAlt");
        }
    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Spacebar is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSpacebar(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("_");
        } // shows as "_" for now, so its not empty empty

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightAlt is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("RightAlt");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightControl is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRControl(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("RightControl");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when LeftArrow is First Pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("LeftArrow");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when UpArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressUpArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("UpArrow");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when RightArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("RightArrow");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when DownArrow is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressDownArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("DownArrow");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse1 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse1(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Mouse1");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse2 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse2(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Mouse2");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse3 is first pressed
    /// /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse3(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Mouse3");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse4 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse4(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Mouse4");
        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when Mouse5 is first pressed
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMouse5(InputAction.CallbackContext context) {

        if (context.performed) {
            ProcessKey("Mouse5");
            
        }

    }
    #endregion


}