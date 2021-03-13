using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThereminPlayer : MonoBehaviour {

    float playInput;
    Oscillator oscillator;

    [SerializeField]
    Vector2 lastMousePositionPixel;
    [SerializeField]
    Vector2 lastMousePositionPercent;

    public PitchClass minimumNote = new PitchClass(Notes.GSHARP, 4);
    public PitchClass maximumNote = new PitchClass(Notes.A, 4);

    float currentFrequency;
    float currentGain;

    private void Start() {
        oscillator = GetComponent<Oscillator>();
    }

    private void OnValidate() {

        oscillator = GetComponent<Oscillator>();
    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromMouse();
        currentGain = GetVolumeFromMouse();
    }

    public void OnPressPlay(InputAction.CallbackContext context) {

        playInput = context.ReadValue<float>();

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentGain);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
        }

    }

    public void OnAim(InputAction.CallbackContext context) {

        lastMousePositionPixel = context.ReadValue<Vector2>();
        lastMousePositionPercent = new Vector2(lastMousePositionPixel.x / (float)Screen.width, lastMousePositionPixel.y / (float)Screen.height);

        switch (playInput) {
            case 1f:
                oscillator.StartPlay(currentFrequency, currentGain);
                break;
            case 0f:
                oscillator.EndPlay();
                break;
        }

    }

    public PitchClass GetMinNote() {
        return minimumNote;
    }

    public void SetMinNote(PitchClass note) {
        minimumNote = note;
    }




    public PitchClass GetMaxNote() {
        return maximumNote;
    }

    public void SetMaxNote(PitchClass note) {
        maximumNote = note;
    }

    /// <summary>
    /// Maps our minimum and maximum frequencies to our screen's height,
    /// so our mouse returns a percentage of how high up the screen it is
    /// </summary>
    /// <returns></returns>
    public float GetFrequencyFromMouse() {
        // we have a float for how high the mouse is
        // lastMousePositionPercent.y
        // we want frequency to be maxFrequency when lastMousePositionPercent.y == 1f
        //                         minFrequency when ... == 0f;
        //  and everything in between

        // Set to the minimum first
        float frequency = lastMousePositionPercent.y;

        return Remap(frequency, 0f, 1f, minimumNote.frequency, maximumNote.frequency);
    }

    /// <summary>
    /// Maps our mouse position to a volume value we can use later
    /// </summary>
    /// <returns></returns>
    public float GetVolumeFromMouse() {
        return lastMousePositionPercent.x;
    }

    /// <summary>
    /// Used to remap between two ranges
    /// </summary>
    /// <param name="value"></param>
    /// <param name="from1"></param>
    /// <param name="to1"></param>
    /// <param name="from2"></param>
    /// <param name="to2"></param>
    /// <returns></returns>
    public float Remap(float value, float from1, float to1, float from2, float to2) {
        value = (value - from1) / (to1 - from1) * (to2 - from2) + from2;

        return value;
    }

}
