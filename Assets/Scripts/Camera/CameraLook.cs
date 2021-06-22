using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour {
    public Camera cam;

    Transform player;

    public float sensitivityX = .033f;
    public float sensitivityY = .033f;
    public float minY = -89f;
    public float maxY = 89f;

    public float lookX;
    public float lookY;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = transform.parent;
        cam = Camera.main;

    }

    void Update() {

        // Add some way to stop our camera from "looking" while paused
        // Hook this in with any pause menu manager functionalities
        if (Keyboard.current.escapeKey.wasPressedThisFrame) {
            ToggleCursorMode();
        }

        if (Cursor.lockState == CursorLockMode.Locked) {
            Look();
        }

        Debug.Log("Camera forward direction: " + transform.forward);

    }

    /// <summary>
    /// Handle our mouse look input from below
    /// </summary>
    public void Look() {

        lookY = Mathf.Clamp(lookY, minY, maxY);

        player.localEulerAngles = new Vector3(0, lookX, 0);

        cam.transform.localEulerAngles = new Vector3(lookY, 0, 0);

    }

    
    /// <summary>
    /// Get Mouse look X delta
    /// </summary>
    /// <param name="context"></param>
    public void OnLookX(InputAction.CallbackContext context) {
        lookX += context.ReadValue<float>() * sensitivityX /** Time.deltaTime*/;
    }

    /// <summary>
    /// Get Mouse look Y delta
    /// </summary>
    /// <param name="context"></param>
    public void OnLookY(InputAction.CallbackContext context) {
        lookY -= context.ReadValue<float>() * sensitivityY /** Time.deltaTime*/;
    }

    private void ToggleCursorMode() {
        Cursor.visible = !Cursor.visible;

        if (Cursor.lockState == CursorLockMode.None) {
            Cursor.lockState = CursorLockMode.Locked;
        } else {
            Cursor.lockState = CursorLockMode.None;
        }

    }

}
