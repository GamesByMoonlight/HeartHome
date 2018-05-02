using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCollidingWithPlayer : MonoBehaviour {

    public bool Collided { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collided = false;
    }
}
