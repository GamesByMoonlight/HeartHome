using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement_Follow : MonoBehaviour {
    [Header("Use fields in HeartState to edit these values")]
    public float DistanceFollowed = 1.0f;
    public float ToolLatchDistance = 5.0f;  // This is the distance to a tool for the heart to start moving towards.
    public float MoveToPlayerSpeed = 4.0f;
    public float MoveToToolSpeed = 4.0f;
    public float LerpToPlayerDrag = 0.05f;
    public float CircleDistance = 1.0f;
    public float RotateSpeed = 2f;

    public Transform followTarget;
    public string ToolTag = "Tool";

    private Transform[] ToolLocations;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
                HeartCircle(target);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, (target - transform.position).normalized * MoveSpeed, .1f);
            
            }
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, LerpToPlayerDrag); 
        }

    }

    private void HeartCircle(Vector3 target)
    {
        rb.velocity = Vector2.zero;
        transform.RotateAround(target, new Vector3(0f, 0f, 1f), RotateSpeed);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

    }
}
