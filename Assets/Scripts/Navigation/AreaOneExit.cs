using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaOneExit : MonoBehaviour {
    public string NextSceneName;

    // Cursed Heart
    void LeaveByChoice(HeartState heart)
    {
        heart.CurrentState = HeartState.HeartStateValues.Cursed;
        SceneManager.LoadScene(NextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var heartState = collision.gameObject.GetComponentInChildren<HeartState>();
        if(heartState)
        {
            LeaveByChoice(heartState);
        }
    }
}
