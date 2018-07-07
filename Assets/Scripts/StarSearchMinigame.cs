using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSearchMinigame : MonoBehaviour
{

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickableStar clickedStar = CastRay();

            if (clickedStar)
            {
                clickedStar.transform.parent.GetComponent<Constellation>().UpdateConstellation(clickedStar);
            }
        }
    }

    ClickableStar CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D objectClicked = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (objectClicked.collider == null)
            return null;

        if (objectClicked.collider.gameObject.GetComponent<ClickableStar>() == null)
        {
            Debug.Log("Raycast found " + objectClicked.collider.gameObject);
            return null;
        }

        return objectClicked.collider.gameObject.GetComponent<ClickableStar>();
    }
}
