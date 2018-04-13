using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    
    [SerializeField] GameObject DownAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject RightAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject LeftAperature;  // The "front" of the player.  For use in using items at correct location
    [SerializeField] GameObject UpAperature;  // The "front" of the player.  For use in using items at correct location
    public GameObject ItemUseAperature;

    private List<GameObject> detectedItems;
    public GameObject DetectedItem { get { return GetLastDetectedItem(); }}
    public GameObject DirectionAperature { get { return GetDirection(); } }

    CharacterMovement Movement;

    // Use this for initialization
    void Awake()
    {
        detectedItems = new List<GameObject>();
        Movement = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAction();
    }

    void CheckForAction()
    {
        if (Input.GetButtonDown("Action"))
        {
            if (DetectedItem == null)
            {
                
                UseItem(DirectionAperature);
                return;
            }

            var item = DetectedItem.GetComponent<IItem>();
            if (item == null)
            {
                UseItem(DetectedItem);
                return;
            }

            Inventory.Current.AddInventory(DetectedItem.GetComponent<IItem>());
        }
    }

    void UseItem(GameObject location)
    {
        if (Inventory.Current.SelectedIventoryItem != null)
        {
            Inventory.Current.SelectedIventoryItem.UseAt(location);
        }
    }

    GameObject GetDirection()
    {
        GameObject Aperature = null;
        switch (Movement.Direction)
        {
            case FacingDirection.Up:
                Aperature = UpAperature;
                break;
            case FacingDirection.Down:
                Aperature = DownAperature;
                break;
            case FacingDirection.Left:
                Aperature = LeftAperature;
                break;
            case FacingDirection.Right:
                Aperature = RightAperature;
                break;
            default:
                Aperature = DownAperature;
                break;
        }

        return Aperature;
    }

    GameObject GetLastDetectedItem()
    {
        return detectedItems.Count > 0 ? detectedItems[detectedItems.Count - 1] : null;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        detectedItems.Add(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        detectedItems.Remove(col.gameObject);
    }
}
