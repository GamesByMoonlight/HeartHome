using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement_Follow : MonoBehaviour {

    public float DistanceFollowed = 0.0f;
    public float MoveSpeed = 0.0f;
    public Transform followTarget;
    public float dist = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {

        dist = Vector3.Distance(followTarget.position, transform.position);

        if (dist > DistanceFollowed)
        {
            float step = MoveSpeed * Time.deltaTime;
            //transform.position = Vector3.Lerp(followTarget.position, transform.position, step);
            transform.position = Vector3.MoveTowards(transform.position, followTarget.position, step);
        }

    }
}
