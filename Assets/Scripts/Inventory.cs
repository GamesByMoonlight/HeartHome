using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Inventory : MonoBehaviour {
    public GameObject SlotPrefab;
    public static  Inventory Current {get; protected set;}

    public IItem SelectedIventoryItem;  // Set by slots onClick

    List<InventorySlot> inventory;
    bool cleanedArea2 = false;

    void Awake() {
        if (Current != null && Current.gameObject != gameObject)
        {
            //Debug.Log("Another Inventory in scene.  Deleting this one.");
            Destroy(gameObject);
            return;
        }
        else if (Current != null)
            return;
        Current = this;
        inventory = new List<InventorySlot>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Clear inventory the first time we enter Area 2
        if(scene.name == "Area 2" && !cleanedArea2)
        {
            cleanedArea2 = true;
            ClearInventory();
        }
        if(scene.name == "Area 3")
        {
            ClearInventory();
        }
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
        Debug.Log("Cleaning farm inventory");
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<Hoe>() != null));
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<Seeds>() != null));
        RemoveInventory(inventory.Find(x => x.Item.gameObject.GetComponent<WateringCan>() != null));
        SelectedIventoryItem = null;
    }

    void RemoveInventory(InventorySlot slot)
    {
        if(slot != null)
        {
            RemoveInventory(slot.Item);
        }
    }

    void ClearInventory()
    {
        // Destroy everything
        foreach (InventorySlot s in inventory)
        {
            DestroyImmediate(s.gameObject.transform.parent.gameObject);  
        }
        inventory.Clear();
        SelectedIventoryItem = null;
    }

    private void OnDisable()
    {
        // A good habit to get into.  Putting this on disable instead of ondestroy bc that's how the documentation shows it
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
