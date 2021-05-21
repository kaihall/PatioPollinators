// Code by Kai Hall.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Primarily manages the text for the instructions on the sign.
// Also rewards the player with new plants once they grow the first three and tracks all the plants in the scene.
// Ended up being more of a game manager.
public class TutorialManager : MonoBehaviour
{
    // Sign text objects
    public TextMeshPro stepText;
    public TextMeshPro taskText;

    // Manages player's held item.
    public SimpleGrabSystem grabSystem;

    // List of all plants in the scene.
    public List<PlantInteractor> plants;

    // Current tutorial stage
    public int stage = 0;
    
    // Update is called once per frame
    void Update()
    {
        // Manages tutorial instructions.
        switch (stage)
        {
            // Advances once the player picks up the trowel.
            case 0:
                stepText.text = "Step 1: Dig Hole";
                taskText.text = "Grab the trowel behind you.";
                if (grabSystem.PickedItemName().Equals("Trowel"))
                    stage = 1;
                break;
            // Advances once the player digs a hole for the seed.
            case 1:
                stepText.text = "Step 1: Dig Hole";
                taskText.text = "Use the trowel on the flower pot.";
                if (plants[0].holeDug)
                    stage = 2;
                break;
            // Advances once the player picks up the seeds.
            case 2:
                stepText.text = "Step 2: Plant Seed";
                taskText.text = "Grab the seed packet behind you.";
                if (grabSystem.PickedItemName().Equals("Seeds"))
                    stage = 3;
                break;
            // Advances once the player plants their first seed.
            case 3:
                stepText.text = "Step 2: Plant Seed";
                taskText.text = "Activate the flower pot to plant your seeds.";
                if (plants[0].seedPlanted)
                    stage = 4;
                break;
            // Advances once the player grabs the watering can.
            case 4:
                stepText.text = "Step 3: Water Plant";
                taskText.text = "Grab the watering can behind you.";
                if (grabSystem.PickedItemName().Equals("Watering Can"))
                    stage = 5;
                break;
            // Advances once the player waters the first plant.
            case 5:
                stepText.text = "Step 3: Water Plant";
                taskText.text = "Activate the flower pot to water your plant.";
                if (plants[0].watered)
                {
                    stage = 6;
                    // Gives the player two new plants.
                    plants[1].gameObject.SetActive(true);
                    plants[2].gameObject.SetActive(true);
                }
                break;
            // Advances once the player plants and cares for two more plants.
            case 6:
                stepText.text = "Step 4: Repeat";
                taskText.text = "Plant and water two new seeds.";
                if (plants[1].watered && plants[2].watered)
                    stage = 7;
                break;
            // Advances once the player completes all other tutorial steps and ends the first day.
            case 7:
                stepText.text = "Step 5: End the Day";
                taskText.text = "Activate the door to end the day and let your plants grow.";
                break;
            // No tutorial instructions.
            default:
                stepText.text = "";
                taskText.text = "Grow your garden. Plants need water.";
                break;
        }

        // Rewards the player with a new plant every time one of the first three reaches the adult stage.
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
