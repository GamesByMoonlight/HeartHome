﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {
    public AnimationCurve FadeInCurve;
    public float FadeInTime = 3f;
    public float Speed = 3f;
    public float KillFirstTime = 3f;

    Rigidbody2D rb;
    Animator animator;
    Flower target;
    SpriteRenderer spriteRenderer;
    Coroutine inspectFirst;
    Shiver shiver;
    bool inspecting = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shiver = GetComponent<Shiver>();
    }

    private void Start()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine(FadeIn());
    }

    void Begin()
    {
        target = Flower.OldestFlower;
        if (target == null)
            StartCoroutine(WaitForFlowers());
        else
            inspectFirst = StartCoroutine(InspectFirstFlower());

        Debug.Log("Lance - Need some Ghost music bro.  (Love, Scott)");
    }

    IEnumerator FadeIn()
    {
        float time = Time.time;
        while(Time.time - time < FadeInTime)
        {
            yield return null;
            spriteRenderer.color = new Color(1f, 1f, 1f, FadeInCurve.Evaluate((Time.time - time) / FadeInTime));
        }
        spriteRenderer.color = Color.white;
        Begin();
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

        inspectFirst = StartCoroutine(InspectFirstFlower());
    }

    IEnumerator InspectFirstFlower()
    {
        var movement = (target.transform.position - transform.position).normalized;
        SetAnimator(Vector2.SignedAngle(Vector2.up, movement));
        while(inspecting)
        {
            movement = (target.transform.position - transform.position).normalized;
            rb.velocity = movement * (Speed / 2f);
            yield return new WaitForFixedUpdate();
        }
        //StartCoroutine(ChaseFlowers());
    }

    IEnumerator KillFlowerInSeconds(Flower flower, float seconds)
    {
        float time = Time.time;
        bool activateOnceFlag = true;
        float startMagnitude = shiver.magnitude;
        while (inspecting && Time.time - time < seconds)  // This coroutine could be called simultaneously.  The first one to finish should kill all
        {
            if((Time.time - time) / seconds > 0.5f)
            {
                if (activateOnceFlag)
                {
                    activateOnceFlag = false;
                    shiver.Shivering = true;
                }
                float timeLeft = seconds / 2;
                shiver.magnitude = startMagnitude + startMagnitude * 2 *  Mathf.Abs(1f - (((Time.time - time) / seconds) / 0.5f)); // A confusing way to slowly scale up with the time that is left
            }
            yield return null;
        }
        shiver.Shivering = false;
        inspecting = false;
        flower.Kill();
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
        {
            if (inspecting)
            {
                rb.velocity = Vector2.zero;
                SetAnimator(180);
                StopCoroutine(inspectFirst);
                StartCoroutine(KillFlowerInSeconds(flower, KillFirstTime));
            }
            else
            {
                flower.Kill();
            }
        }
    }

}
