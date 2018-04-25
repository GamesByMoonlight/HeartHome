using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToLoopingBody : MonoBehaviour {

    /*      A class to transition from an intro into a looping body of music.
     *      Calculates transition from first child to second child based on BPM and beats in intro
     */

    public float bpm, beatsInIntro;
    private float _delay;

    private List<AudioSource> _track = new List<AudioSource>();

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
            _track.Add(child.GetComponent<AudioSource>());

        _delay = bpm * beatsInIntro / 60;

        StartCoroutine(ChangeTracks(_delay));
    }
	
	IEnumerator ChangeTracks(float delay)
    {
        yield return new WaitForSeconds(delay);
        _track[0].Stop();
        _track[1].loop = true;
        _track[1].Play();
    }
}
