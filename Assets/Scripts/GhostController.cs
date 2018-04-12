using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
    public float Speed = 5f;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        var horizontal = Input.GetAxis("Horizontal");
        var  vertical = Input.GetAxis("Vertical");
        var movement = new Vector2(horizontal, vertical);

        //Debug.Log(Vector2.SignedAngle(Vector2.up, movement));


        rb.velocity = movement * Speed;
	}
}
