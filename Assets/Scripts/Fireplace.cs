using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireplace : MonoBehaviour {
    
	public GameObject burningLog;
	public int burnForSeconds = 5;

	public bool stuffBurning {get; private set;}

    HeartState heart;

    private void Awake()
    {
        heart = DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<HeartState>();
    }


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
        heart.CurrentState = HeartState.HeartStateValues.Happy;
    }

     IEnumerator StopBurning()
     {
         yield return new WaitForSeconds(burnForSeconds);

		 //the item is all done burning
         burningLog.SetActive(false);
		 this.stuffBurning = false;
        gameObject.tag = "Tool";
        GameEventSystem.Instance.ToolsChanged.Invoke();
        heart.CurrentState = HeartState.HeartStateValues.Cold;
     }	 

}
