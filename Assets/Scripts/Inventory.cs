using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public static  Inventory Current {get; protected set;}

    public IItem SelectedIventoryItem;  // Set by slots onClick

    List<InventorySlot> inventory;

    void Awake() {
        if (Current != null && Current.gameObject != gameObject)
        {
            Debug.Log("There is more than one Inventory bar in the scene. Current is " + Current.gameObject.name + " and new is " + name + "Destroying this one.");
            Destroy(gameObject);
            return;
        }
        else if (Current != null)
            return;
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
        // Destroy gameobject in scene
        foreach(InventorySlot s in inventory)
        {
            if(s.Item == item)
            {
                DestroyImmediate(s.gameObject.transform.parent.gameObject);  // poorly organized, but whatever.  That's how the prefab was set up
            }
        }

        // Remove reference from list
        inventory.RemoveAll(x => x.Item == item);
        inventory.ForEach(x => x.UpdateSlotNumber());
    }

    public void CleanFarmingInventory()
    {
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<Hoe>() != null));
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<Seeds>() != null));
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<WateringCan>() != null));
    }

    void RemoveInventory(InventorySlot slot)
    {
        if(slot != null)
        {
            RemoveInventory(slot.Item);
        }
    }

}
