using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
    protected static bool sceneLoadInProgress = false;

    public string NextSceneName;
    public float FadeTime = 1f;

    FadeInOut Shade;
    GameObject player;

    protected void Start()
    {
        sceneLoadInProgress = false;
        player = DontDestroyPlayerOnLoad.playerObject.gameObject;
        Shade = player.GetComponentInChildren<FadeInOut>();
        if (Shade == null)
            Debug.LogError("Shade is null.  Create a Panel with an Image UI and add FadeInOut script.  Parent under Player's Canvas.");
        StartCoroutine(Shade.FadeIn(FadeTime));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // In case another exit is triggered to transfer
        if (sceneLoadInProgress)
            return;

        // Do not want to load scene twice.  For some reason, OnTriggerEnter is called twice (or many times at least) in succession 
        var playerCollision = collision.gameObject.GetComponent<PlayerAction>();
        if (playerCollision)
        {
            sceneLoadInProgress = true;
            StartCoroutine(WaitForFade());
        }
    }

    protected IEnumerator WaitForFade()
    {
        StartCoroutine(Shade.FadeOut(FadeTime));
        yield return new WaitForSeconds(FadeTime);
        SceneManager.LoadSceneAsync(NextSceneName);
    }
}
