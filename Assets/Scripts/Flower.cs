using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to identify flower objects, if needed, and to add behaviors later

public class Flower : MonoBehaviour {
    public static Flower OldestFlower;

    private static Flower YoungestFlower;
    private static int FlowersCreated = 0;
    private static bool GhostMade = false;  // Some extra reduntancy 

    Flower next;
    public Flower Next { get { return next; } }
    bool alive = true;
    public bool Alive { get { return alive; } }

    Animator animator;

    private void Awake()
    {
        // Create a linked list
        if (OldestFlower == null)
        {
            OldestFlower = this;
        }

        if (YoungestFlower != null)
        {
            YoungestFlower.next = this;
        }
        YoungestFlower = this;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GetComponent<FlowerGrowth>().PlayAudioClip();
        FlowersCreated++;

        if (FlowersCreated == 4)
        {
            GameEventSystem.Instance.MakePaintbrush.Invoke();
        }

        if(FlowersCreated >= 40 && !GhostMade)
        {
            GhostMade = true;
            GameEventSystem.Instance.MakeGhost.Invoke();
        }
    }

    public void Kill()
    {
        alive = false;
        animator.SetTrigger("Die");           
    }
}
