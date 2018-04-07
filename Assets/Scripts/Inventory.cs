using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public GameObject ExamplePrefab;
    public IItem SelectedIventoryItem;  // Set by slots onClick

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            var go = Instantiate(ExamplePrefab);
            AddInventory(go.GetComponent<IItem>());
        }
    }

    void AddInventory(IItem item)
    {
        var slot = Instantiate(SlotPrefab, transform).GetComponent<InventorySlot>();
        slot.Item = item;
    }
}
