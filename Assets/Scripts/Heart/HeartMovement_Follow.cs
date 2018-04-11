using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMovement_Follow : MonoBehaviour {

    public float DistanceFollowed = 0.0f;
    // This is the distance to a tool for the heart to start moving towards.
    public float ToolLatchDistance = 0.0f;
    public float MoveToPlayerSpeed = 0.0f;
    public float MoveToToolSpeed = 0.0f;
    public Transform followTarget;
    public string ToolTag = "Tool";

    private Transform[] ToolLocations;

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

        Debug.Log("Num Tools: " + temp.Length);
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
            if (Vector3.Distance(tool.position, followTarget.position) < ToolLatchDistance)
            {
                Debug.Log(Vector3.Distance(tool.position, followTarget.position));
                target = tool.position;
                targetFound = true;
                MoveSpeed = MoveToToolSpeed;
            }
        }

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
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }
}
