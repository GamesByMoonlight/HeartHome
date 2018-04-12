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
        else
            StartCoroutine(ChaseFlowers());
    }

    IEnumerator WaitForFlowers()
    {
        while (target == null)
        {
            yield return new WaitForSeconds(.3f);
            target = Flower.OldestFlower;
        }

        StartCoroutine(ChaseFlowers());
    }

    // Update is called once per frame
    IEnumerator ChaseFlowers() {
        while (true)
        {
            target = UpdateTargetFlower(target);
            var movement = (target.transform.position - transform.position).normalized;

            SetAnimator(Vector2.SignedAngle(Vector2.up, movement));
            rb.velocity = movement * Speed;

            yield return new WaitForFixedUpdate();
        }
	}

    Flower UpdateTargetFlower(Flower currentTarget)
    {
        if (currentTarget.Alive)
            return currentTarget;

        return currentTarget.Next ? currentTarget.Next : currentTarget;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var flower = collision.gameObject.GetComponent<Flower>();
        if (flower && flower.Alive)
            flower.Kill();
    }

}
