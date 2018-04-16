using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public static  Inventory Current {get; protected set;}

    public IItem SelectedIventoryItem;  // Set by slots onClick

    List<InventorySlot> inventory;

    void Awake() {
        if(Current != null)
        {
            Debug.LogError("There is more than one Inventory bar in the scene. Current is " + Current.gameObject.name + " and new is " + name);
        }
        Current = this;
        inventory = new List<InventorySlot>();
    }


    public void AddInventory(IItem item)
    {
        var slot = Instantiate(SlotPrefab, transform).GetComponentInChildren<InventorySlot>();
        slot.Item = item;
        inventory.Add(slot);
    }

    public void RemoveInventory(IItem item)
    {
        inventory.RemoveAll(x => x.Item == item);
    }

}
