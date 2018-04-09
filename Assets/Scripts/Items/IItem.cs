using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem {
    GameObject gameObject { get; }  // This is defined by MonoBehaviour class, so you shouldn't have to explicitly implement in your item class
    Sprite InventoryIcon { get; }   // The sprite that appears in the inventory panel when item is picked up
    void UseAt(GameObject location);// passing Gameobject instead of Transform to give more access if needed.  This is the in game location an item should be used
    void UseAt(Vector2 location);
}
