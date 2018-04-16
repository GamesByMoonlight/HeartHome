using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Item {

	public GameObject FertileSoilPrefab;

    PlayerAction playerAction;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

	void Awake() {
		playerAction = GameObject.Find ("Player").GetComponent<PlayerAction>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

		if (playerAction == null) {
			Debug.LogError("PlayerAction not found in scene. Hoe needs it");
		}
	

	}

	public override void UseAt (GameObject location)
	{
		var soil = Instantiate (FertileSoilPrefab);
		soil.transform.position = playerAction.DirectionAperature.transform.position;
        audioSource.Play();
        PlayAnimation();
        
	}

	void PlayAnimation()
    {
        // Record where this Hoe is currently stored (should be off screen somewhere)
        Vector2 start = transform.position;
        int renderOrder = spriteRenderer.sortingOrder;
        transform.position = playerAction.ItemUseAperature.transform.position;
        spriteRenderer.sortingOrder = 3; // Move in front of player


        string trigger = GetTrigger(playerAction.DirectionAperature.name);
        if (trigger.Contains("Up"))
            spriteRenderer.sortingOrder = renderOrder;
        animator.SetTrigger(trigger);


        // Return the Hoe back to the start once the animation is finished
        StartCoroutine(ReturnToStart(start, renderOrder));

    }

    string GetTrigger(string direction)
    {
        string trigger = "";
        if (direction.Contains("Left")) // Sloppy, but whatever.  This logic requires the Aperatures in Player prefab to be named properly
        {
            trigger = "SlashLeft";
        }
        else if (direction.Contains("Right")) 
        {
            trigger = "SlashRight";
        }
        else if(direction.Contains("Down"))
        {
            trigger = "SlashDown";
        }
        else if(direction.Contains("Up"))
        {
            trigger = "SlashUp";
        }
        else{
            Debug.LogWarning("Could not determine which way Character is facing.  Animation trigger may not be set properly.");
        }

        return trigger;
    }

    IEnumerator ReturnToStart(Vector2 start, int renderOrder)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        transform.position = start;
        spriteRenderer.sortingOrder = renderOrder;
    }

}
