using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour {
    public float MaxAdjust = 2.4f;
    public float speed = .1f;

    Camera mc;
    Vector3 old;
    float start;

    private void Awake()
    {
        mc = Camera.main;
    }

    private void Start()
    {
        old = mc.transform.position;
        start = MaxAdjust;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float diff = (old.y - mc.transform.position.y);


        if(mc.transform.localPosition.y < MaxAdjust && diff < 0f)
        {
            mc.transform.position = mc.transform.position - new Vector3(0f, diff);
        }
        else if(mc.transform.localPosition.y > 0f && diff > 0f)
        {
            mc.transform.position = mc.transform.position - new Vector3(0f, diff);
        }
        old = mc.transform.position;
	}
}
