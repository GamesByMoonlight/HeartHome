using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool PaintingMiniGamePlayed;// { get; set; }

    private void Awake()
    {
        PaintingMiniGamePlayed = false;
    }

    //private void Start()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}


    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{

    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

}
