using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool PaintingMiniGamePlayed;// { get; set; }

    GameObject player;

    private void Awake()
    {
        PaintingMiniGamePlayed = false;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = DontDestroyPlayerOnLoad.playerObject.gameObject;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = CharacterSpawn.StartPoint.position;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
