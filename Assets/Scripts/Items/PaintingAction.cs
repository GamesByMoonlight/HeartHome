using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingAction : MonoBehaviour
{

    public Item addableObject1;
    public Item addableObject2;
    public Item addableObject3;
    public GameObject ObstacleToRemove;

    public CastleMusic castleMusic;

    private bool collided = false;
    private bool fireplaceUsed = false;
    HeartState heart;

    private void Start()
    {
        heart = FindObjectOfType<HeartState>();
        castleMusic = FindObjectOfType<CastleMusic>();
        StartCoroutine(Running());
    }

    // Update is called once per frame
    IEnumerator Running()
    {
        while (!fireplaceUsed)
        {
            ActionCheck();
            yield return null;
        }
        gameObject.tag = "Untagged";
        GameEventSystem.Instance.ToolsChanged.Invoke();
        Destroy(ObstacleToRemove);
        heart.CurrentState = HeartState.HeartStateValues.Cold;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collided = false;
    }

    void ActionCheck()
    {
        if (Input.GetButtonDown("Action") && collided && !fireplaceUsed)
        {
            Inventory.Current.AddInventory(addableObject1);
            castleMusic.StartTrack("violin");
            Inventory.Current.AddInventory(addableObject2);
            castleMusic.StartTrack("harp");
            Inventory.Current.AddInventory(addableObject3);
            castleMusic.StartTrack("percussion");
            fireplaceUsed = true;
        }

    }

}
