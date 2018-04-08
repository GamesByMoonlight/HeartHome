using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour {
    private static InventorySlot SelectedSlot;

    public Text NumberTextElement;
    public Image ImageIconElement;
    public Sprite SelectedSprite;

    private IItem item;
    public IItem Item { get { return item; } set { SetItem(value); } }

    Sprite NormalSprite;
    int inventoryNumber = -1;
    Inventory inventory;
    Button button;
    Image buttonImage;

	// Use this for initialization
	void Awake () {
        InitializeSlot();
        inventory = transform.parent.GetComponent<Inventory>();
        button = GetComponentInChildren<Button>();
        buttonImage = button.GetComponent<Image>();
        NormalSprite = buttonImage.sprite;

        if(inventoryNumber > 9)
        {
            Debug.LogError("Can not have more than 9 items in inventory.  Should this ever happen in our game?  Destroying slot and item");
            Destroy(gameObject);
        }

	}

    void OnValidate()
    {
        InitializeSlot();
    }

    private void Update()
    {
        if(Input.GetKeyDown(inventoryNumber.ToString()))
        {
            SlotSelected();
        }
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
        button.Select();
        SelectedSlot = this;
        inventory.SelectedIventoryItem = item;
        Debug.Log(item.gameObject + " selected for use");

    }
}
