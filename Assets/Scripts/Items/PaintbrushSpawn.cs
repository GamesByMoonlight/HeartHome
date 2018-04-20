using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintbrushSpawn : MonoBehaviour {
    public GameObject PaintbrushPrefab;

    public void PaintbrushListener()
    {
        var paintbrush = Instantiate(PaintbrushPrefab);
        Inventory.Current.CleanFarmingInventory();
        Inventory.Current.AddInventory(paintbrush.GetComponent<IItem>());

    }

}
