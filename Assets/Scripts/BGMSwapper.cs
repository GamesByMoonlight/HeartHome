using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwapper : MonoBehaviour {

    private BGMManager bgmManager;

    private void Awake()
    {
        bgmManager = FindObjectOfType<BGMManager>();
    }

    // Use this for initialization
    public void SwapToTrack (int track) {
        try
        {
            bgmManager.ChangeTrack(track);
        }
        catch (System.Exception)
        {
            throw;
        }

	}
	
	public void SetTrackVolume(int track, float volume)
    {
        bgmManager.SetVolume(track, volume);
    }
}
