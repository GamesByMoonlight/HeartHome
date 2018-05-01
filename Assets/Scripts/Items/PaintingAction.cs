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
    private bool paintingGamePlayed = false;
    HeartState heart;
    GameManager gm;

    private void Start()
    {
        gm = DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<GameManager>();
        if (gm.PaintingMiniGamePlayed)
        {
            FinishGameAndOpenRoom();
            return;
        }

        heart = DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<HeartState>();
        castleMusic = FindObjectOfType<CastleMusic>();
        StartCoroutine(Running());
    }

    // Update is called once per frame
    IEnumerator Running()
    {
        while (!paintingGamePlayed)
        {
            ActionCheck();
            yield return null;
        }
        FinishGameAndOpenRoom();
        heart.CurrentState = HeartState.HeartStateValues.Happy;
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
        if (Input.GetButtonDown("Action") && collided && !paintingGamePlayed)
        {
            Inventory.Current.AddInventory(addableObject1);
            castleMusic.StartTrack("violin");
            Inventory.Current.AddInventory(addableObject2);
            castleMusic.StartTrack("harp");
            Inventory.Current.AddInventory(addableObject3);
            castleMusic.StartTrack("percussion");
            paintingGamePlayed = true;
            gm.PaintingMiniGamePlayed = true;
        }

    }

    void FinishGameAndOpenRoom()
    {
        gameObject.tag = "Untagged";
        GameEventSystem.Instance.ToolsChanged.Invoke();
        Destroy(ObstacleToRemove);
    }
}
