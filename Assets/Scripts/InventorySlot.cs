using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {
    public Text NumberTextElement;
    public Image ImageIconElement;

    private IItem item;
    public IItem Item { get { return item; } set { SetItem(value); } }

    int inventoryNumber = -1;

	// Use this for initialization
	void Awake () {
        InitializeSlot();
	}

    void OnValidate()
    {
        InitializeSlot();
    }

    void InitializeSlot()
    {
        if (transform == null || transform.parent == null)
            return;
        
        var slots = transform.parent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; ++i)
        {
            if (slots[i] == this)
            {
                SetInventoryNumber(i + 1);
            }
        }
    }
	
	void SetInventoryNumber(int i)
    {
        inventoryNumber = i;
        NumberTextElement.text = i.ToString();
    }

    void SetItem(IItem newItem)
    {
        ImageIconElement.sprite = newItem.InventoryIcon;
        item = newItem;
        item.gameObject.transform.SetParent(transform, true);
        item.gameObject.transform.position = new Vector2(5000 + inventoryNumber * 100, 0);  // Just move it far out of the way

    }

    public void SlotSelected()
    {
        transform.parent.GetComponent<Inventory>().SelectedIventoryItem = item;
        Debug.Log(item.gameObject + " selected for use");
    }

}
