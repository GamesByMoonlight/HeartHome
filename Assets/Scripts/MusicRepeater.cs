using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRepeater : MonoBehaviour {

    /* A very gnarly, hard-coded way to loop a piece after a certain intro period
     * Could be expanded by doing calculations based on BPM of intro (as long as it was constant),
     * # of beats in intro.  
     * 
     * Although this is actually inferior to just recording the intro and the looping section separately,
     * then just delaying the play of the looping section until after the intro.  I already have this
     * started so I'm going to finish it, but lesson learned...
     */


    AudioSource audioSource;
    int lastTimeSample = 0;
    int thisTimeSample = 0;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        lastTimeSample = thisTimeSample;
        thisTimeSample = audioSource.timeSamples;

        // Using this and last sample count to create a very small buffer to help avoid a single-frame gap in audio
        if (audioSource.timeSamples >= 2944000 - (thisTimeSample -lastTimeSample))
        {
            audioSource.timeSamples = 384000;
            audioSource.Play();
            thisTimeSample = 384000;
        }


            
	}
}
