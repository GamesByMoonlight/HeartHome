using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAction : MonoBehaviour
{

    public Item addableObject1;
    public Item addableObject2;
    public Item addableObject3;

    private bool collided = false;
    private bool fireplaceUsed = false;

    // Update is called once per frame
    void Update()
    {
        ActionCheck();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        collided = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        collided = false;
    }

    void ActionCheck()
    {
        if (Input.GetButtonDown("Action") && collided && !fireplaceUsed)
        {
            Inventory.Current.AddInventory(addableObject1);
            Inventory.Current.AddInventory(addableObject2);
            Inventory.Current.AddInventory(addableObject3);
            fireplaceUsed = true;
        }

    }

}
