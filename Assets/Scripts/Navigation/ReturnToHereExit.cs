using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToHereExit : Exit {
    public Transform ReturnHere;

    new void OnTriggerEnter2D(Collider2D collision)
    {
        DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<GameManager>().ReturnToThisPoint(ReturnHere);
        base.OnTriggerEnter2D(collision);
    }
}
