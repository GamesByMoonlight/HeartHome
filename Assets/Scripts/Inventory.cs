using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public static  Inventory Current {get; protected set;}

    public IItem SelectedIventoryItem;  // Set by slots onClick

    void Start() {
        Current = this;
    }

    private void Update()
    {
    }

    public void AddInventory(IItem item)
    {
        var slot = Instantiate(SlotPrefab, transform).GetComponentInChildren<InventorySlot>();
        slot.Item = item;
    }
}
