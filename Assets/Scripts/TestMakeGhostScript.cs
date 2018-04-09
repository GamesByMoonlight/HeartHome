using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMakeGhostScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(GameEventSystem.Instance != null)
            GameEventSystem.Instance.MakeGhost.AddListener(MakeGhost);
	}

    void MakeGhost()
    {
        Debug.Log("Make Ghost!");
    }

    private void OnDestroy()
    {
        if(GameEventSystem.Instance != null)
        {
            GameEventSystem.Instance.MakeGhost.RemoveListener(MakeGhost);
        }
    }

}
