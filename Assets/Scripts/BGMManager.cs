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

    public void ChangeTrack(int trackNumber)
    {
        foreach (AudioSource track in _musicTracks)
        {
            track.Stop();
        }

        _musicTracks[trackNumber].Play();
    }
}
