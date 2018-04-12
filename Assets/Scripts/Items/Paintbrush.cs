using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintbrush : Item {
    public GameObject FlowerPrefab;

    public override void UseAt(GameObject location)
    {
        UseAt(location.transform.position);
    }

    public override void UseAt(Vector2 location)
    {
        var Flower = Instantiate(FlowerPrefab);
        Flower.transform.position = location;
    }
}
