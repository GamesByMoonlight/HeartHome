using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour {

    public IsCollidingWithPlayer AvoidObstacle;
    public Transform PlayerEndingPosition;
    public CircleCollider2D DeleteThisCollider;
    public float ReduceSpeedFactor = .2f;
    public float SecondsToPanUp = 5f;
    public float DegreesToPanUp = -12f;
    public float UnitsToRise = .6f;

    CharacterMovement player;

    private void Start()
    {
        player = DontDestroyPlayerOnLoad.playerObject.GetComponent<CharacterMovement>();
        GameEventSystem.Instance.GameEnded.AddListener(GameEndedListener);
    }

    void GameEndedListener()
    {
        player.AutomatedMovement = true;
        Destroy(DeleteThisCollider);
        StartCoroutine(MovePlayerToFinalPoint());
    }

    IEnumerator MovePlayerToFinalPoint()
    {
        var direction = PlayerEndingPosition.position - player.transform.position;
        while(Vector2.Distance(PlayerEndingPosition.position, player.transform.position) > .03f)
        {
            if (AvoidObstacle.Collided)
                WalkAroundObstacle(direction);
            else
                WalkDirectlyToPoint(direction);
            
            yield return new WaitForFixedUpdate();
            direction = PlayerEndingPosition.position - player.transform.position;
        }

        Debug.Log("Done");
        player.AutoPilot(0f, .01f);   // Face up
        yield return null;
        player.AutoPilot(0f, 0f);   // Stop

        yield return new WaitForSeconds(.5f);
        StartCoroutine(LookAtStars());
    }

    private IEnumerator LookAtStars()
    {
        var time = Time.time;
        var delta = 0f;
        Vector3 start = Camera.main.transform.position;
        while(delta < SecondsToPanUp)
        {
            var percent = (delta / SecondsToPanUp);
            Camera.main.transform.rotation = Quaternion.Euler(percent * DegreesToPanUp, 0f, 0f);//Rotate(new Vector3(Time.deltaTime * DegreesToPanUp, 0f, 0f));
            Camera.main.transform.position = start + new Vector3(0f, percent * UnitsToRise, 0f);
            yield return null;
            delta = Time.time - time;
        }
    }

    private void WalkDirectlyToPoint(Vector3 direction)
    {
        direction = direction.normalized * ReduceSpeedFactor;
        player.AutoPilot(direction.x, direction.y);
    }

    private void WalkAroundObstacle(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0f, 0f);
        direction = direction.normalized * ReduceSpeedFactor;
        player.AutoPilot(direction.x, 0f);
    }
}
