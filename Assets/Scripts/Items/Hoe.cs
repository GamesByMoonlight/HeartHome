using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Item {

	public GameObject FertileSoilPrefab;

    PlayerAction playerAction;
    Animator animator;
    SpriteRenderer spriteRenderer;

	void Awake() {
		playerAction = GameObject.Find ("Player").GetComponent<PlayerAction>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        // Record where this Hoe is currently stored (should be off screen somewhere)
        Vector2 start = transform.position;
        int renderOrder = spriteRenderer.sortingOrder;

        transform.position = playerAction.ItemUseAperature.transform.position;
        spriteRenderer.sortingOrder = 3; // Move in front of player

        // Determine which animation to play, then play it
        string direction = playerAction.DirectionAperature.name;
        if(direction.Contains("Left")) // Sloppy, but whatever
        {
            animator.SetTrigger("SlashLeft");
        }
        else if (direction.Contains("Right")) // Sloppy, but whatever
        {
            animator.SetTrigger("SlashRight");
        }

        // Return the Hoe back to the start once the animation is finished
        StartCoroutine(ReturnToStart(start, renderOrder));

    }

    IEnumerator ReturnToStart(Vector2 start, int renderOrder)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = start;
        spriteRenderer.sortingOrder = renderOrder;
    }

}
