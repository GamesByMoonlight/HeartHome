using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushSpawn : MonoBehaviour {
    public GameObject PaintbrushPrefab;

    public void PaintbrushListener()
    {
        var paintbrush = Instantiate(PaintbrushPrefab);
        RemoveOldInventoryItems();
        Inventory.Current.AddInventory(paintbrush.GetComponent<IItem>());

    }

    void RemoveOldInventoryItems()
    {
        var slots = GetComponentsInChildren<InventorySlot>();
        foreach(InventorySlot s in slots)
        {
            var hoe = s.Item.gameObject.GetComponent<Hoe>();
            var seeds = s.Item.gameObject.GetComponent<Seeds>();
            var waterCan = s.Item.gameObject.GetComponent<WateringCan>();
            if(hoe)
            {
                Inventory.Current.RemoveInventory(hoe);
            }else if (seeds)
            {
                Inventory.Current.RemoveInventory(seeds);
            }else if (waterCan)
            {
                Inventory.Current.RemoveInventory(waterCan);
            }
        }
    }

}
