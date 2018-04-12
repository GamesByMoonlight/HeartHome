using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Class to control the growth of the flower after planting
 * Uses Animator and Audio Source components
 */

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class FlowerGrowth : MonoBehaviour {

    private Animator animator;
    private AudioSource audioSource;

    private int growthStage = 0;

    // To allow for easier tweaking/balancing
    public float secondsPerStage = 5;

    public AudioClip[] flowerStage;

    // The animator sets the growthStage with this method.  Also will be used so that ghosts can track the growthStage of the flowers.
    public int GrowthStage
    {
        get
        {
            return growthStage;
        }

        set
        {
            if (value < 0 || value > 3)
            {
                Debug.LogWarning("Trying to set GrowthStage as " + value + ", range is 0 - 3");
            }
            else
            {
                growthStage = value;
            }
            
        }
    }
    

	void Awake () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("FlowerUpgrade");
	}
	
    // Method to make the flower grow every few seconds, set by "Seconds Per Stage".  Stops on growth stage 2.  Stage 3 is a dead flower, set by a ghost attacking the flower.
    private IEnumerator FlowerUpgrade()
    {
        yield return new WaitForSeconds(secondsPerStage);
        if (growthStage < 2)
        {
            growthStage++;
            animator.SetTrigger("GrowOneStage");
            StartCoroutine("FlowerUpgrade");
        }
    }

    // Method to let the animator play the audio clip based on growth stage
    public void PlayAudioClip()
    {
        audioSource.clip = flowerStage[growthStage];
        audioSource.Play();
    }

}
