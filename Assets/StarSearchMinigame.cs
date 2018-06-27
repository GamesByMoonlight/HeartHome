using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSearchMinigame : MonoBehaviour {

    ClickableStar[] stars;
    
	// Use this for initialization
	void Start () {
        stars = GetComponentsInChildren<ClickableStar>();
	}
	
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D objectClicked = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (objectClicked.collider == null)
            return;

        if (objectClicked.collider.gameObject.GetComponent<ClickableStar>() == null)
            return;

        ClickableStar starClicked = objectClicked.collider.gameObject.GetComponent<ClickableStar>();

                    
        //Debug.Log("Raycast has found object " + objectClicked.collider.gameObject.name);

        if (IsValidNextStar(starClicked) == true)
        {
            Debug.Log("Available Next Star clicked");

            starClicked.HasBeenFound = true;
            starClicked.UpdateStatus();

        } else if (IsFirstStar() == true)
        {
            starClicked.HasBeenFound = true;
            starClicked.UpdateStatus();
        }

    }

    bool IsValidNextStar(ClickableStar clickedStar)
    {
        clickedStar.IveBeenClicked = true;

        int index = 0;
        
        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i].IveBeenClicked == true)
                index = i;
        }

        int previousNeighborIndex, nextNeighborIndex;

        previousNeighborIndex = index - 1;
        nextNeighborIndex = index + 1;

        if (index == 0)
            previousNeighborIndex = stars.Length - 1;
        if (index == stars.Length)
            nextNeighborIndex = 0;

        if (stars[previousNeighborIndex].HasBeenFound || stars[nextNeighborIndex].HasBeenFound)
            return true;

        return false;
    }

    // Method to check the entire array of stars in this object and if no star has been activated, retur
    bool IsFirstStar()
    {
        foreach (ClickableStar star in stars)
        {
            if (star.HasBeenFound == true)
                return false;
        }

        return true;
    }
}
