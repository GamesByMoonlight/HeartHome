using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour {

    public IsCollidingWithPlayer AvoidObstacle;
    public Transform PlayerEndingPosition;
    public float ReduceSpeedFactor = .2f;

    CharacterMovement player;

    private void Start()
    {
        player = DontDestroyPlayerOnLoad.playerObject.GetComponent<CharacterMovement>();
        GameEventSystem.Instance.GameEnded.AddListener(GameEndedListener);
    }

    void GameEndedListener()
    {
        player.AutomatedMovement = true;
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
    }

    private void WalkDirectlyToPoint(Vector3 direction)
    {
        direction = direction.normalized * ReduceSpeedFactor;
        player.AutoPilot(direction.x, direction.y);
    }

    private void WalkAroundObstacle(Vector3 direction)
    {
        var obstacleDir = AvoidObstacle.transform.position - player.transform.position;

        if (direction.magnitude < obstacleDir.magnitude)
            WalkDirectlyToPoint(direction);

        // If the magnitude grows and obstacle closer than target (i.e if the obstacle is in front of the player relative to the final point)
        if(Mathf.Abs(obstacleDir.x + direction.x) > Mathf.Abs(direction.x) && Mathf.Abs(direction.x) > Mathf.Abs(obstacleDir.x))
        {
            var angle = Vector2.Angle(Vector2.up, obstacleDir);
            if (angle > 45 && angle < 135)
            {
                Debug.Log("In here");
                direction = new Vector3(0f, 1f, 0f);
                direction = direction.normalized * ReduceSpeedFactor;
                player.AutoPilot(0f, direction.y);
                return;
            }
        }

        direction = new Vector3(direction.x, 0f, 0f);
        direction = direction.normalized * ReduceSpeedFactor;
        player.AutoPilot(direction.x, 0f);
    }
}
