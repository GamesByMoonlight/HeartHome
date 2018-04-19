using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMaker : MonoBehaviour {
    public GameObject[] FlowerPrefabs;

    public void MakeFlowerAt(Vector3 position)
    {
        int color = Random.Range(0, FlowerPrefabs.Length);
        Instantiate(FlowerPrefabs[color], position, Quaternion.Euler(0f, 0f, 0f));
    }
}
