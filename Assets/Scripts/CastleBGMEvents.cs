using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CastleBGMEvents : MonoBehaviour {

    public static CastleBGMEvents Instance;

    void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    [System.Serializable]
    public class BGMEvent : UnityEvent { }

    public BGMEvent TestEvent;


}
