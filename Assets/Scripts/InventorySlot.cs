using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Text NumberTextElement;

    //int inventoryNumber = -1;

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
        //inventoryNumber = i;
        NumberTextElement.text = i.ToString();
    }
}
