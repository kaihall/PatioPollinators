using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayEnder : MonoBehaviour
{
    public ObjectFadinator fadeSphere;
    public TutorialManager taskManager;
    public SceneFader sceneFader;

    private int dayNumber = 0;
    private bool ending = false;

    public void EndDay() {
        if (!ending)
        {
            ending = true;

            fadeSphere.FadeIn();
            taskManager.grabSystem.DropItem();

            if (dayNumber == 10)
            {
                sceneFader.Transition();
            }
            else
            {
                dayNumber++;
                StartCoroutine("StartDay");
            }
        }
    }

    IEnumerator StartDay()
    {
        yield return new WaitForSeconds(1.5f);

        if (taskManager.stage == 7)
            taskManager.stage = 8;

        foreach (PlantInteractor plant in taskManager.plants)
        {
            plant.Grow();
        }

        fadeSphere.FadeOut();
        ending = false;
    }
}
