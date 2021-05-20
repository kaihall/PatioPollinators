// Code courtesy of Patryck Galach (https://bitbucket.org/gaello/grab-system/src/master/)

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

    public Transform homeBase;
    public string itemName;

    /// <summary>
    /// Method called on initialization.
    /// </summary>
    private void Awake()
    {
        // Get reference to the rigidbody
        rb = GetComponent<Rigidbody>();
    }
}