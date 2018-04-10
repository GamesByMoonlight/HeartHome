using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to identify flower objects, if needed, and to add behaviors later

public class Flower : MonoBehaviour {
    public static Flower OldestFlower;

    private static Flower YoungestFlower;
    private static int FlowersCreated = 0;
    private static bool GhostMade = false;  // Some extra reduntancy 

    Flower Next;

    private void Awake()
    {
        // Create a linked list
        if (OldestFlower == null)
        {
            OldestFlower = this;
        }

        if (YoungestFlower != null)
        {
            YoungestFlower.Next = this;
        }
        YoungestFlower = this;
    }

    private void Start()
    {
        if(FlowersCreated == 0)
        {
            GetComponent<FlowerGrowth>().PlayAudioClip();
        }
        FlowersCreated++;

        if(FlowersCreated >= 40 && !GhostMade)
        {
            GhostMade = true;
            GameEventSystem.Instance.MakeGhost.Invoke();
        }
    }
}
