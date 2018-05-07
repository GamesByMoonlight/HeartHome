using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaOneExit : Exit {

    new void Start()
    {
        
        GameEventSystem.Instance.AllFlowersDead.AddListener(LeaveByForce);
        base.Start();
    }

    // Cursed Heart
    void LeaveByChoice(HeartState heart)
    {
        heart.CurrentState = HeartState.HeartStateValues.Cursed;
        //StartCoroutine(WaitForFade());
    }

    // Broken Heart
    void LeaveByForce()
    {
        FindObjectOfType<HeartState>().CurrentState = HeartState.HeartStateValues.Broken;
        StartCoroutine(WaitForFade());
    }

    new void OnTriggerEnter2D(Collider2D collision)
    {
        var heartState = collision.gameObject.GetComponentInChildren<HeartState>();
        if(heartState)
        {
            LeaveByChoice(heartState);
            base.OnTriggerEnter2D(collision);
        }
    }

    private void OnDestroy()
    {
        // A good habit to get into.  When you add a listener.. always remove it in OnDestroy()
        if (GameEventSystem.Instance != null)
            GameEventSystem.Instance.AllFlowersDead.RemoveListener(LeaveByForce);
    }
}
