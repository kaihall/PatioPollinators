// Code courtesy of Patryck Galach (https://bitbucket.org/gaello/grab-system/src/master/)

using UnityEngine;

/// <summary>
/// Simple example of Grabbing system.
/// </summary>
public class SimpleGrabSystem : MonoBehaviour
{
    // Reference to the character camera.
    [SerializeField]
    private Camera characterCamera;

    // Reference to the slot for holding picked item.
    [SerializeField]
    private Transform slot;

    // Reference to the currently held item.
    private PickableItem pickedItem;

    /// <summary>
    /// Method called very frame.
    /// </summary>
    /*
    private void Update()
    {
        // Execute logic only on button pressed
        if (XRInputManager.IsButtonDown())
        {
            // Check if player picked some item already
            if (pickedItem)
            {
                // If yes, drop picked item
                DropItem(pickedItem);
            }
            
            else
            {
                // If no, try to pick item in front of the player
                // Create ray from center of the screen
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                // Shot ray to find object to pick
                if (Physics.Raycast(ray, out hit, 1.5f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();

                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick it
                        PickItem(pickable);
                    }
                }
            }
        }
    }
    */

    /// <summary>
    /// Method for picking up item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void PickItem(PickableItem item)
    {
        // Drop held item (if any)
        if (pickedItem) {
            DropItem();
        }
        
        // Assign reference
        pickedItem = item;

        // Disable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        // Set Slot as a parent
        item.transform.SetParent(slot);

        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;

    }

    /// <summary>
    /// Method for dropping item.
    /// </summary>
    /// <param name="item">Item.</param>
    public void DropItem()
    {
        // Remove parent
        pickedItem.transform.SetParent(null);

        // Enable rigidbody
        pickedItem.Rb.isKinematic = false;

        // Set item's home base as a parent
        pickedItem.transform.SetParent(pickedItem.homeBase);

        // Reset position and rotation
        pickedItem.transform.localPosition = Vector3.zero;
        pickedItem.transform.localEulerAngles = Vector3.zero;

        // Remove reference
        pickedItem = null;
    }

    public string PickedItemName() {
        if (pickedItem == null)
            return "";

        return pickedItem.itemName;
    }
}