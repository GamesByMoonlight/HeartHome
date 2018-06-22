using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupFadeController : MonoBehaviour {
    public FadeInOut Shade;
    public CanvasGroup canvasGroup;
    public Canvas StartScreenCanvas;
    public Canvas OptionsScreenCanvas;
    public float FadeInTime = 1f;
    public float FadeOutTime = 1f;
    public string NextSceneName = "Area 1";

    bool startGame = false;

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(StartHelper());
    }
    
    public void StartGame()
    {
        StartCoroutine(FadeToGame());
    }

    public void OptionsScreen()
    {
        StartScreenCanvas.enabled = false;
        OptionsScreenCanvas.enabled = true;
    }

    public void BackToStart()
    {
        StartScreenCanvas.enabled = true;
        OptionsScreenCanvas.enabled = false;
    }

    public void EndGame()
    {
        Application.Quit();  // This method is not deployable to all platforms (potential aproblems: Android, iPhone, webGL)
    }

    IEnumerator StartHelper()
    {
        canvasGroup.alpha = 0f;
        StartCoroutine(Shade.FadeInWhite(FadeInTime));
        yield return new WaitForSeconds(FadeInTime);
        StartCoroutine(FadeInInstructions(FadeInTime));
    }

    IEnumerator FadeInInstructions(float time)
    {
        var startTime = Time.time;

        List<Button> buttons = new List<Button>();
        List<Text> texts = new List<Text>();

        GetComponentsInParent(true, buttons);
        GetComponentsInParent(true, texts);

        while (Time.time - startTime < time)
        {
            canvasGroup.alpha = (Time.time - startTime) / time;

            foreach (Button eachButton in buttons)
            {
                Color buttonColor = eachButton.colors.normalColor;
                buttonColor.a = (Time.time - startTime)/time;
            }

            foreach (Text eachText in texts)
            {
                Color textColor = eachText.color;
                textColor.a = (Time.time - startTime) / time;
            }

            yield return null;
        }

        canvasGroup.alpha = 1f;
        
    }

    IEnumerator FadeToGame()
    {
        StartCoroutine(Shade.FadeOut(FadeOutTime));
        yield return new WaitForSeconds(FadeOutTime);
        SceneManager.LoadSceneAsync(NextSceneName);
    }
      
}
