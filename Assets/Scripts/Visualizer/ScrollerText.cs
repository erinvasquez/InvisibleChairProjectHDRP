using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollerText : MonoBehaviour {

    private TextMeshProUGUI textComponent;
    public TMP_FontAsset[] fonts = new TMP_FontAsset[15];
    int currentFont = 0;
    float lastTimeFontChanged = 0f;

    private void Awake() {
        textComponent = GetComponent<TextMeshProUGUI>();
    }


    private void Start() {

        Debug.Log("Font Asset: " + transform.gameObject.GetComponent<TextMeshProUGUI>().font);
    

    }


    // Update is called once per frame
    void Update() {

        if (!Conductor.visualizerReady) {
            return;
        }

        
        if (Conductor.songPositionInSeconds - lastTimeFontChanged >= Conductor.secondsPerBeat / 4) {

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
