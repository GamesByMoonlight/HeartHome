using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// This should be added to the Player prefab only
/// </summary>
public class DontDestroyPlayerOnLoad : MonoBehaviour {
    public static PlayerAction playerObject;

	// Use this for initialization
	void Awake () {
        if(playerObject != null && playerObject.gameObject != gameObject)
        {
            Destroy(gameObject);
            return;
        }
        playerObject = GetComponent<PlayerAction>();
        DontDestroyOnLoad(gameObject);

        if(playerObject == null)
        {
            Debug.LogError("PlayerAction script not found on this prefab.  This script should only be added to the player prefab.");
        }
	}
	
}
