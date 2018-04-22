using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSwapper : MonoBehaviour {

    private BGMManager bgmManager;
    
	// Use this for initialization
	void Start () {
        bgmManager = FindObjectOfType<BGMManager>();

        try
        {
            bgmManager.ChangeTrack(1);
        }
        catch (System.Exception)
        {
            throw;
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
