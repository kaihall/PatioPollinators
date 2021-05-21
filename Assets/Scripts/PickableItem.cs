// Code by Patryck Galach (https://bitbucket.org/gaello/grab-system/src/master/)
// Item name and home base added by Kai Hall.

using UnityEngine;

/// <summary>
/// Attach this class to make object pickable.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PickableItem : MonoBehaviour
{
    // Reference to the rigidbody
    private Rigidbody rb;
    public Rigidbody Rb => rb;

    public Transform homeBase;  // Where the item will return to when dropped.
    public string itemName;     // Name of the held item. Used to figure out what tool is being used.

    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        // Get reference to the rigidbody
        rb = GetComponent<Rigidbody>();
    }
}