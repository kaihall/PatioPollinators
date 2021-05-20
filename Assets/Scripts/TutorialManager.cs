using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TextMeshPro stepText;
    public TextMeshPro taskText;

    public SimpleGrabSystem grabSystem;

    public List<PlantInteractor> plants;

    internal int stage = 0;
    
    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                stepText.text = "Step 1: Dig Hole";
                taskText.text = "Grab the trowel behind you.";
                if (grabSystem.PickedItemName().Equals("Trowel"))
                    stage = 1;
                break;
            case 1:
                stepText.text = "Step 1: Dig Hole";
                taskText.text = "Use the trowel on the flower pot.";
                if (plants[0].holeDug)
                    stage = 2;
                break;
            case 2:
                stepText.text = "Step 2: Plant Seed";
                taskText.text = "Grab the seed packet behind you.";
                if (grabSystem.PickedItemName().Equals("Seeds"))
                    stage = 3;
                break;
            case 3:
                stepText.text = "Step 2: Plant Seed";
                taskText.text = "Activate the flower pot to plant your seeds.";
                if (plants[0].seedPlanted)
                    stage = 4;
                break;
            case 4:
                stepText.text = "Step 3: Water Plant";
                taskText.text = "Grab the watering can behind you.";
                if (grabSystem.PickedItemName().Equals("Watering Can"))
                    stage = 5;
                break;
            case 5:
                stepText.text = "Step 3: Water Plant";
                taskText.text = "Activate the flower pot to water your plant.";
                if (plants[0].watered)
                {
                    stage = 6;
                    plants[1].gameObject.SetActive(true);
                    plants[2].gameObject.SetActive(true);
                }
                break;
            case 6:
                stepText.text = "Step 4: Repeat";
                taskText.text = "Plant and water two new seeds.";
                if (plants[1].watered && plants[2].watered)
                    stage = 7;
                break;
            case 7:
                stepText.text = "Step 5: End the Day";
                taskText.text = "Activate the door to end the day and let your plants grow.";
                break;
            default:
                stepText.text = "";
                taskText.text = "Grow your garden. Plants need water.";
                break;
        }

        if (plants[0].stage.Equals("adult") && !plants[3].isActiveAndEnabled)
        {
            plants[3].gameObject.SetActive(true);
        }
        if (plants[1].stage.Equals("adult") && !plants[4].isActiveAndEnabled)
        {
            plants[4].gameObject.SetActive(true);
        }
        if (plants[2].stage.Equals("adult") && !plants[5].isActiveAndEnabled)
        {
            plants[5].gameObject.SetActive(true);
        }
    }
}
