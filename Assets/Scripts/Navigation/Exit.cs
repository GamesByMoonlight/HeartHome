using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

    public string NextSceneName;
    public float FadeTime = 1f;
    public Vector2 NextStartLocation;
    public FadeInOut Shade;

    GameObject player;

    protected void Start()
    {
        if (Shade == null)
            Debug.LogError("Shade is null.  Create a Panel with an Image UI and a FadeInOut script and reference it here for fading effect");
        player = DontDestroyPlayerOnLoad.playerObject.gameObject;
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
        player.transform.position = NextStartLocation;
        SceneManager.LoadSceneAsync(NextSceneName);
    }
}
