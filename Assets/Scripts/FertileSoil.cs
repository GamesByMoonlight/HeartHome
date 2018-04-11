using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileSoil : MonoBehaviour {

    public GameObject ThrownSeedsPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlantSeeds()
    {
        var seeds = Instantiate(ThrownSeedsPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);

    }
}
