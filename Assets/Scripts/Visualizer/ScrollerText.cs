using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollerText : MonoBehaviour {

    private TextMeshProUGUI textComponent;
    public TMP_FontAsset[] fonts = new TMP_FontAsset[15];
    [Range(1, 16)]
    public int scrollRate = 4;
    int currentFont = 0;
    float lastTimeFontChanged = 0f;

    //bool moving = false;

    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
    }


    private void Start() {

    }


    // Update is called once per frame
    void Update() {

        if (!Conductor.musicSourceReadyForVisualizing) {
            return;
        }


        if (Conductor.songPositionInSeconds - lastTimeFontChanged >= Conductor.secondsPerBeat / scrollRate) {

            lastTimeFontChanged = Conductor.songPositionInSeconds;

            //ScrollText(textComponent);
            ScrollTextRandom(textComponent);

        }


    }


    void ScrollText(TextMeshProUGUI textComp) {
        textComp.font = fonts[currentFont];

        if (currentFont >= fonts.Length - 1) {
            currentFont = 0;
        }

        currentFont++;

    }

    void ScrollTextRandom(TextMeshProUGUI textComp) {

        textComp.font = fonts[Random.Range(0, fonts.Length)];

    }


}
