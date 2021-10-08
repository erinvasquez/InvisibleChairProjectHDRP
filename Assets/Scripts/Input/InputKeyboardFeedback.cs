using UnityEngine;
using UnityEngine.InputSystem;

public class InputKeyboardFeedback : MonoBehaviour {

    KeyboardFeedBackText feedbackText;

    private void Start() {
        feedbackText = GameObject.Find("FeedbackText").GetComponent<KeyboardFeedBackText>();
    }

    void FixedUpdate() {


    }

    /// <summary>
    /// Called by the Input System as a Unity Event when the ESC is pressed or held down
    /// </summary>
    /// <param name="context"></param>
    public void OnPressESC(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("ESC");

        switch (input) {
            case 1f:
                Debug.Log("ESC still being pressed!");
                feedbackText.SetInput("ESC");
                break;

            case 0f:
                Debug.Log("ESC not being pressed anymore");
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F1 is pressed or held down
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF1(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F1");

        switch (input) {
            case 1f:
                Debug.Log("F1 still being pressed!");
                feedbackText.SetInput("F1");
                break;

            case 0f:
                Debug.Log("F1 not being pressed anymore");
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// Called by the Input System as a Unity Event when F2 is pressed or held down
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF2(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F2");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F2");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF3(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F3");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F3");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF4(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F4");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F4");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF5(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F5");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F5");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF6(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F6");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F6");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF7(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F7");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F7");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF8(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F8");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F8");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF9(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F9");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F9");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF10(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F10");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F10");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF11(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F11");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F11");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF12(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F12");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F12");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTilde(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("~");

        switch (input) {
            case 1f:
                feedbackText.SetInput("~");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress1(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("1");

        switch (input) {
            case 1f:
                feedbackText.SetInput("1");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress2(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("2");

        switch (input) {
            case 1f:
                feedbackText.SetInput("2");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress3(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("3");

        switch (input) {
            case 1f:
                feedbackText.SetInput("3");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress4(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("4");

        switch (input) {
            case 1f:
                feedbackText.SetInput("4");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress5(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("5");

        switch (input) {
            case 1f:
                feedbackText.SetInput("5");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress6(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("6");

        switch (input) {
            case 1f:
                feedbackText.SetInput("6");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress7(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("7");

        switch (input) {
            case 1f:
                feedbackText.SetInput("7");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress8(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("8");

        switch (input) {
            case 1f:
                feedbackText.SetInput("8");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress9(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("9");

        switch (input) {
            case 1f:
                feedbackText.SetInput("9");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }


    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPress0(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("0");

        switch (input) {
            case 1f:
                feedbackText.SetInput("0");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressMinus(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("-");

        switch (input) {
            case 1f:
                feedbackText.SetInput("-");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEquals(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("=");

        switch (input) {
            case 1f:
                feedbackText.SetInput("=");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackspace(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Backspace");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Backspace");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressTab(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("TAB");

        switch (input) {
            case 1f:
                feedbackText.SetInput("TAB");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressQ(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Q");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Q");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressW(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("W");

        switch (input) {
            case 1f:
                feedbackText.SetInput("W");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressE(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("E");

        switch (input) {
            case 1f:
                feedbackText.SetInput("E");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressR(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("R");

        switch (input) {
            case 1f:
                feedbackText.SetInput("R");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressT(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("T");

        switch (input) {
            case 1f:
                feedbackText.SetInput("T");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressY(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Y");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Y");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressU(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("U");

        switch (input) {
            case 1f:
                feedbackText.SetInput("U");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressI(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("I");

        switch (input) {
            case 1f:
                feedbackText.SetInput("I");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressO(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("O");

        switch (input) {
            case 1f:
                feedbackText.SetInput("O");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressP(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("P");

        switch (input) {
            case 1f:
                feedbackText.SetInput("P");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftBracket(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("[");

        switch (input) {
            case 1f:
                feedbackText.SetInput("[");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightBracket(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("]");

        switch (input) {
            case 1f:
                feedbackText.SetInput("]");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressBackslash(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("\\");

        switch (input) {
            case 1f:
                feedbackText.SetInput("\\");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressCapsLock(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("CapsLock");

        switch (input) {
            case 1f:
                feedbackText.SetInput("CapsLock");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressA(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("A");

        switch (input) {
            case 1f:
                feedbackText.SetInput("A");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressS(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("S");

        switch (input) {
            case 1f:
                feedbackText.SetInput("S");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressD(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("D");

        switch (input) {
            case 1f:
                feedbackText.SetInput("D");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressF(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("F");

        switch (input) {
            case 1f:
                feedbackText.SetInput("F");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressG(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("G");

        switch (input) {
            case 1f:
                feedbackText.SetInput("G");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressH(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("H");

        switch (input) {
            case 1f:
                feedbackText.SetInput("H");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressJ(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("J");

        switch (input) {
            case 1f:
                feedbackText.SetInput("J");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressK(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("K");

        switch (input) {
            case 1f:
                feedbackText.SetInput("K");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressL(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("L");

        switch (input) {
            case 1f:
                feedbackText.SetInput("L");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSemicolon(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput(";");

        switch (input) {
            case 1f:
                feedbackText.SetInput(";");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressApostrophe(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("'");

        switch (input) {
            case 1f:
                feedbackText.SetInput("'");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressEnter(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Enter");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Enter");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLShift(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("LShift");

        switch (input) {
            case 1f:
                feedbackText.SetInput("LShift");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressZ(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Z");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Z");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressX(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("X");

        switch (input) {
            case 1f:
                feedbackText.SetInput("X");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressC(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("C");

        switch (input) {
            case 1f:
                feedbackText.SetInput("C");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressV(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("V");

        switch (input) {
            case 1f:
                feedbackText.SetInput("V");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressB(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("B");

        switch (input) {
            case 1f:
                feedbackText.SetInput("B");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressN(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("N");

        switch (input) {
            case 1f:
                feedbackText.SetInput("N");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressM(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("M");

        switch (input) {
            case 1f:
                feedbackText.SetInput("M");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressComma(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput(",");

        switch (input) {
            case 1f:
                feedbackText.SetInput(",");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressPeriod(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput(".");

        switch (input) {
            case 1f:
                feedbackText.SetInput(".");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressForwardslash(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("/");

        switch (input) {
            case 1f:
                feedbackText.SetInput("/");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRShift(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("RShift");

        switch (input) {
            case 1f:
                feedbackText.SetInput("RShift");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLControl(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("LControl");

        switch (input) {
            case 1f:
                feedbackText.SetInput("LControl");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLAlt(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("LAlt");

        switch (input) {
            case 1f:
                feedbackText.SetInput("LAlt");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressSpacebar(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("Spacebar");

        switch (input) {
            case 1f:
                feedbackText.SetInput("Spacebar");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRAlt(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("RAlt");

        switch (input) {
            case 1f:
                feedbackText.SetInput("RAlt");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRControl(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("RControl");

        switch (input) {
            case 1f:
                feedbackText.SetInput("RControl");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressLeftArrow(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("LeftArrow");

        switch (input) {
            case 1f:
                feedbackText.SetInput("LeftArrow");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressUpArrow(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("UpArrow");

        switch (input) {
            case 1f:
                feedbackText.SetInput("UpArrow");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressRightArrow(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("RightArrow");

        switch (input) {
            case 1f:
                feedbackText.SetInput("RightArrow");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public void OnPressDownArrow(InputAction.CallbackContext context) {
        float input = context.ReadValue<float>();

        feedbackText.SetInput("DownArrow");

        switch (input) {
            case 1f:
                feedbackText.SetInput("DownArrow");
                break;

            case 0f:
                feedbackText.SetInput("");
                break;

        }

    }
}