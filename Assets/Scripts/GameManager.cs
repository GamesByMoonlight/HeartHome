using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    Dictionary<string, Vector2> ReturnPoints = new Dictionary<string, Vector2>();

    public bool PaintingMiniGamePlayed;

    GameObject player;

    public void ReturnToThisPoint(Transform returnHere)
    {
        var sceneName = SceneManager.GetActiveScene().name;

        // OnTriggerEnter is typically called twice (for some reason), this prevents multiple additions
        if (!ReturnPoints.ContainsKey(sceneName))
        {                  
            ReturnPoints.Add(SceneManager.GetActiveScene().name, returnHere.position);
        }
    }

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
        Vector2 spawn;
        if(ReturnPoints.TryGetValue(scene.name, out spawn))
        {
            player.transform.position = spawn;
            ReturnPoints.Remove(scene.name);
            return;
        }
        player.transform.position = CharacterSpawn.StartPoint.position;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
