using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : MonoBehaviour {
    public GameObject GhostPrefab;

    public void GhostEvent()
    {
        Instantiate(GhostPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Destroy(gameObject);  // Destroy self

    }

    // Could do this programatically, but instead I just assigned the GhostEvent() method to the GameEvent Object in editor
    //private void Awake()
    //{
    //    GameEventSystem.Instance.MakeGhost.AddListener(GhostEvent);
    //}

    //private void OnDestroy()
    //{
    //    if(GameEventSystem.Instance != null)
    //    {
    //        GameEventSystem.Instance.MakeGhost.RemoveListener(GhostEvent);
    //    }
    //}
}
