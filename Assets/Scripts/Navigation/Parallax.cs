using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
    public float speed = 0.1f; // Higher the number, closer the background, faster the scrolling

    Camera mc;
    Vector3 old;

    private void Awake()
    {
        mc = Camera.main;
    }

    private void Start()
    {
        old = mc.transform.position;
    }

    private void FixedUpdate()
    {
        Vector2 diff = (old - mc.transform.position) * speed;// * Time.deltaTime;
        old = mc.transform.position;

        transform.position = ((Vector2)transform.position + diff);
    }
}
