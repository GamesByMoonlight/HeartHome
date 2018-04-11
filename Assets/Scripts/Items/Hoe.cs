using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Item {

	public GameObject FertileSoilPrefab;

    PlayerAction playerAction;
    Animator animator;

	void Awake() {
		playerAction = GameObject.Find ("Player").GetComponent<PlayerAction>();
        animator = GetComponent<Animator>();

		if (playerAction == null) {
			Debug.LogError("PlayerAction not found in scene. Hoe needs it");
		}
	

	}

	public override void UseAt (GameObject location)
	{
		var soil = Instantiate (FertileSoilPrefab);
		soil.transform.position = playerAction.DirectionAperature.transform.position;
        PlayAnimation();
	}

	void PlayAnimation()
    {
        string direction = playerAction.DirectionAperature.name;
        Vector2 start = transform.position;
        transform.position = playerAction.transform.position;

        if(direction.Contains("Left")) // Sloppy, but whatever
        {
            animator.SetTrigger("SlashLeft");
            StartCoroutine(ReturnToStart(start));
        }
        else if (direction.Contains("Right")) // Sloppy, but whatever
        {
            animator.SetTrigger("SlashRight");
            StartCoroutine(ReturnToStart(start));
        }

    }

    IEnumerator ReturnToStart(Vector2 start)
    {
        yield return new WaitForSeconds(.3f);
        transform.position = start;
    }

}
