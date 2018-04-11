using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Item {

	private PlayerAction playerAction;
	public GameObject FertileSoilPrefab;

	void Awake() {
		playerAction = GameObject.Find ("Player").GetComponent<PlayerAction>();

		if (playerAction == null) {
			Debug.LogError("PlayerAction not found in scene. Hoe needs it");
		}
	

	}

	public override void UseAt (GameObject location)
	{
		var soil = Instantiate (FertileSoilPrefab);
		soil.transform.position = playerAction.DirectionAperature.transform.position;
	}

	// Use this for initialization

}
