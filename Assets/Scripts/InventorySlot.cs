using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {
    private static InventorySlot SelectedSlot;

    public Image ImageIconElement;
    public Sprite SelectedSprite;
    public Text NumberTextElement;

    private IItem item;
    public IItem Item { get { return item; } set { SetItem(value); } }

    int inventoryNumber = -1;
    Toggle toggle;

	// Use this for initialization
	void Awake () {
        InitializeSlot();
        toggle = GetComponent<Toggle>();

        if(inventoryNumber > 9)
        {
            Debug.LogError("Can not have more than 9 items in inventory.  Should this ever happen in our game?  Destroying slot and item");
            Destroy(gameObject);
        }
	}

    private void Start()
    {
        GetComponent<Toggle>().group = Inventory.Current.GetComponent<ToggleGroup>();
    }

    private void OnValidate()
    {
        InitializeSlot();
    }

    void InitializeSlot()
    {
        if (transform == null || transform.parent == null || transform.parent.parent == null)
            return;

        var slots = transform.parent.parent.GetComponentsInChildren<InventorySlot>();

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
        if (NumberTextElement != null)
            NumberTextElement.text = i.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(inventoryNumber.ToString()))
        {
            toggle.isOn = true;
        }
    }

    void SetItem(IItem newItem)
    {
        if (ImageIconElement.sprite != null)
            Debug.LogError("This slot already has an iventory item.  You should delete this slot entirely and make a new slot instead.");
        
        ImageIconElement.sprite = newItem.InventoryIcon;
        item = newItem;
        item.gameObject.transform.SetParent(transform.parent, true);
        item.gameObject.transform.position = new Vector2(5000 + inventoryNumber * 100, 0);  // Just move it far out of the way

    }

    public void SlotToggled(bool isOn)
    {
        if (isOn)
        {
            Inventory.Current.SelectedIventoryItem = item;
        }
    }

}
