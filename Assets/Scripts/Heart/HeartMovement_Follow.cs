using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement_Follow : MonoBehaviour {

    private float DistanceFollowed = 1.0f;
    // This is the distance to a tool for the heart to start moving towards.
    private float ToolLatchDistance = 5.0f;
    private float MoveToPlayerSpeed = 4.0f;
    private float MoveToToolSpeed = 4.0f;
    private float CircleDistance = 1.0f;
    public Transform followTarget;
    public string ToolTag = "Tool";

    private Transform[] ToolLocations;
    private float angle = 0.0f;

	// Use this for initialization
	void Start ()
    {
        var temp = GameObject.FindGameObjectsWithTag(ToolTag);
        ToolLocations = new Transform[temp.Length];

        // This apparently clears or goes null when an object is removed from the map. 
        // So I don't need to keep checking every so often if things are static.
        for (int i = 0; i < ToolLocations.Length; i++)
        {
            ToolLocations[i] = temp[i].GetComponent<Transform>();
        }

        //Debug.Log("Num Tools: " + temp.Length);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log()
    }

    // FixedUpdate is called as often as possible
    void FixedUpdate()
    {
        float MoveSpeed = 0.0f;
        Vector3 target = new Vector3(0, 0, 0);
        bool targetFound = false;
        float dist = 0.0f;

        foreach (Transform tool in ToolLocations)
        {
            dist = Vector3.Distance(tool.position, followTarget.position);

            if (dist < ToolLatchDistance && dist > CircleDistance)
            {
                //Debug.Log("Target Distance = " + Vector3.Distance(tool.position, transform.position));
                target = tool.position;
                targetFound = true;
                MoveSpeed = MoveToToolSpeed;
            }
            else if (dist <= CircleDistance)
            {

            }
        }

        // If no tool is found to latch to, move towards the player.
        if (!targetFound)
        {
            dist = Vector3.Distance(followTarget.position, transform.position);

            if (dist > DistanceFollowed)
            {
                target = followTarget.position;
                targetFound = true;
                MoveSpeed = MoveToPlayerSpeed;
            }
        }

        if (targetFound)
        {
            dist = Vector3.Distance(target, transform.position);
            if (dist <= CircleDistance)
            {
                //
                // Figure out the current angle between the objects.
                //
                Vector3 dir = target - transform.position;
                //angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                HeartCircle(target);
            }
            else
            {
                float step = MoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
        }

    }

    private void HeartCircle(Vector3 target)
    {
        float RotateSpeed = 2f;

        angle += RotateSpeed * Time.deltaTime;

	    var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * CircleDistance;
	    transform.position = target + offset;

        //Debug.Log("Circling, Offset =" + offset);
    }
}
