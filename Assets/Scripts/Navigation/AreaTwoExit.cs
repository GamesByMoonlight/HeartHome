using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTwoExit : Exit {
    public static bool SupposedToTransition = false;   // A flag in case another exit is already transitioned before Frozen event is raised

    new void Start()
    {
        GameEventSystem.Instance.HeartFrozen.AddListener(LeaveByForce);
        base.Start();

        if (SupposedToTransition)
        {
            LeaveByForce();
        }
    }

    // Frozen Heart
    void LeaveByForce()
    {
        StartCoroutine(SmallDelay(2f));
    }

    IEnumerator SmallDelay(float seconds)
    {
        sceneLoadInProgress = true;
        SupposedToTransition = true;
        yield return new WaitForSeconds(seconds);
        StartCoroutine(WaitForFade());
    }

    private void OnDestroy()
    {
        // A good habit to get into.  When you add a listener.. always remove it in OnDestroy()
        if (GameEventSystem.Instance != null)
            GameEventSystem.Instance.AllFlowersDead.RemoveListener(LeaveByForce);
    }
}
