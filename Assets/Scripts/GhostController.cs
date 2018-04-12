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

        Debug.Log("Lance - Need some Ghost music bro.  (Love, Scott)");
    }

    IEnumerator WaitForFlowers()
    {
        // First while loop is generally only applicable if there's a Ghost before any flowers are present in scene
        while (target == null)
        {
            yield return new WaitForSeconds(.3f);
            target = Flower.OldestFlower;
        }

        // This one will only iterate through until a new Flower (not dead) is created.  Applicable while only dead flowers in scene
        SetAnimator(180f); // Down Animation
        while(!target.Alive)
        {
            target = target.Next ? target.Next : target;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ChaseFlowers());
    }

    // Update is called once per frame
    IEnumerator ChaseFlowers() {
        while (target)
        {
            var movement = (target.transform.position - transform.position).normalized;

            SetAnimator(Vector2.SignedAngle(Vector2.up, movement));
            rb.velocity = movement * Speed;

            yield return new WaitForFixedUpdate();
            target = UpdateTargetFlower(target);
        }

        rb.velocity = new Vector2(0f, 0f);
        StartCoroutine(WaitForFlowers());
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var flower = collision.gameObject.GetComponent<Flower>();
        if (flower && flower.Alive)
            flower.Kill();
    }

}
