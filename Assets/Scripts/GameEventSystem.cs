using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventSystem : MonoBehaviour {
    public static GameEventSystem Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Another instance of GameEventSystem was hanging around when this one was created");
        }
        Instance = this;
    }

    [System.Serializable]
    public class GameEvent : UnityEvent { }                     // An Event that does not take an arguments
    [System.Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }   // Takes GameObject argument

    public GameEvent MakeGhost;
    public GameEvent MakePaintbrush;
    public GameEvent InventorySlotRemoved;
    public GameEvent AllFlowersDead;
    public GameEvent ToolsChanged;
}
