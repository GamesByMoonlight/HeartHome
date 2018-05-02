using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour {
    public static Transform StartPoint;

    private void Awake()
    {
        StartPoint = transform;
    }
}
