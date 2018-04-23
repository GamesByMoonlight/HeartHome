using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    private List<AudioSource> _musicTracks = new List<AudioSource>();
    int currentTrack;

	// Use this for initialization
	void Start () {
        currentTrack = 0;
        foreach (Transform child in transform)
            _musicTracks.Add(child.GetComponent<AudioSource>());
	}

    public void SetVolume(float volume)
    {
        _musicTracks[currentTrack].volume = volume;
    }

    public void ChangeTrack(int trackNumber)
    {
        foreach (AudioSource track in _musicTracks)
        {
            track.Stop();
        }
        currentTrack = trackNumber;
        _musicTracks[trackNumber].Play();
    }
}
