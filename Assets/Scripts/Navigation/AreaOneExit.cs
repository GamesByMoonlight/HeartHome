using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaOneExit : MonoBehaviour {
    public string NextSceneName;
    public float FadeTime = 2f;
    public FadeInOut Shade;

    private void Start()
    {
        if (Shade == null)
            Debug.LogError("Shade is null.  Create a Panel with an Image UI and a FadeInOut script and reference it here for fading effect");

        GameEventSystem.Instance.AllFlowersDead.AddListener(LeaveByForce);
        StartCoroutine(Shade.FadeIn(FadeTime));
    }

    // Cursed Heart
    void LeaveByChoice(HeartState heart)
    {
        heart.CurrentState = HeartState.HeartStateValues.Cursed;
        StartCoroutine(WaitForFade());
    }

    // Broken Heart
    void LeaveByForce()
    {
        FindObjectOfType<HeartState>().CurrentState = HeartState.HeartStateValues.Broken;
        StartCoroutine(WaitForFade());
    }

    IEnumerator WaitForFade()
    {
        StartCoroutine(Shade.FadeOut(FadeTime));
        yield return new WaitForSeconds(FadeTime);
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

    private void OnDestroy()
    {
        // A good habit to get into.  When you add a listener.. always remove it in OnDestroy()
        if (GameEventSystem.Instance != null)
            GameEventSystem.Instance.AllFlowersDead.RemoveListener(LeaveByForce);
    }
}
