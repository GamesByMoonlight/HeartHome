using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public GameObject currentItem;
	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CheckForAction();
	}

	void CheckForAction() {
        if(Input.GetButtonDown("Jump"))
        {
			if (currentItem == null) {
				return;
			}

			var item = currentItem.GetComponent<IItem>();
			if (item == null) {
				return;
			}

            Inventory.Current.AddInventory(currentItem.GetComponent<IItem>());
        }		
	}

	void OnTriggerEnter2D(Collider2D collider) {
		this.currentItem = collider.gameObject;	
		// Debug.Log("current item: ");
		// Debug.Log(currentItem);
	}

	void OnTriggerExit2D(Collider2D collider) {
		this.currentItem = null;	
	}	
}
