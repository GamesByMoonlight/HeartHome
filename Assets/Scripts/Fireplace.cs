using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FirePlace : MonoBehaviour {

	public GameObject burningLog;
	public int burnForSeconds = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Burn()
    {
		burningLog.SetActive(true);
	 	StartCoroutine(StopBurning());
        Debug.Log("the fire is so delightful");
    }

     IEnumerator StopBurning()
     {
         yield return new WaitForSeconds(burnForSeconds);

		 //the item is all done burning
         burningLog.SetActive(false);
     }	 

}
