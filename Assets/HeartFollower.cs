using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFollower : MonoBehaviour {

    public Transform heart;
	
	// Update is called once per frame
	void Update () {
        if (heart)
            transform.position = heart.transform.position;
	}
}
