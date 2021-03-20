using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// <summary>
/// A replacement for ScrollerText, since we don't
/// want to instantiate a bunch of fonts multiple times
/// and waste memory. I'm keeping it under control with this
/// here script.
/// </summary>
public class ScrollingText : MonoBehaviour {

    /// <summary>
    /// GameObjects for the individual characters
    /// </summary>
    //private GameObject textCharacters;
    /// <summary>
    /// A list of our each of our characters' TMPro text components
    /// </summary>
    //private TextMeshProUGUI[] textComponents;
    /// <summary>
    /// The font assets we'll use on this text
    /// </summary>
    public TMP_FontAsset[] fonts = new TMP_FontAsset[15];
    /// <summary>
    /// The text to be displayed by our scrolling text characters
    /// </summary>
    public string text = "";
    [Range(1, 16)]
    public int scrollRate = 4;
    /// <summary>
    /// The font we're currently using
    /// </summary>
    //private int fontIndex = 0;
    /// <summary>
    /// The last time in seconds our font was changed
    /// </summary>
    //private float lastTimeFontChanged = 0f;

    // Start is called before the first frame update
    void Start() {

        // Create each one of our characters in our text variable as set in the editor [or in script?]

    }

    // Update is called once per frame
    void Update() {

    }
}
