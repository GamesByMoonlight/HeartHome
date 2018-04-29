using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleMusic : MonoBehaviour {

    public enum Track { melody, harp, violin, percussion }

    public AudioClip[] audioClips;

    private List<AudioSource> audioSources;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
        {
            audioSources.Add(child.GetComponent<AudioSource>());
        }

        StartCoroutine(StartPartA());
	}
	
    public void StartTrack(Track track)
    {
        StartCoroutine(FadeIn(track));
    }

    public void StopTrack(Track track)
    {
        StartCoroutine(FadeOut(track));
    }

    IEnumerator FadeIn(Track track)
    {
        yield return new WaitForSeconds(0.1f);

        audioSources[(int)track].volume += 0.05f;

        int itemCounter = -1;

        foreach (AudioSource audio in audioSources)
        {
            if (audio.volume == 1)
            {
                itemCounter++;
            }
        }

        if (itemCounter == 3)
        {
            StartCoroutine(StartPartB(audioSources[0].timeSamples));
        }

        if (audioSources[(int)track].volume < 1)
        {
            StartTrack(track);
        }
    }

    IEnumerator FadeOut(Track track)
    {
        yield return new WaitForSeconds(0.1f);

        audioSources[(int)track].volume -= 0.05f;

        if (audioSources[(int)track].volume > 0)
        {
            StopTrack(track);
        }
    }

    IEnumerator StartPartA()
    {
        yield return new WaitForSeconds(8f);

        audioSources[0].clip = audioClips[0];
        audioSources[0].loop = true;

        audioSources[0].Play();
        audioSources[1].Play();
        audioSources[2].Play();
        audioSources[3].Play();
    }

    IEnumerator StartPartB(float samplesPlayed)
    {
        yield return new WaitForSeconds(58f - (samplesPlayed / 32000));

        audioSources[0].clip = audioClips[1];
        audioSources[1].clip = audioClips[2];
        audioSources[2].clip = audioClips[3];
        audioSources[3].clip = audioClips[4];
    }
}
