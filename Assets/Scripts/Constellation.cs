using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constellation : MonoBehaviour {

    public GameObject MiniStar;
    public float smallStarGap = 0.08f;

    protected ClickableStar[] stars;

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
	
    public void UpdateConstellation(ClickableStar starClicked)
    {
        Constellation thisConstellation = starClicked.transform.parent.GetComponent<Constellation>();

        //Debug.Log("starClicked - " + starClicked + " in thisConstellation - " + thisConstellation);
        
        Neighborstars neighborStars = GetNeighborStars(starClicked);
        

        if (IsValidNextStar(neighborStars) == true)
        {   
            starClicked.HasBeenFound = true;
            starClicked.UpdateStatus();
            ConnectStars(starClicked);

        } else if (IsFirstStar(starClicked) == true)
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
    bool IsFirstStar(ClickableStar starToCheck)
    {
        ClickableStar[] starArray = starToCheck.transform.parent.GetComponent<Constellation>().stars;

        foreach (ClickableStar star in starArray)
        {
            if (star.HasBeenFound == true)
                return false;
        }

        //Debug.Log("First Star in consteallation " + starToCheck.transform.parent);

        return true;
    }

    void ConnectStars(ClickableStar starToCheck)
    {
        ClickableStar[] starArray = starToCheck.transform.parent.GetComponent<Constellation>().stars;

        foreach (ClickableStar star in starArray)
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
