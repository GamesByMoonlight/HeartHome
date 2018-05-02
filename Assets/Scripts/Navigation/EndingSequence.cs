using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingSequence : MonoBehaviour {

    public IsCollidingWithPlayer AvoidObstacle;
    public Transform PlayerEndingPosition;

    GameObject player;
    Rigidbody2D rb;

    private void Start()
    {
        player = DontDestroyPlayerOnLoad.playerObject.gameObject;
        rb = player.GetComponent<Rigidbody2D>();
        GameEventSystem.Instance.GameEnded.AddListener(GameEndedListener);
    }

    void GameEndedListener()
    {
        Destroy(player.GetComponent<CharacterMovement>());
        StartCoroutine(MovePlayerToFinalPoint());
    }

    IEnumerator MovePlayerToFinalPoint()
    {
        while((player.transform.position - PlayerEndingPosition.position).magnitude > .1f)
        {
            
            yield return new WaitForFixedUpdate();
        }
    }
}
