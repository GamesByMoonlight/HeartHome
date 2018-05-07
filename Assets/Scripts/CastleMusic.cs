using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleMusic : MonoBehaviour {

    public enum Track { melody, harp, violin, percussion }

    public AudioClip[] audioClips;

    private List<AudioSource> audioSources = new List<AudioSource>();


	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
        {
            audioSources.Add(child.GetComponent<AudioSource>());
        }

        StartPartA();
	}
	
    public void StartTrack(string trackString)
    {
        switch (trackString)
        {
            case "harp":
                StartCoroutine(FadeIn(Track.harp));
                return;
            case "violin":
                StartCoroutine(FadeIn(Track.violin));
                return;
            case "percussion":
                StartCoroutine(FadeIn(Track.percussion));
                return;
            default:
                Debug.Log("No track associated with string " + trackString);
                break;
        }
                
    }

    public void StopTrack(string trackString)
    {
        switch (trackString)
        {
            case "harp":
                StartCoroutine(FadeOut(Track.harp));
                return;
            case "violin":
                StartCoroutine(FadeOut(Track.violin));
                return;
            case "percussion":
                StartCoroutine(FadeOut(Track.percussion));
                return;
            default:
                Debug.Log("No track associated with string " + trackString);
                break;
        }
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

        if (itemCounter == 2)
        {
            Debug.Log("Doing this");
            StartCoroutine(StartPartB(audioSources[0].timeSamples));
        }

        if (audioSources[(int)track].volume < 1)
        {
            StartCoroutine(FadeIn(track));
        }
    }

    IEnumerator FadeOut(Track track)
    {
        yield return new WaitForSeconds(0.1f);

        audioSources[(int)track].volume -= 0.05f;

        if (audioSources[(int)track].volume > 0)
        {
            StartCoroutine(FadeOut(track));
        }
    }

    void StartPartA()
    {
        //yield return new WaitForSeconds(8f);

        audioSources[0].clip = audioClips[0];
        audioSources[0].loop = true;

        audioSources[0].Play();
        audioSources[1].Play();
        audioSources[2].Play();
        audioSources[3].Play();

    }

    IEnumerator StartPartB(float samplesPlayed)
    {
        float partATime = audioSources[1].isPlaying ? (58f - (samplesPlayed / 32000)) : (66f - (samplesPlayed / 32000));

        yield return new WaitForSeconds(partATime);

        audioSources[0].clip = audioClips[1];
        audioSources[1].clip = audioClips[2];
        audioSources[2].clip = audioClips[3];
        audioSources[3].clip = audioClips[4];
        audioSources[0].Play();
        audioSources[1].Play();
        audioSources[2].Play();
        audioSources[3].Play();
    }
}
