using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
                 
public class InitSlotNumber : MonoBehaviour {
    public Text NumberTextElement;

    private void Awake()
    {
        InitializeSlot();
    }

    private void OnValidate()
    {
        InitializeSlot();
    }

    void InitializeSlot()
    {
        if (transform == null || transform.parent == null)
            return;

        var slots = transform.parent.GetComponentsInChildren<InitSlotNumber>();

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
        GetComponentInChildren<InventorySlot>().InventoryNumber = i;
        if (NumberTextElement != null)
            NumberTextElement.text = i.ToString();
    }
}
