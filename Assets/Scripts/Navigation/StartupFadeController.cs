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
    public Button[] buttons;
    public Text[] texts;
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

        /*
        transform.parent.GetComponentsInChildren(true, buttons);
        transform.parent.GetComponentsInChildren(true, texts);
        */
        
        while (Time.time - startTime < time)
        { 
            canvasGroup.alpha = (Time.time - startTime) / time;

            foreach (Button eachButton in buttons)
            {
                ColorBlock cb = eachButton.colors;
                Color thisColor = cb.normalColor;
                
                thisColor.a = (Time.time - startTime) / time;
                cb.normalColor = thisColor;

                eachButton.colors = cb;
            }

            foreach (Text eachText in texts)
            {
                Color textColor = eachText.color;
                textColor.a = (Time.time - startTime) / time;
                eachText.color = textColor;
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
