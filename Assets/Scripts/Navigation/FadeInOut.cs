using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {
    Image shade;
    BGMSwapper bgm;

	void Awake() {
        shade = GetComponent<Image>();
        bgm = GetComponent<BGMSwapper>();
	}
	
    public IEnumerator FadeIn(float seconds)
    {
        var start = Time.time;
        shade.color = new Color(0f, 0f, 0f, 1f);
        while((Time.time - start) / seconds < 1f)
        {
            shade.color = new Color(0f, 0f, 0f, 1f - (Time.time - start) / seconds);
            yield return null;
        }
        shade.color = new Color(0f, 0f, 0f, 0f); // just in case
    }

    public IEnumerator FadeOut(float seconds)
    {
        var start = Time.time;
        shade.color = new Color(0f, 0f, 0f, 0f);
        while ((Time.time - start) / seconds < 1f)
        {
            shade.color = new Color(0f, 0f, 0f, (Time.time - start) / seconds);
            bgm.SetTrackVolume(1 - (Time.time - start) / seconds);
            yield return null;
        }
        shade.color = new Color(0f, 0f, 0f, 1f); // just in case
    }
}
