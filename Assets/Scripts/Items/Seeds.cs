using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeds : Item {


    private PlayerAction playerAction;
    

    void Awake()
    {
        playerAction = GameObject.Find("Player").GetComponent<PlayerAction>();

        if (playerAction == null)
        {
            Debug.LogError("PlayerAction not found in scene. Hoe needs it");
        }


    }

    public override void UseAt(GameObject location)
    {

       
        var soil = location.GetComponent<FertileSoil>();
        if (soil != null )
        {
            soil.PlantSeeds();

        }
    }



}
