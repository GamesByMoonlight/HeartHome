using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireplace : MonoBehaviour {
    
	public GameObject burningLog;
	public int burnForSeconds = 5;

	public bool stuffBurning {get; private set;}



    public void Burn(IItem itemToBurn)
    {
		if (this.stuffBurning) {
			return;
		}

		this.stuffBurning = true;

		Inventory.Current.RemoveInventory(itemToBurn);
		burningLog.SetActive(true);
        gameObject.tag = "Untagged";
        GameEventSystem.Instance.ToolsChanged.Invoke();
	 	StartCoroutine(StopBurning());
        Debug.Log("the fire is so delightful");
    }

     IEnumerator StopBurning()
     {
         yield return new WaitForSeconds(burnForSeconds);

		 //the item is all done burning
         burningLog.SetActive(false);
		 this.stuffBurning = false;
        gameObject.tag = "Tool";
        GameEventSystem.Instance.ToolsChanged.Invoke();
     }	 

}
