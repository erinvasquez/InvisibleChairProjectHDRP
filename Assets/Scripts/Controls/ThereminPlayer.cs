using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThereminPlayer : MonoBehaviour {

    float playInput;
    Oscillator oscillator;

    [SerializeField]
    Vector2 lastmousePositionPixel;
    [SerializeField]
    Vector2 lastmousePositionPercent;


    public HalfStepsFromA4 minPianoKey = HalfStepsFromA4.C0;
    int minKey;
    
    public HalfStepsFromA4 maxPianoKey = HalfStepsFromA4.E2;
    int maxKey;

    [SerializeField]
    float minFrequency;
    [SerializeField]
    public float maxFrequency;

    float currentFrequency;
    float currentVolume;

    private void Start() {
        oscillator = GetComponent<Oscillator>();

        minKey = (int) minPianoKey;
        maxKey = (int) maxPianoKey;

        minFrequency = oscillator.GetEqualTempramentFrequency((int) minPianoKey);
        maxFrequency = oscillator.GetEqualTempramentFrequency((int) maxPianoKey);

    }

    private void OnValidate() {

        oscillator = GetComponent<Oscillator>();

        minKey = (int)minPianoKey;
        maxKey = (int)maxPianoKey;

        minFrequency = oscillator.GetEqualTempramentFrequency((int)minPianoKey);
        maxFrequency = oscillator.GetEqualTempramentFrequency((int)maxPianoKey);
    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromMouse();
        currentVolume = GetVolumeFromMouse();
    }

    public void OnPressPlay(InputAction.CallbackContext context) {

        playInput = context.ReadValue<float>();

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentVolume);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
        }

    }

    public void OnAim(InputAction.CallbackContext context) {

        lastmousePositionPixel = context.ReadValue<Vector2>();
        lastmousePositionPercent = new Vector2(lastmousePositionPixel.x / (float)Screen.width, lastmousePositionPixel.y / (float)Screen.height);

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentVolume);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
        }

    }

    public float GetFrequencyFromMouse() {
        // we have a float for how high the mouse is
        // lastMousePositionPercent.y
        // we want frequency to be maxFrequency when lastMousePositionPercent.y == 1f
        //                         minFrequency when ... == 0f;
        //  and everything in between

        // Set to the minimum first
        float frequency = lastmousePositionPercent.y;
        
        return Remap(frequency, 0f, 1f, minFrequency, maxFrequency);
    }

    public float GetVolumeFromMouse() {
        return lastmousePositionPercent.x;
    }

    public float Remap(float value, float from1, float to1, float from2, float to2) {
        value = (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        return value;
    }

}
