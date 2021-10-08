using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles our 2D Theremin player
/// </summary>
public class ThereminPlayer : MonoBehaviour {

    /// <summary>
    /// Our mouse input value, controls when to play
    /// our instrument
    /// </summary>
    private float mouseClickInput;
    
    /// <summary>
    /// The oscillator used to
    /// synthesize our music
    /// </summary>
    private Oscillator oscillator;

    /// <summary>
    /// Pixel coordinate for the last position on screen
    /// for our mouth
    /// </summary>
    [SerializeField]
    private Vector2 lastMousePositionPixel;

    /// <summary>
    /// The lowest note we can play on our Theremin
    /// </summary>
    public MusicNote lowNote = new MusicNote(SharpNotes.A, 0);
    /// <summary>
    /// The highest note we can play on our Theremin
    /// </summary>
    public MusicNote highNote = new MusicNote(SharpNotes.E, 2);

    /// <summary>
    /// The frequency we want to set our oscillator to
    /// </summary>
    private float currentFrequency;
    /// <summary>
    /// The "gain" we want to set our oscillator to
    /// Figure out if this should be volume instead
    /// </summary>
    private float currentVolume;
    /// <summary>
    /// A Rectangle area on our canvas that defines our instrument's
    /// interaction and "play" area on screen
    /// </summary>
    private RectTransform playArea;
    /// <summary>
    /// A corner array for our play area
    /// </summary>
    private Vector3[] corners = new Vector3[4];

    /// <summary>
    /// 
    /// </summary>
    public GameObject localInstrumentPrefab;
    /// <summary>
    /// 
    /// </summary>
    MeshRenderer instrumentMeshRenderer;
    /// <summary>
    /// The next color that our sphere will turn into
    /// </summary>
    public Color baseColor = Color.red;

    private void Start() {
        // Cache some stuff
        oscillator = GetComponent<Oscillator>();
        playArea = GameObject.Find("Play Area").GetComponent<RectTransform>();
        localInstrumentPrefab = GameObject.Find("LocalInstrument");

        // Initialize play area
        InitPlayArea();

        // Set up instrument color changing
        instrumentMeshRenderer = localInstrumentPrefab.GetComponent<MeshRenderer>();
    }

    private void Update() {

        Color nextColor = new Color(baseColor.r * currentVolume, baseColor.g * currentVolume, baseColor.b * currentVolume);
        instrumentMeshRenderer.material.SetColor("_BaseColor", nextColor);
        
        
        localInstrumentPrefab.transform.localScale = new Vector3(0.1f * currentFrequency, 0.1f * currentFrequency, 0.1f * currentFrequency);


    }

    private void FixedUpdate() {
        currentFrequency = GetFrequencyFromMouse();
        currentVolume = GetVolumeFromMouse();
        oscillator.SetVolume(currentVolume);

        

        


    }

    #region input system

    /// <summary>
    /// Callled by the Input System as a Unity Event when
    /// the mouse is clicked or held down
    /// 
    /// they also 
    /// </summary>
    /// <param name="context"></param>
    public void OnPressPlay(InputAction.CallbackContext context) {

        mouseClickInput = context.ReadValue<float>();

        Debug.Log("Click!");


        if (RectTransformUtility.RectangleContainsScreenPoint(playArea, lastMousePositionPixel)) {

            // If the mouse has been clicked or is still being held down, keep
            switch (mouseClickInput) {
                case 1f:
                    oscillator.StartPlay(currentFrequency, currentVolume);
                    //oscillator.StartPlay();

                    break;
                case 0f:
                    oscillator.EndPlay();
                    break;
            }

        } else {
            oscillator.EndPlay();
        }

    }

    /// <summary>
    /// Callled by the Input System as a Unity Event when
    /// the mouse is moved.
    /// 
    /// Since our frequency AND volume are dependent on mouse position,
    /// we also need to make sure to let the oscillator know NOT to stop playing
    /// until we have a "0f" playInput
    /// </summary>
    /// <param name="context"></param>
    public void OnAim(InputAction.CallbackContext context) {

        if (RectTransformUtility.RectangleContainsScreenPoint(playArea, lastMousePositionPixel)) {

            lastMousePositionPixel = context.ReadValue<Vector2>();


        }


        currentFrequency = GetFrequencyFromMouse();
        currentVolume = GetVolumeFromMouse();

        oscillator.SetFrequency(currentFrequency);
        oscillator.SetVolume(currentVolume);

    }

    #endregion

    #region low and high notes

    /// <summary>
    /// Get our low note
    /// </summary>
    /// <returns></returns>
    public MusicNote GetLowNote() {
        return lowNote;
    }

    /// <summary>
    /// Set our low note
    /// </summary>
    /// <param name="note">The note we want to use</param>
    public void SetLowNote(MusicNote note) {
        lowNote = note;
    }

    /// <summary>
    /// Get our high note
    /// </summary>
    /// <returns></returns>
    public MusicNote GetHighNote() {
        return highNote;
    }

    /// <summary>
    /// Set our high note
    /// </summary>
    /// <param name="note">The note we want to use</param>
    public void SetHighNote(MusicNote note) {
        highNote = note;
    }
    
    #endregion

    /// <summary>
    /// Uses our lastMousePositionPixel
    /// 
    /// Maps our minimum and maximum frequencies to our screen's height,
    /// so our mouse returns a percentage of how high up the screen it is
    /// </summary>
    /// <returns></returns>
    public float GetFrequencyFromMouse() {

        // get the positions of our play area's cornerss
        // 0 is bot left, 1 is top left?
        // Remap from the y values used for our play area's corners to 0-1
        // (AKA normalize our input)
        float normalizedY = Remap(lastMousePositionPixel.y, corners[0].y, corners[1].y, 0f, 1f);

        // Remap our normalized input into our frequency
        return Remap(normalizedY, 0f, 1f, lowNote.equalTemperamentfrequency, highNote.equalTemperamentfrequency);
    }

    /// <summary>
    /// Maps our mouse position to a volume value we can use later
    /// </summary>
    /// <returns></returns>
    public float GetVolumeFromMouse() {

        // Corner 0 is bottom left, corner 2 is top right
        float normalizedX = Remap(lastMousePositionPixel.x, corners[0].x, corners[2].x, 0f, 1f);

        return normalizedX;
    }

    /// <summary>
    /// Get our play area's variables ready
    /// </summary>
    private void InitPlayArea() {

        playArea.GetWorldCorners(corners);

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
