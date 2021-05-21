// Code by Kai Hall.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Fades to black then switches the scene.
public class SceneFader : MonoBehaviour
{
    public ObjectFadinator fadeSphere;      // A black sphere that causes the view to fade to black when faded in.
    public string sceneName;                // Name of the next scene in the editor.
    public float transitionWaitTime = 5;    // Number of seconds the screen will stay black before switching the scene.
    private bool ending = false;            // Whether the scene is transitioning right now.

    public void Transition()
    {
        // If the scene isn't already ending...
        if (!ending)
        {
            ending = true;          // ... it is now.
            fadeSphere.FadeIn();    // Fade to black.
            StartCoroutine("ChangeScene");  // Switch scene.
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(transitionWaitTime);    // Wait for the transition time.
        SceneManager.LoadScene(sceneName);                      // Load the next scene.
    }
}
