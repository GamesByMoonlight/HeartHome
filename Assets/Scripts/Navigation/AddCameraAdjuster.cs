using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCameraAdjuster : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerAction>();
        if(player)
        {
            var adjust = player.gameObject.GetComponent<CameraAdjuster>();
            if (adjust)
                adjust.adjusting = true;
            else
                player.gameObject.AddComponent<CameraAdjuster>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerAction>();
        if (player)
        {
            var adjust = player.gameObject.GetComponent<CameraAdjuster>();
            if (adjust)
                adjust.adjusting = false;
        }
    }
}
