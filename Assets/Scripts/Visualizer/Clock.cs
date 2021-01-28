using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// From a tutorial from catlikecoding.com/unity/tutorials/basics
/// </summary>
public class Clock : MonoBehaviour {

    [SerializeField]
    public Transform hoursPivot = default, minutesPivot, secondsPivot;

    const float hoursToDegrees = -30f;
    const float minutesToDegrees = -6f;
    const float secondsToDegrees = -6f;


    // Start is called before the first frame update
    void Awake() {
        Debug.Log(DateTime.Now);
        DateTime time = DateTime.Now;

        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * time.Hour);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * time.Minute);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondsToDegrees * time.Second);

    }

    /// <summary>
    /// 
    /// </summary>
    private void Update() {
        TimeSpan time = DateTime.Now.TimeOfDay;

        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * (float) time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * (float) time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondsToDegrees * (float) time.TotalSeconds);

    }

}
