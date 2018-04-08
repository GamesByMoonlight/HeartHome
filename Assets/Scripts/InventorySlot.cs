using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDeselectHandler {
    private static InventorySlot SelectedSlot;

    public Image ImageIconElement;
    public Sprite SelectedSprite;
    public Text NumberTextElement;

    private IItem item;
    public IItem Item { get { return item; } set { SetItem(value); } }

    int inventoryNumber = -1;

    Button button;
    Image buttonImage;
    Sprite NormalSprite;


	// Use this for initialization
	void Awake () {
        InitializeSlot();
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        NormalSprite = buttonImage.sprite;

        if(inventoryNumber > 9)
        {
            Debug.LogError("Can not have more than 9 items in inventory.  Should this ever happen in our game?  Destroying slot and item");
            Destroy(gameObject);
        }

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
            SlotSelected();
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

    public void SlotSelected()
    {
        button.Select();
        SelectedSlot = this;
        Inventory.Current.SelectedIventoryItem = item;
        Debug.Log(item.gameObject + " selected for use");

    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("deselect called");
        if(SelectedSlot == this)
        {
            buttonImage.sprite = SelectedSprite;
        }else{
            buttonImage.sprite = NormalSprite;
        }
    }
}
