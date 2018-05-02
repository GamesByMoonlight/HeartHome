using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnableItem : Item {

    public string instrument;

    public override void UseAt(GameObject location)
    {
        var firePlace = location.GetComponent<Fireplace>();
        if (firePlace != null)
        {
            firePlace.Burn(this);

            if (instrument != null)
            {
                FindObjectOfType<CastleMusic>().StopTrack(instrument);
            }
        }
    }


}
