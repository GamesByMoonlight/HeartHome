using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlFollower : MonoBehaviour {

    public Transform girl;
	
	// Update is called once per frame
	void Update () {
        if (girl)
            transform.position = girl.transform.position;
	}
}
