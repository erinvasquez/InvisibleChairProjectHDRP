using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscilloscope : MonoBehaviour {
    //creating the arrays "total" holds both left and right channel's data, and later is splited into left and right seperatly
    private float[] totalSamples;
    private float[] leftChannel;
    private float[] rightChannel;

    public AudioSource audio;
   
  
    //playRate is number of samples played within a fixed update(0.02 sec)
    public int playRate;

    [Range(0f, 15f)]
    //afterGlowRef is just my way to advoid a red error on the first few frame, so retty much useless
    public int afterGlowRef;
    //how many time it will redraw the previous frame
    [Range (0f, 15f)]
    public int afterGlow;
    
    
    //how many of fixed update has past since the beginning
    public int cyclePlayed;
    public int totalCycles;
    //a vector3 to hold all the vectors of the graphed lines
    private Vector3[] points;
    //factor to multiply each axis
    public float zStretch;
    public float xStretch;
    public float yStretch;
    //i dont actaully know what "resolution" does so...
    public int resolution;




	void Start () {
        //AudioSource audio = GetComponent<AudioSource>();
       
         totalSamples = new float[audio.clip.samples * audio.clip.channels];
         leftChannel = new float[audio.clip.samples];
         rightChannel = new float[audio.clip.samples];
    
        //fill "totalsamples" with samples from the audioclip, the other perameter is an "offset", so 0 mean to read from the beginning, while 100 mean it will skip 100 samples and start reading from sample 101
        audio.clip.GetData(totalSamples, 0);

        //audio.clip.getdata will fill the array wih both left and right channel, and do it in a way that left channel and right channel alternates. the script below is a simple even/odd sort, so leftChannel[] has only the odd sample, and right has the even ones
        int l = 0;
        int r = 0;
        for (int i = 0; i < totalSamples.Length; i++) {
            
            if (i % 2 == 0)
            {
                leftChannel[l] = totalSamples[i];
                l++;
            }
            else {
                rightChannel[r] = totalSamples[i];
                r++;
            }
        }  
        //audio.clip.frequency is actually the sample rate of the audio clip. "(int)" is to force the  output to be an interger, although it really shouldntmatter since Time.delta time here will always out in 0.05, and audio.clip.frequency is always a multiple of 5
        playRate =(int)(audio.clip.frequency * Time.deltaTime);
        //how many time.deltatimes there is in this audio clip
        totalCycles = audio.clip.samples / playRate;
      
    }
	
	//using fixed update(updates every 0.05 sec) so the visual never lags behind or go ahead of the actual audio, since the visual and the audio is connected at all, making look like that they are in sync really matterss.
     void FixedUpdate()
    {

        //total numbere of line segments there should be
        points = new Vector3[playRate * afterGlow];
        //the minimal distance between 2 points, in theory at least. like i said im not too how this part really works..
        float increment = 1f / (resolution - 1);

        float z = 0;
        //so this is the juicy part, "playRate * cyclesPlayed" is all the samples that have been played already. "i - playrate * cyclesPlayed" is a tempory varible that keeps track of how many times the for loop ran. 
        //playRate * afterGlow" is the target, which is to fill every position in "points".
        for ( int i = playRate * cyclePlayed; i - playRate * cyclePlayed < playRate * afterGlow; i++) {
            //setting x and y value of each position in the array, z axis in this case is the time axis(kinda, not really. its only the time axis of this for loop, a proper time axis takes few more lines of code, but how i like how it is right now)
            points[i - playRate * cyclePlayed] = new Vector3((resolution * leftChannel[i] ) * increment * xStretch, (resolution * rightChannel[i]) * increment * yStretch, z * increment * zStretch);
            z++;
        }

        LineRenderer lineRender = GetComponent<LineRenderer>();
        //making sure the line is as long as the array is
        lineRender.positionCount = points.Length;
        //actually setting the positions of the line segments
        lineRender.SetPositions(points);
       
        //"after glow" redraws the past few frame as well as the current one, but at first there enough frame before the current one, so is to get arond that
        if (afterGlow < afterGlowRef)
        {
            afterGlow++;
        }
        
        cyclePlayed++;
        //to loop the visual to the beginning at the very end.
        if (cyclePlayed == totalCycles)
        {
            cyclePlayed = 0;
        }
    }

}
//i thought my code was like this enigma of a audio system, but after writting these comments it becomes so laughably simple...