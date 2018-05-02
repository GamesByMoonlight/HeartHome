using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    public string NextSceneName;
    public float FadeTime = 1f;

    FadeInOut Shade;
    GameObject player;

    protected void Start()
    {
        player = DontDestroyPlayerOnLoad.playerObject.gameObject;
        Shade = player.GetComponentInChildren<FadeInOut>();
        if (Shade == null)
            Debug.LogError("Shade is null.  Create a Panel with an Image UI and add FadeInOut script.  Parent under Player's Canvas.");
        StartCoroutine(Shade.FadeIn(FadeTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitForFade());
    }

    protected IEnumerator WaitForFade()
    {
        StartCoroutine(Shade.FadeOut(FadeTime));
        yield return new WaitForSeconds(FadeTime);
        SceneManager.LoadSceneAsync(NextSceneName);
    }
}
