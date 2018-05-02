using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupFadeController : MonoBehaviour {
    public FadeInOut Shade;
    public CanvasGroup canvasGroup;
    public float FadeInTime = 1f;
    public float FadeOutTime = 1f;
    public string NextSceneName = "Area 1";

    bool startGame = false;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(StartHelper());
    }

    IEnumerator StartHelper()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(Shade.FadeInWhite(FadeInTime));
        yield return new WaitForSeconds(FadeInTime);
        StartCoroutine(FadeInInstructions(FadeInTime));
        StartCoroutine(WaitForAnyKey());

    }

    IEnumerator FadeInInstructions(float time)
    {
        var startTime = Time.time;
        while(Time.time - startTime < time)
        {
            canvasGroup.alpha = (Time.time - startTime) / time;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }

    IEnumerator WaitForAnyKey()
    {
        while (!startGame)
        {
            if (Input.anyKey)
            {
                startGame = true;
            }
            yield return null;
        }

        StartCoroutine(Shade.FadeOut(FadeOutTime));
        yield return new WaitForSeconds(FadeOutTime);
        SceneManager.LoadSceneAsync(NextSceneName);
    }

}
