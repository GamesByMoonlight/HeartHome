using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceAreaTrigger : MonoBehaviour {
    public Fireplace TheFireplace;

    HeartState heart;

    private void Start()
    {
        heart = DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<HeartState>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!TheFireplace.stuffBurning)
        {
            heart.CurrentState = HeartState.HeartStateValues.Cold;
        }
        else{
            heart.CurrentState = HeartState.HeartStateValues.Happy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        heart.CurrentState = HeartState.HeartStateValues.Cold;
    }
}
