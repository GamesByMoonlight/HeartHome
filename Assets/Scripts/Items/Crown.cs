using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : Item {



    public override void UseAt(GameObject location)
    {


        var firePlace = location.GetComponent<FirePlace>();
        if (firePlace != null)
        {
            firePlace.Burn();
            Inventory.Current.RemoveInventory(this);
        }
    }

}
