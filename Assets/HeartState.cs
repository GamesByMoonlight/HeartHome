using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartState : MonoBehaviour {

    public enum HeartStateValues { Happy, Cold, Cursed, Broken, Frozen };

    public HeartStateValues currentState = HeartStateValues.Happy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
