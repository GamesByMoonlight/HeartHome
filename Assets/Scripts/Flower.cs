using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to identify flower objects, if needed, and to add behaviors later

public class Flower : MonoBehaviour {
    private static int FlowersCreated = 0;

    private void Start()
    {
        if(FlowersCreated == 0)
        {
            GetComponent<FlowerGrowth>().PlayAudioClip();
        }
        FlowersCreated++;
    }
}
