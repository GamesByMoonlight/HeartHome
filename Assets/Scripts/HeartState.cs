using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartState : MonoBehaviour {
    private HeartMovement_Follow follow;

    public enum HeartStateValues { Happy, Cold, Cursed, Broken, Frozen };

    [SerializeField]
    private HeartStateValues currentState = HeartStateValues.Happy;
    public HeartStateValues CurrentState { get { return currentState;} set { SetState(value); }}
    public float HappyDistanceFollow = 1f;
    public float ColdDistanceFollow = 1f;

    private void Awake()
    {
        follow = GetComponent<HeartMovement_Follow>();
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
                follow.DistanceToFollow = HappyDistanceFollow;
                break;
            case HeartStateValues.Cold:
                follow.DistanceToFollow = ColdDistanceFollow;
                break;
            default:
                follow.DistanceToFollow = HappyDistanceFollow;
                break;
        }

    }
	
}
