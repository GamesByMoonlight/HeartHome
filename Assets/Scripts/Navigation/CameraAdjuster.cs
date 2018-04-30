using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour {
    public float MaxAdjust = 2.4f;
    public float speed = .3f;
    public bool adjusting = true;

    Camera mc;
    Vector3 old;

    private void Awake()
    {
        mc = Camera.main;
    }

    private void Start()
    {
        old = mc.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float diff = (old.y - mc.transform.position.y) * speed;


        if(mc.transform.localPosition.y < MaxAdjust && diff < 0f)
        {
            Adjust(diff);
        }
        else if(mc.transform.localPosition.y > 0f && diff > 0f)
        {
            Adjust(diff);
        }
        old = mc.transform.position;
	}

    void Adjust(float byAmount)
    {
        if (!adjusting)
            return;
        
        float diff = byAmount;
        //if (mc.transform.localPosition.y > MaxAdjust / 3f)
        //    diff *= 2f;
        //else
            //diff /= 2f;
    
        mc.transform.position = mc.transform.position - new Vector3(0f, diff);    
    }
}
