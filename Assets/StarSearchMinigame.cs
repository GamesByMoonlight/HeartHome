using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSearchMinigame : MonoBehaviour {

    public GameObject MiniStar;
    public float smallStarGap = 0.05f;

    ClickableStar[] stars;

    class Neighborstars
    {
        public Neighborstars(ClickableStar previousNeighbor, ClickableStar nextNeighbor)
        {
            PreviousNeighbor = previousNeighbor;
            NextNeighbor = nextNeighbor;
        }

        public ClickableStar PreviousNeighbor { get; set; }
        public ClickableStar NextNeighbor { get; set; }
    }
    
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
        Neighborstars neighborStars = GetNeighborStars(starClicked);
                    
        //Debug.Log("Raycast has found object " + objectClicked.collider.gameObject.name);

        if (IsValidNextStar(neighborStars) == true)
        {
            starClicked.HasBeenFound = true;
            starClicked.UpdateStatus();
            ConnectStars();

        } else if (IsFirstStar() == true)
        {
            
            starClicked.HasBeenFound = true;
            starClicked.UpdateStatus();
        }

    }

    bool IsValidNextStar(Neighborstars myNeighbors)
    {


        if (myNeighbors.PreviousNeighbor.HasBeenFound || myNeighbors.NextNeighbor.HasBeenFound)
        {

            return true;
        }
            

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

    void ConnectStars()
    {
        foreach (ClickableStar star in stars)
        {
            Neighborstars myNeighbors = GetNeighborStars(star);
            if (star.IsConnected == false && star.HasBeenFound == true && myNeighbors.NextNeighbor.HasBeenFound)
            {
                Vector3 startPoint = star.transform.position;
                Vector3 endPoint = myNeighbors.NextNeighbor.transform.position;

                Vector3 connectionLine = endPoint - startPoint;
                float smallStarsNeeded = (connectionLine.magnitude / smallStarGap);

                for (float i = 0; i <= smallStarsNeeded; i++)
                {
                    GameObject miniStar = Instantiate(MiniStar, startPoint + (connectionLine/smallStarsNeeded * i), Quaternion.identity);
                    miniStar.transform.parent = star.transform;
                }

                star.IsConnected = true;
            }
        }

        return;
    }

    Neighborstars GetNeighborStars(ClickableStar thisStar)
    {
        thisStar.IveBeenClicked = true;

        int index = 0;

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i].IveBeenClicked == true)
                index = i;
        }

        int previousNeighborIndex, nextNeighborIndex;

        if (index == 0)
        {
            previousNeighborIndex = stars.Length - 1;
            nextNeighborIndex = index + 1;
        }
        else if (index == stars.Length - 1)
        {
            previousNeighborIndex = index - 1;
            nextNeighborIndex = 0;
        }

        else
        {
            previousNeighborIndex = index - 1;
            nextNeighborIndex = index + 1;
        }

        thisStar.IveBeenClicked = false;

        return (new Neighborstars(stars[previousNeighborIndex], stars[nextNeighborIndex]));
    }

       
}
