// Code by Hunt Sparra. Raycasting added by Kai Hall (unused in project).

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// MonoBehavior for an interactable objects. Triggers different <c>UnityEvent</c>s
/// (sets of functions) when the user is gazing at the object. The margin of error
/// for gazing (in degrees) can be adjusted to allow for more generous, comfortable
/// control.
/// </summary>
public class GazeOverEvent : MonoBehaviour
{
    /// <summary>
    /// A boolean that determines which version of object selection to use: 
    /// Raycasting or Field of View. When true, the object will be activated
    /// when hit by a ray originating from the center of the screen.
    /// </summary>
    public bool useRaycasting = false;

    /// <summary>
    /// A boolean that determines whether the ray is shown on the screen in
    /// raycasting mode.
    /// </summary>
    public bool raycastDebuggingMode = false;

    /// <summary>
    /// Margin of error for a valid gaze. The number of degrees off from a direct
    /// gaze that will still be counted as looking at the object.
    /// </summary>
    /// <remarks>This should be larger for larger objects.</remarks>
    [Range(0, 360)]
    public float maximumAngleForEvent = 30f;

    public UnityEvent OnHoverBegin;
    public UnityEvent OnHover;
    public UnityEvent OnHoverEnd;
    public UnityEvent OnButtonPressedDuringHover;

    /// <summary>
    /// A boolean that tracks if the object is currently hovered over. Used to
    /// ensure OnHoverBegin and OnHoverEnd are only fired once per gaze start/end.
    /// </summary>
    private bool isHovering = false;

    void Update()
    {
        bool looking = false;
        if (useRaycasting)
        {
            // The player is looking at the object if a ray cast from the center of the screen hits its collider
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            // If an object is hit...
            if (Physics.Raycast(ray, out hit)) {
                GameObject objectHit = hit.transform.gameObject;
                // and that object is the GameObject this script is attached to...
                if (objectHit == this.gameObject) {
                    // then the player is looking at the object
                    looking = true;
                }
            }

            if (raycastDebuggingMode)
            {
                // For debugging only. Draws a ray where the player is looking.
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
        }
        else
        {
            // The player is looking at the object if it is within a certain field of view
            var cameraForward = Camera.main.transform.forward;
            var vectorToObject = transform.position - Camera.main.transform.position;

            // Check if the angle between the camera and object is within the hover range
            var angleFromCameraToObject = Vector3.Angle(cameraForward, vectorToObject);
            looking = angleFromCameraToObject <= maximumAngleForEvent;
        }
        
        if (looking)
        {
            Hovering();

            if (XRInputManager.IsButtonDown())
            {
                ButtonPressed();
            }
        }
        else
        {
            NotHovering();
        }
    }

    private void Hovering()
    {
        if (isHovering)
        {
            OnHover.Invoke();
        }
        else
        {
            OnHoverBegin.Invoke();
            isHovering = true;
        }
    }

    private void NotHovering()
    {
        if (isHovering)
        {
            OnHoverEnd.Invoke();
            isHovering = false;
        }
    }

    private void ButtonPressed()
    {
        OnButtonPressedDuringHover.Invoke();
    }
}
