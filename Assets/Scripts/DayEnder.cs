// Code by Kai Hall

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script that ends a day without changing the scene
public class DayEnder : MonoBehaviour
{
    public ObjectFadinator fadeSphere;      // A black sphere around the camera that makes the view fade to black when faded in.
    public TutorialManager taskManager;     // Manages the flow of the game, especially the tutorial section.
    public SceneFader sceneFader;           // Fades and transitions to the next scene when the game is done.

    private int dayNumber = 0;              // Number of in-game days passed. Max is 10.
    private bool ending = false;            // Tracks whether the day is ending right now. Stops repeated calls to end-of-day functions when the player clicks the door while the script is running.

    // Manages a day's end. Fades to black, clears held items, and transitions to the next day or the end scene.
    public void EndDay() {
        // Only run function if the day isn't already ending.
        if (!ending)
        {
            ending = true;

            fadeSphere.FadeIn();                // Fade to black
            taskManager.grabSystem.DropItem();  // Drop held items

            // If it's the last day (day 10), ends the game.
            if (dayNumber == 10)
            {
                sceneFader.Transition();
            }
            // Otherwise, increments the number of days and starts the next day.
            else
            {
                dayNumber++;
                StartCoroutine("StartDay");
            }
        }
    }

    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(1.5f);  // Waits to create feeling of time passing.

        // If the tutorial is complete, clears the tutorial instructions.
        if (taskManager.stage == 7)
            taskManager.stage = 8;

        // Tell each plant to grow.
        foreach (PlantInteractor plant in taskManager.plants)
        {
            plant.Grow();
        }

        // Fade scene back in
        fadeSphere.FadeOut();
        // Day is no longer ending
        ending = false;
    }
}
