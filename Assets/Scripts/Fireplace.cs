using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireplace : MonoBehaviour {

	public GameObject burningLog;
	public int burnForSeconds = 5;

	public bool stuffBurning {get; private set;}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Burn(IItem itemToBurn)
    {
		if (this.stuffBurning) {
			return;
		}

		this.stuffBurning = true;

		Inventory.Current.RemoveInventory(itemToBurn);
		burningLog.SetActive(true);
	 	StartCoroutine(StopBurning());
        Debug.Log("the fire is so delightful");
    }

     IEnumerator StopBurning()
     {
         yield return new WaitForSeconds(burnForSeconds);

		 //the item is all done burning
         burningLog.SetActive(false);
		 this.stuffBurning = false;
     }	 

}
