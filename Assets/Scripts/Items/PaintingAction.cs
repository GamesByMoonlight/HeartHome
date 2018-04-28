using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAction : MonoBehaviour
{

    public Item addableObject1;
    public Item addableObject2;
    public Item addableObject3;

    private bool fireplaceUsed = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        ActionCheck();
        Debug.Log("Col - Stay");
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        ActionCheck();

        Debug.Log("Col - Enter");
    }

    void ActionCheck()
    {
        if (Input.GetButtonDown("Action") && !fireplaceUsed)
        {
            Inventory.Current.AddInventory(addableObject1);
            Inventory.Current.AddInventory(addableObject2);
            Inventory.Current.AddInventory(addableObject3);
            fireplaceUsed = true;
        }

    }

}
