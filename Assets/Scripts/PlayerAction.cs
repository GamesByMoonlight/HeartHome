using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public GameObject DetectedItem;
    public GameObject Aperature;  // The front of the player

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckForAction();
	}

	void CheckForAction() {
        if(Input.GetButtonDown("Action"))
        {
			if (DetectedItem == null) {
                if(Inventory.Current.SelectedIventoryItem != null)
                {
                    Inventory.Current.SelectedIventoryItem.UseAt(Aperature);
                }
                return;
			}

			var item = DetectedItem.GetComponent<IItem>();
			if (item == null) {
				return;
			}

            Inventory.Current.AddInventory(DetectedItem.GetComponent<IItem>());
        }		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		this.DetectedItem = collider.gameObject;	
	}

	void OnTriggerExit2D(Collider2D collider) {
		this.DetectedItem = null;	
	}	
}
