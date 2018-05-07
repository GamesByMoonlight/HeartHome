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
    int counter = 0;

    private void Start()
    {
        gm = DontDestroyPlayerOnLoad.playerObject.GetComponentInChildren<GameManager>();

        PlayTheRightMusic(gm.PaintingItemsAdded);

        if (gm.PaintingItemsAdded >= 3)
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
            AddItem(counter++);
            gm.PaintingItemsAdded = counter;
        }

    }

    void AddItem(int item)
    {
        switch(item)
        {
            case 0:
                Inventory.Current.AddInventory(addableObject1);
                PlayTrack(item);
                break;
            case 1:
                Inventory.Current.AddInventory(addableObject2);
                PlayTrack(item);
                break;
            case 2:
                Inventory.Current.AddInventory(addableObject3);
                PlayTrack(item);
                paintingGamePlayed = true;
                break;
        }
    }

    void PlayTrack(int track)
    {
        switch (track)
        {
            case 0:
                castleMusic.StartTrack("violin");
                break;
            case 1:
                castleMusic.StartTrack("harp");
                break;
            case 2:
                castleMusic.StartTrack("percussion");
                break;
        }
    }

    void PlayTheRightMusic(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; ++i)
        {
            PlayTrack(i);
        }
    }

    void FinishGameAndOpenRoom()
    {
        gameObject.tag = "Untagged";
        GameEventSystem.Instance.ToolsChanged.Invoke();
        Destroy(ObstacleToRemove);
    }
}
