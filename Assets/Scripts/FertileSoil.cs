using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileSoil : MonoBehaviour {

    public GameObject ThrownSeedsPrefab;
    public GameObject FlowerPrefab;

    bool seedsPlanted = false;
    GameObject seeds;

    public void PlantSeeds()
    {
        seedsPlanted = true;
        seeds = Instantiate(ThrownSeedsPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);

    }

    internal void WaterSoil()
    {
        if (!seedsPlanted)
            return;
        
        var flower = Instantiate(FlowerPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);
        Destroy(seeds);
    }
}
