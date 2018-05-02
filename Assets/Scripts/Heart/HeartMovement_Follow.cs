using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartMovement_Follow : MonoBehaviour {
    [Header("Use fields in HeartState to edit these values")]
    public float DistanceFollowed = 1.0f;
    public float ToolLatchDistance = 5.0f;  // This is the distance to a tool for the heart to start moving towards.
    public float MoveToPlayerSpeed = 4.0f;
    public float MoveToToolSpeed = 4.0f;
    public float LerpToPlayerDrag = 0.05f;
    public float CircleDistance = 1.0f;
    public float RotateSpeed = 2f;

    public GameObject player;
    public string ToolTag = "Tool";

    private Transform[] ToolLocations;

    Rigidbody2D rb;
    GameObject TargetGameObject;
    float MoveSpeed;
    bool circlingTargetFlag = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TargetGameObject = null;
    }

    // Use this for initialization
    void Start ()
    {
        GameEventSystem.Instance.ToolsChanged.AddListener(GlobalUpdateToolListener);
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateTools();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GlobalUpdateToolListener();
    }

    void GlobalUpdateToolListener()
    {
        UpdateTools();
        TargetGameObject = null;
    }

    void UpdateTools()
    {
        var temp = GameObject.FindGameObjectsWithTag(ToolTag);
        ToolLocations = new Transform[temp.Length];

        // This apparently clears or goes null when an object is removed from the map. 
        // So I don't need to keep checking every so often if things are static.
        for (int i = 0; i < ToolLocations.Length; i++)
        {
            ToolLocations[i] = temp[i].GetComponent<Transform>();
        }
    }

    void RefreshTargetGameObject()
    {
        float dist = 0.0f;
        bool needToUpdate = false;
        float latchDistance = ToolLatchDistance;

        if(TargetGameObject != null && TargetGameObject != player)
        {
            dist = Vector3.Distance(TargetGameObject.transform.position, player.transform.position);
            var info = TargetGameObject.GetComponent<CustomHeartLatchInfo>();
            latchDistance = info != null ? info.Distance : ToolLatchDistance;
            if (dist < latchDistance)
                return;
        }

        TargetGameObject = null;
        MoveSpeed = 0.0f;
        foreach (Transform tool in ToolLocations)
        {
            if (tool != null)
            {
                dist = Vector3.Distance(tool.position, player.transform.position);
                var info = tool.GetComponent<CustomHeartLatchInfo>();
                latchDistance = info != null ? info.Distance : ToolLatchDistance;
                if (dist < latchDistance)
                {
                    TargetGameObject = tool.gameObject;
                    MoveSpeed = MoveToToolSpeed;
                    break;
                }
            }
            else
            {
                needToUpdate = true;
            }

        }
        if (needToUpdate)
            UpdateTools();
        
    }


    // FixedUpdate is called as often as possible
    void FixedUpdate()
    {
        RefreshTargetGameObject();


        // If no tool is found to latch to, move towards the player.
        if (TargetGameObject == null)
        {
            var dist = Vector3.Distance(player.transform.position, transform.position);

            if (dist > DistanceFollowed)
            {
                TargetGameObject = player;
                MoveSpeed = MoveToPlayerSpeed;
            }
        }

        if (TargetGameObject != null)
        {
            var dist = Vector3.Distance(TargetGameObject.transform.position, transform.position);
            var target = TargetGameObject.transform.position;
            if (dist <= CircleDistance)
            {
                //
                // Figure out the current angle between the objects.
                //
                var playerDist = Vector3.Distance(TargetGameObject.transform.position, player.transform.position);
                if(playerDist <= CircleDistance)
                    HeartCircle(target, 2f);
                else
                    HeartCircle(target, 1f);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, (target - transform.position).normalized * MoveSpeed, .1f);
            }
        }
        else
        {
            if(rb.velocity == Vector2.zero && circlingTargetFlag)
            {
                // Just to give it a little kick when it's done circling so the heart doesn't look like such a goob after you pick up an item
                circlingTargetFlag = false;
                rb.velocity = Vector3.Cross(new Vector3(0f, 0f, -1f), (player.transform.position - transform.position).normalized).normalized * RotateSpeed;
            }
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, LerpToPlayerDrag); 
        }

    }

    private void HeartCircle(Vector3 target, float boost)
    {
        rb.velocity = Vector2.zero;
        transform.RotateAround(target, new Vector3(0f, 0f, 1f), RotateSpeed * boost);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        circlingTargetFlag = true;
    }

    private void OnDisable()
    {
        // A good habit to get into.  Putting this on disable instead of ondestroy bc that's how the documentation shows it
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // A good habit to get into
        if(GameEventSystem.Instance != null)
            GameEventSystem.Instance.ToolsChanged.RemoveListener(GlobalUpdateToolListener);
    }
}
