using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartState : MonoBehaviour {
    public enum HeartStateValues { Happy, Cold, Cursed, Broken, Frozen };
    private HeartMovement_Follow follow;
    [SerializeField]
    private HeartStateValues currentState = HeartStateValues.Happy;
    public HeartStateValues CurrentState { get { return currentState;} set { SetState(value); }}
    public float HappyDistanceFollow = 1f;
    public float DamagedDistanceFollow = 2f;
    public float HappyToPlayerSpeed = 12f;
    public float HappyToToolSpeed = 16f;
    public float DamagedToPlayerSpeed = 6f;
    public float DamagedToToolSpeed = 8f;
    public float HappyRotateSpeed = 4f;
    public float ColdRotateSpeed = 2f;
    public Animator spriteAnimator;
    public Animator floatAnimator;

    private void Awake()
    {
        follow = GetComponent<HeartMovement_Follow>();
    }

    private void Start()
    {
        SetState(currentState);
    }

    // Called when changes in editor occur (allows us to debug and set Heart state manually in editor and observe behavior change)
    private void OnValidate()
    {
        follow = GetComponent<HeartMovement_Follow>();
        SetState(currentState);
    }

    void SetState(HeartStateValues state)
    {
        switch(state)
        {
            case HeartStateValues.Happy:
                follow.DistanceFollowed = HappyDistanceFollow;
                follow.MoveToPlayerSpeed = HappyToPlayerSpeed;
                follow.MoveToToolSpeed = HappyToToolSpeed;
                follow.RotateSpeed = HappyRotateSpeed;
                if (Application.isPlaying)
                {
                    spriteAnimator.SetTrigger("Happy");
                    floatAnimator.SetBool("Float", true);
                }
                break;
            case HeartStateValues.Cold:
                follow.DistanceFollowed = DamagedDistanceFollow;
                follow.MoveToPlayerSpeed = DamagedToPlayerSpeed;
                follow.MoveToToolSpeed = DamagedToToolSpeed;
                follow.RotateSpeed = ColdRotateSpeed;
                if (Application.isPlaying)
                {
                    floatAnimator.SetBool("Float", true);
                }
                break;
            case HeartStateValues.Frozen:
                follow.DistanceFollowed = DamagedDistanceFollow;
                follow.MoveToPlayerSpeed = DamagedToPlayerSpeed;
                follow.MoveToToolSpeed = DamagedToToolSpeed;
                follow.RotateSpeed = ColdRotateSpeed;
                if (Application.isPlaying)
                {
                    spriteAnimator.SetTrigger("Frozen");
                    floatAnimator.SetBool("Float", false);
                }
                break;
            case HeartStateValues.Broken:
                follow.DistanceFollowed = DamagedDistanceFollow;
                follow.MoveToPlayerSpeed = DamagedToPlayerSpeed;
                follow.MoveToToolSpeed = HappyToToolSpeed;
                follow.RotateSpeed = ColdRotateSpeed;
                if (Application.isPlaying)
                {
                    spriteAnimator.SetTrigger("Broken");
                    floatAnimator.SetBool("Float", true);
                }
                break;
            case HeartStateValues.Cursed:
                follow.DistanceFollowed = DamagedDistanceFollow;
                follow.MoveToPlayerSpeed = DamagedToPlayerSpeed;
                follow.MoveToToolSpeed = HappyToToolSpeed;
                follow.RotateSpeed = ColdRotateSpeed;
                if (Application.isPlaying)
                {
                    spriteAnimator.SetTrigger("Cursed");
                    floatAnimator.SetBool("Float", true);
                }
                break;
            default:
                follow.DistanceFollowed = HappyDistanceFollow;
                spriteAnimator.SetTrigger("Happy");
                floatAnimator.SetBool("Float", true);
                break;
        }
    }
	
}
