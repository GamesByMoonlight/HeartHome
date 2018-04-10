using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour {
    public GameObject DetectedItem;
    [SerializeField] GameObject DownAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject RightAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject LeftAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject UpAperature;  // The "front" of the player.  For use in using items at correct location

    CharacterMovement Movement;

	// Use this for initialization
	void Awake () {
        Movement = GetComponent<CharacterMovement>();
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
                    switch(Movement.Direction)
                    {
                        case FacingDirection.Up:
                            Inventory.Current.SelectedIventoryItem.UseAt(UpAperature);
                            break;
                        case FacingDirection.Down:
                            Inventory.Current.SelectedIventoryItem.UseAt(DownAperature);
                            break;
                        case FacingDirection.Left:
                            Inventory.Current.SelectedIventoryItem.UseAt(LeftAperature);
                            break;
                        case FacingDirection.Right:
                            Inventory.Current.SelectedIventoryItem.UseAt(RightAperature);
                            break;
                    }
                    //Inventory.Current.SelectedIventoryItem.UseAt(Aperature);
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
