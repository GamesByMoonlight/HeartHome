using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour {
    public Color NewBackgroundColor;

	// Use this for initialization
	void Start () {
        Debug.Log("Changing background color");
        Camera.main.backgroundColor = NewBackgroundColor;
	}
	
}
