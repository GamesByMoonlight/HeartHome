using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    private List<AudioSource> _musicTracks = new List<AudioSource>();

	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
            _musicTracks.Add(child.GetComponent<AudioSource>());
	}

    public void SetVolume(int trackNumber, float volume)
    {
        if(trackNumber < _musicTracks.Count)
            _musicTracks[trackNumber].volume = volume;
    }

    public void ChangeTrack(int trackNumber)
    {
        foreach (AudioSource track in _musicTracks)
        {
            track.Stop();
        }

        _musicTracks[trackNumber].Play();
    }
}
