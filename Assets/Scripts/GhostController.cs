using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
    public float Speed = 5f;

    Rigidbody2D rb;
    Animator animator;
    Flower target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        target = Flower.OldestFlower;
        if (target == null)
            StartCoroutine(WaitForFlowers());
    }

    // Update is called once per frame
    void FixedUpdate () {
        //var horizontal = Input.GetAxis("Horizontal");
        //var  vertical = Input.GetAxis("Vertical");
        //var movement = new Vector2(horizontal, vertical);

        //Debug.Log(Vector2.SignedAngle(Vector2.up, movement));



        //var angle = Vector2.SignedAngle(Vector2.up, movement);
        //SetAnimator(angle);

        //rb.velocity = movement * Speed;
	}

    Flower UpdateTargetFlower(Flower currentTarget)
    {
        if (currentTarget.Alive)
            return currentTarget;

        return currentTarget.Next;
    }

    void SetAnimator(float angle)
    {
        if (Mathf.Abs(angle) < 45f)
        {
            animator.SetInteger("Vertical", 1);
            animator.SetInteger("Horizontal", 0);
        }
        else if (Mathf.Abs(angle) > 135f)
        {
            animator.SetInteger("Vertical", -1);
            animator.SetInteger("Horizontal", 0);
        }
        else if (angle >= 45f && angle <= 135f)
        {
            animator.SetInteger("Vertical", 0);
            animator.SetInteger("Horizontal", -1);
        }
        else if (angle <= -45f && angle >= -135f)
        {
            animator.SetInteger("Vertical", 0);
            animator.SetInteger("Horizontal", 1);
        }
    }

    IEnumerator WaitForFlowers()
    {
        while (target == null)
        {
            yield return new WaitForSeconds(.3f);
            target = Flower.OldestFlower;
        }
    }
}
