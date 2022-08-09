using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// Handles the creation of our UI keys, and how they
/// react to our key presses.
/// 
/// Instantiate our key display
/// Get our keyboard input, and apply it to these keys.
/// 
/// The largest US/ANSI Keyboards have 108 Keys,
/// F1 to F12, PrtScr, ScrlLk, Pause
/// `, 1 to 0, -, = Backspace
/// TAB Q to P, [, ], \,
/// CAPS, A to L, ; ' Enter
/// LShift, Z to M, ,, ., /, RShift
/// LCtrl, Windows, LAlt, Spacebar, RAlt, Windows, Fn, Ctrl,
/// Insert, Home, PageUp
/// Delete, End, PageDown
/// Up, Down, Left, Right
/// Num Lock, , *, -
/// Numpad 7 to 3, +
/// 0, ., Enter
/// 
/// Basically, what I want to do with this is get our currently
/// pressed keys from another script (like InputKeyboardFeedback, or an improved version),
/// and use that to make the appropriate action happen for our key display here
/// </summary>
public class KeyDisplay : MonoBehaviour {

    List<GameObject> displayKeys = new List<GameObject>();

    GameObject EscapeCanvas;
    GameObject F1Canvas;
    GameObject F2Canvas;
    GameObject F3Canvas;
    GameObject F4Canvas;
    GameObject F5Canvas;
    GameObject F6Canvas;
    GameObject F7Canvas;
    GameObject F8Canvas;
    GameObject F9Canvas;
    GameObject F10Canvas;
    GameObject F11Canvas;
    GameObject F12Canvas;
    GameObject F13Canvas;
    GameObject F14Canvas;
    GameObject BackquoteCanvas;
    GameObject Alpha1Canvas;
    GameObject Alpha2Canvas;
    GameObject Alpha3Canvas;
    GameObject Alpha4Canvas;
    GameObject Alpha5Canvas;
    GameObject Alpha6Canvas;
    GameObject Alpha7Canvas;
    GameObject Alpha8Canvas;
    GameObject Alpha9Canvas;
    GameObject MinusCanvas;
    GameObject EqualCanvas;
    GameObject BackspaceCanvas;
    GameObject TabCanvas;
    GameObject QCanvas;
    GameObject WCanvas;
    GameObject ECanvas;
    GameObject RCanvas;
    GameObject TCanvas;
    GameObject YCanvas;
    GameObject UCanvas;
    GameObject ICanvas;
    GameObject OCanvas;
    GameObject PCanvas;
    GameObject LeftBracketCanvas;
    GameObject RightBracketCanvas;
    GameObject BackslashCanvas;
    GameObject CapsLockCanvas;
    GameObject ACanvas;
    GameObject SCanvas;
    GameObject DCanvas;
    GameObject FCanvas;
    GameObject GCanvas;
    GameObject HCanvas;
    GameObject JCanvas;
    GameObject KCanvas;
    GameObject LCanvas;
    GameObject SemicolonCanvas;
    GameObject QuoteCanvas;
    GameObject EnterCanvas;
    GameObject LeftShiftCanvas;
    GameObject ZCanvas;
    GameObject XCanvas;
    GameObject CCanvas;
    GameObject VCanvas;
    GameObject BCanvas;
    GameObject NCanvas;
    GameObject MCanvas;
    GameObject CommaCanvas;
    GameObject PeriodCanvas;
    GameObject SlashCanvas;
    GameObject RightShiftCanvas;
    GameObject LeftControlCanvas;
    GameObject LeftAltCanvas;
    GameObject SpaceCanvas;
    GameObject RightAltCanvas;
    GameObject RightControlCanvas;
    GameObject PrintScreenCanvas;
    GameObject ScrollLockCanvas;
    GameObject PauseCanvas;
    GameObject InsertCanvas;
    GameObject HomeCanvas;
    GameObject PageUpCanvas;
    GameObject DeleteCanvas;
    GameObject EndCanvas;
    GameObject PageDownCanvas;
    GameObject UpArrowCanvas;
    GameObject LeftArrowCanvas;
    GameObject DownArrowCanvas;
    GameObject RightArrowCanvas;
    GameObject NumLockCanvas;
    GameObject NumpadDivideCanvas;
    GameObject NumpadMultiplyCanvas;
    GameObject NumpadMinusCanvas;
    GameObject Numpad7Canvas;
    GameObject Numpad8Canvas;
    GameObject Numpad9Canvas;
    GameObject Numpad4Canvas;
    GameObject Numpad5Canvas;
    GameObject Numpad6Canvas;
    GameObject Numpad1Canvas;
    GameObject Numpad2Canvas;
    GameObject Numpad3Canvas;
    GameObject Numpad0Canvas;
    GameObject NumpadPeriodCanvas;
    GameObject NumpadPlusCanvas;
    GameObject NumpadEnterCanvas;




    private void Start() {

        Debug.Log(Keyboard.KeyCount);

        InstantiateKeys();

    }

    /// <summary>
    /// Instantiate our variables, like our keys
    /// </summary>
    void InstantiateKeys() {

        KeyControl space = Keyboard.current.spaceKey;



        Debug.Log("Key for space is " + space + ", " + Keyboard.current["#(space)"] + ", " + Keyboard.current[Key.Space]);


        // Start finding our canvases
        EscapeCanvas = GameObject.Find("Escape Canvas");
        F1Canvas = GameObject.Find("F1 Canvas");
        F2Canvas = GameObject.Find("F2 Canvas");
        F3Canvas = GameObject.Find("F3 Canvas");
        F4Canvas = GameObject.Find("F4 Canvas");
        F5Canvas = GameObject.Find("F5 Canvas");
        F6Canvas = GameObject.Find("F6 Canvas");
        F7Canvas = GameObject.Find("F7 Canvas");
        F8Canvas = GameObject.Find("F8 Canvas");
        F9Canvas = GameObject.Find("F9 Canvas");
        F10Canvas = GameObject.Find("F10 Canvas");
        F11Canvas = GameObject.Find("F11 Canvas");
        F12Canvas = GameObject.Find("F12 Canvas");
        F13Canvas = GameObject.Find("F13 Canvas");
        F14Canvas = GameObject.Find("F14 Canvas");
        BackquoteCanvas = GameObject.Find("Backquote Canvas");
        Alpha1Canvas = GameObject.Find("Alpha1 Canvas");
        Alpha2Canvas = GameObject.Find("Alpha2 Canvas");
        Alpha3Canvas = GameObject.Find("Alpha3 Canvas");
        Alpha4Canvas = GameObject.Find("Alpha4 Canvas");
        Alpha5Canvas = GameObject.Find("Alpha5 Canvas");
        Alpha6Canvas = GameObject.Find("Alpha6 Canvas");
        Alpha7Canvas = GameObject.Find("Alpha7 Canvas");
        Alpha8Canvas = GameObject.Find("Alpha8 Canvas");
        Alpha9Canvas = GameObject.Find("Alpha9 Canvas");
        MinusCanvas = GameObject.Find("Minus Canvas");
        EqualCanvas = GameObject.Find("Equal Canvas");
        BackspaceCanvas = GameObject.Find("Backspace Canvas");
        TabCanvas = GameObject.Find("Tab Canvas");
        QCanvas = GameObject.Find("Q Canvas");
        WCanvas = GameObject.Find("W Canvas");
        ECanvas = GameObject.Find("E Canvas");
        RCanvas = GameObject.Find("R Canvas");
        TCanvas = GameObject.Find("T Canvas");
        YCanvas = GameObject.Find("Y Canvas");
        UCanvas = GameObject.Find("U Canvas");
        ICanvas = GameObject.Find("I Canvas");
        OCanvas = GameObject.Find("O Canvas");
        PCanvas = GameObject.Find("P Canvas");
        LeftBracketCanvas = GameObject.Find("LeftBracket Canvas");
        RightBracketCanvas = GameObject.Find("RightBracket Canvas");
        BackslashCanvas = GameObject.Find("Backslash Canvas");
        CapsLockCanvas = GameObject.Find("CapsLock Canvas");
        ACanvas = GameObject.Find("A Canvas");
        SCanvas = GameObject.Find("S Canvas");
        DCanvas = GameObject.Find("D Canvas");
        FCanvas = GameObject.Find("F Canvas");
        GCanvas = GameObject.Find("G Canvas");
        HCanvas = GameObject.Find("H Canvas");
        JCanvas = GameObject.Find("J Canvas");
        KCanvas = GameObject.Find("K Canvas");
        LCanvas = GameObject.Find("L Canvas");
        SemicolonCanvas = GameObject.Find("Semicolon Canvas");
        QuoteCanvas = GameObject.Find("Quote Canvas");
        EnterCanvas = GameObject.Find("Enter Canvas");
        LeftShiftCanvas = GameObject.Find("LeftShift Canvas");
        ZCanvas = GameObject.Find("Z Canvas");
        XCanvas = GameObject.Find("X Canvas");
        CCanvas = GameObject.Find("C Canvas");
        VCanvas = GameObject.Find("V Canvas");
        BCanvas = GameObject.Find("B Canvas");
        NCanvas = GameObject.Find("N Canvas");
        MCanvas = GameObject.Find("M Canvas");
        CommaCanvas = GameObject.Find("Comma Canvas");
        PeriodCanvas = GameObject.Find("Period Canvas");
        SlashCanvas = GameObject.Find("Slash Canvas");
        RightShiftCanvas = GameObject.Find("RightShift Canvas");
        LeftControlCanvas = GameObject.Find("LeftControl Canvas");
        LeftAltCanvas = GameObject.Find("LeftAlt Canvas");
        SpaceCanvas = GameObject.Find("Space Canvas");
        RightAltCanvas = GameObject.Find("RightAlt Canvas");
        RightControlCanvas = GameObject.Find("RightControl Canvas");
        PrintScreenCanvas = GameObject.Find("PrintScreen Canvas");
        ScrollLockCanvas = GameObject.Find("ScrollLock Canvas");
        PauseCanvas = GameObject.Find("Pause Canvas");
        InsertCanvas = GameObject.Find("Insert Canvas");
        HomeCanvas = GameObject.Find("Home Canvas");
        PageUpCanvas = GameObject.Find("PageUp Canvas");
        DeleteCanvas = GameObject.Find("Delete Canvas");
        EndCanvas = GameObject.Find("End Canvas");
        PageDownCanvas = GameObject.Find("PageDown Canvas");
        UpArrowCanvas = GameObject.Find("UpArrow Canvas");
        LeftArrowCanvas = GameObject.Find("LeftArrow Canvas");
        DownArrowCanvas = GameObject.Find("DownArrow Canvas");
        RightArrowCanvas = GameObject.Find("RightArrow Canvas");
        NumLockCanvas = GameObject.Find("NumLock Canvas");
        NumpadDivideCanvas = GameObject.Find("NumpadDivide Canvas");
        NumpadMultiplyCanvas = GameObject.Find("NumpadMultiply Canvas");
        NumpadMinusCanvas = GameObject.Find("NumpadMinus Canvas");
        Numpad7Canvas = GameObject.Find("Numpad7 Canvas");
        Numpad8Canvas = GameObject.Find("Numpad8 Canvas");
        Numpad9Canvas = GameObject.Find("Numpad9 Canvas");
        Numpad4Canvas = GameObject.Find("Numpad4 Canvas");
        Numpad5Canvas = GameObject.Find("Numpad5 Canvas");
        Numpad6Canvas = GameObject.Find("Numpad6 Canvas");
        Numpad1Canvas = GameObject.Find("Numpad1 Canvas");
        Numpad2Canvas = GameObject.Find("Numpad2 Canvas");
        Numpad3Canvas = GameObject.Find("Numpad3 Canvas");
        Numpad0Canvas = GameObject.Find("Numpad0 Canvas");
        NumpadPeriodCanvas = GameObject.Find("NumpadPeriod Canvas");
        NumpadPlusCanvas = GameObject.Find("NumpadPlus Canvas");
        NumpadEnterCanvas = GameObject.Find("NumpadEnter Canvas");



    }

    /// <summary>
    /// I feel like this was sample code from somewhere
    /// </summary>
    /// <param name="character"></param>
    void OnTextInput(char character) {

    }

    /// <summary>
    /// Called by Unity Input Manager Unity events
    /// </summary>
    /// <param name="keyCode"></param>
    void KeyPerformed(KeyCode keyCode) {

        Debug.Log(keyCode + " performed");

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyCode"></param>
    void KeyCanceled(KeyCode keyCode) {
        Debug.Log(keyCode + " cancelled");
    }

    #region On Press

    public void OnPressBackspace(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Backspace);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Backspace);
        }


    }

    public void OnPressTab(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Tab);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Tab);
        }


    }

    public void OnPressEnter(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Return);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Return);
        }

    }

    public void OnPressPause(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Pause);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Pause);
        }

    }

    public void OnPressEscape(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Escape);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Escape);
        }

    }

    public void OnPressSpace(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Space);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Space);
        }

    }

    public void OnPressQuote(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Quote);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Quote);
        }

    }

    public void OnPressComma(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Comma);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Comma);
        }

    }

    public void OnPressMinus(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Minus);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Minus);
        }


    }

    public void OnPressPeriod(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Period);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Period);
        }


    }

    public void OnPressSlash(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Slash);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Slash);
        }

    }

    public void OnPressAlpha0(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha0);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha0);
        }


    }

    public void OnPressAlpha1(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha1);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha1);
        }


    }

    public void OnPressAlpha2(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha2);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha2);
        }


    }

    public void OnPressAlpha3(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha3);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha3);
        }


    }

    public void OnPressAlpha4(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha4);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha4);
        }


    }

    public void OnPressAlpha5(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha5);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha5);
        }


    }

    public void OnPressAlpha6(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha6);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha6);
        }


    }

    public void OnPressAlpha7(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha7);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha7);
        }


    }

    public void OnPressAlpha8(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha8);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha8);
        }


    }

    public void OnPressAlpha9(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Alpha9);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Alpha9);
        }


    }

    public void OnPressSemicolon(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Semicolon);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Semicolon);
        }


    }

    public void OnPressEquals(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Equals);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Equals);
        }


    }

    public void OnPressLeftBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.LeftBracket);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.LeftBracket);
        }


    }

    public void OnPressBackslash(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Backslash);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Backslash);
        }


    }

    public void OnPressRightBracket(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.RightBracket);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.RightBracket);
        }


    }

    public void OnPressBackquote(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.BackQuote);
        }
        if (context.canceled) {
            KeyCanceled(KeyCode.BackQuote);
        }


    }

    public void OnPressA(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.A);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.A);
        }


    }

    public void OnPressB(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.B);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.B);
        }


    }


    public void OnPressC(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.C);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.C);
        }


    }


    public void OnPressD(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.D);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.D);
        }


    }


    public void OnPressE(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.E);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.E);
        }


    }

    public void OnPressF(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F);
        }


    }

    public void OnPressG(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.G);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.G);
        }


    }

    public void OnPressH(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.H);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.H);
        }


    }

    public void OnPressI(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.I);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.I);
        }


    }

    public void OnPressJ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.J);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.J);
        }


    }

    public void OnPressK(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.K);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.K);
        }


    }

    public void OnPressL(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.L);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.L);
        }


    }

    public void OnPressM(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.M);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.M);
        }


    }

    public void OnPressN(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.N);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.N);
        }


    }

    public void OnPressO(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.O);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.O);
        }


    }

    public void OnPressP(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.P);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.P);
        }


    }

    public void OnPressQ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Q);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Q);
        }


    }

    public void OnPressR(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.R);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.R);
        }


    }

    public void OnPressS(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.S);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.S);
        }


    }

    public void OnPressT(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.T);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.T);
        }


    }

    public void OnPressU(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.U);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.U);
        }


    }

    public void OnPressV(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.V);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.V);
        }


    }

    public void OnPressW(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.W);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.W);
        }


    }

    public void OnPressX(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.X);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.X);
        }


    }

    public void OnPressY(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Y);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Y);
        }


    }

    public void OnPressZ(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Z);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Z);
        }


    }


    public void OnPressDelete(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Delete);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Delete);
        }


    }

    public void OnPressKeypad0(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad0);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad0);
        }


    }

    public void OnPressKeypad1(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad1);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad1);
        }


    }

    public void OnPressKeypad2(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad2);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad2);
        }


    }

    public void OnPressKeypad3(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad3);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad3);
        }


    }

    public void OnPressKeypad4(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad4);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad4);
        }


    }

    public void OnPressKeypad5(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad5);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad5);
        }


    }

    public void OnPressKeypad6(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad6);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad6);
        }


    }

    public void OnPressKeypad7(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad7);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad7);
        }


    }

    public void OnPressKeypad8(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad8);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad8);
        }


    }

    public void OnPressKeypad9(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Keypad9);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Keypad9);
        }


    }

    public void OnPressKeypadPeriod(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Period);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Period);
        }


    }

    public void OnPressKeypadDivide(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadDivide);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadDivide);
        }


    }

    public void OnPressKeypadMultiply(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadMultiply);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadMultiply);
        }


    }

    public void OnPressKeypadMinus(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadMinus);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadMinus);
        }


    }

    public void OnPressKeypadPlus(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadPlus);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadPlus);
        }


    }

    public void OnPressKeypadEnter(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadEnter);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadEnter);
        }


    }

    public void OnPressKeypadEquals(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.KeypadEquals);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.KeypadEquals);
        }


    }

    public void OnPressUpArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.UpArrow);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.UpArrow);
        }


    }

    public void OnPressDownArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.DownArrow);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.DownArrow);
        }


    }

    public void OnPressRightArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.RightArrow);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.RightArrow);
        }


    }

    public void OnPressLeftArrow(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.LeftArrow);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.LeftArrow);
        }


    }

    public void OnPressInsert(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Insert);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Insert);
        }


    }

    public void OnPressHome(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Home);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Home);
        }


    }

    public void OnPressEnd(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.End);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.End);
        }


    }

    public void OnPressPageUp(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.PageUp);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.PageUp);
        }


    }

    public void OnPressPageDown(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.PageDown);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.PageDown);
        }


    }

    public void OnPressF1(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F1);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F1);
        }


    }

    public void OnPressF2(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F2);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F2);
        }


    }

    public void OnPressF3(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F3);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F3);
        }


    }

    public void OnPressF4(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F4);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F4);
        }


    }

    public void OnPressF5(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F5);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F6);
        }


    }

    public void OnPressF6(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F6);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F6);
        }


    }

    public void OnPressF7(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F7);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F7);
        }


    }

    public void OnPressF8(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F8);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F8);
        }


    }

    public void OnPressF9(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F9);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F9);
        }


    }

    public void OnPressF10(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F10);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F10);
        }


    }

    public void OnPressF11(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F11);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F11);
        }


    }

    public void OnPressF12(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.F12);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.F12);
        }


    }



    public void OnPressF15(InputAction.CallbackContext context) {

        Debug.Log("KeyCode.Pause was used for F15. Reconsider later?");

        if (context.performed) {
            KeyPerformed(KeyCode.Pause);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Pause);
        }


    }


    public void OnPressNumLock(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Numlock);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Numlock);
        }


    }


    public void OnPressCapsLock(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.CapsLock);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.CapsLock);
        }


    }


    public void OnPressScrollLock(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.ScrollLock);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.ScrollLock);
        }


    }



    public void OnPressRightShift(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.RightShift);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.RightShift);
        }


    }


    public void OnPressLeftShift(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.LeftShift);
        } else if (context.performed) {
            KeyPerformed(KeyCode.LeftShift);
        }

    }

    public void OnPressRightControl(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.RightControl);
        } else if (context.performed) {
            KeyPerformed(KeyCode.RightControl);
        }

    }

    public void OnPressLeftControl(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.LeftControl);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.LeftControl);
        }


    }

    public void OnPressRightAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.RightAlt);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.RightAlt);
        }


    }

    public void OnPressLeftAlt(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.LeftAlt);
        } else if (context.performed) {
            KeyPerformed(KeyCode.LeftAlt);
        }

    }

    public void OnPressPrintScreen(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Print);
        } else if (context.performed) {
            KeyPerformed(KeyCode.Print);
        }

    }

    public void OnPressMouse1(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse1);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse1);
        }


    }

    public void OnPressMouse2(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse2);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse2);
        }


    }

    public void OnPressMouse3(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse3);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse3);
        }


    }

    public void OnPressMouse4(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse4);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse4);
        }


    }

    public void OnPressMouse5(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse5);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse5);
        }


    }

    public void OnPressMouse6(InputAction.CallbackContext context) {

        if (context.performed) {
            KeyPerformed(KeyCode.Mouse6);
        } else if (context.canceled) {
            KeyCanceled(KeyCode.Mouse6);
        }

    }

    #endregion
}
