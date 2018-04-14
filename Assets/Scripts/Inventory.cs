using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public static  Inventory Current {get; protected set;}

    public IItem SelectedIventoryItem;  // Set by slots onClick

    void Awake() {
        if(Current != null)
        {
            Debug.LogError("There is more than one Inventory bar in the scene. Current is " + Current.gameObject.name + " and new is " + name);
        }
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

    public void RemoveInventory(IItem item)
    {

        //DestroyObject(item);
        Debug.Log("Scott pretty please remove burned objects");

        throw new System.NotImplementedException();

    }


}
