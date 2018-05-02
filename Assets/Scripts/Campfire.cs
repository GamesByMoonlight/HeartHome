using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour {
    public Animator CampfireAnim;

    bool entered = false;
    bool started = false;
    Coroutine checking;

    private void Start()
    {
        StartCoroutine(CheckForBlaze());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        entered = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entered = false;

    }

    IEnumerator CheckForBlaze()
    {
        while(!started)
        {
            if (entered && Input.GetButtonDown("Action"))
            {
                CampfireAnim.SetTrigger("Burn");
                started = true;
                DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<HeartState>().CurrentState = HeartState.HeartStateValues.Happy;
            }
            yield return null;
        }

    }
}
