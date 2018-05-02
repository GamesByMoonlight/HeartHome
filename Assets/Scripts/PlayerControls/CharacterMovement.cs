using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    // Speed modifier for character movement
    public float Speed = 4.0f;
    public FacingDirection Direction;

    private Rigidbody2D playerRigidBody;
    private Animator playerAnim;
    private SpriteRenderer playerSpriteImage;

    // To track movement from input
    private float movePlayerHorizontal;
    private float movePlayerVertical;
    private Vector2 movement;

    // Use this for initialization
    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSpriteImage = GetComponent<SpriteRenderer>();
    }
	
    // Update is called once per frame
    void Update()
    {
        movePlayerHorizontal = Input.GetAxis("Horizontal");
        movePlayerVertical = Input.GetAxis("Vertical");
        movement = new Vector2(movePlayerHorizontal, movePlayerVertical);

        // Set Animation paramaters
        if (movePlayerVertical != 0)
        {
            playerAnim.SetBool("xMove", false);
            playerSpriteImage.flipX = false;
            Direction = movePlayerVertical > 0 ? FacingDirection.Up : FacingDirection.Down;
            playerAnim.SetInteger("yMove", movePlayerVertical > 0 ? 1 : -1);
            playerAnim.SetBool("moving", true);
        }
        else if (movePlayerHorizontal != 0)
        {
            playerAnim.SetBool("xMove", true);
            playerAnim.SetInteger("yMove", 0);
            Direction = movePlayerHorizontal > 0 ? FacingDirection.Right : FacingDirection.Left;
            playerSpriteImage.flipX = movePlayerHorizontal > 0 ? false : true;
            playerAnim.SetBool("moving", true);
        }
        else
        {
            playerAnim.SetBool("xMove", false);
            playerAnim.SetInteger("yMove", 0);
            playerAnim.SetBool("moving", false);
        }

        // Move Character
        playerRigidBody.velocity = movement * Speed;
    }

    public void MoveLeft()
    {
        
    }

    public void MoveRight()
    {

    }

    public void MoveUp()
    {

    }

    public void MoveDown()
    {

    }
}

public enum FacingDirection
{
    Up, Right, Down, Left
}
