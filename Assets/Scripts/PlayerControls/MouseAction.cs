using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAction : MonoBehaviour {
    public float MouseDownLag = .2f;    // How many seconds we should wait before registering another mouse click

    float lastMouseDown = 0f;

    // Update is called once per frame
    void Update()
    {
        CheckForAction();
    }

    void CheckForAction()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time - lastMouseDown > MouseDownLag)
            {
                lastMouseDown = Time.time;
                if(Inventory.Current.SelectedIventoryItem != null)
                    Inventory.Current.SelectedIventoryItem.UseAt(Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y)));
            }
        }
    }
}
