// Code by Kai Hall.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractor : MonoBehaviour
{
    // Manages player's held items.
    public SimpleGrabSystem grabSystem;

    // Highlighting materials for hovering.
    public Material validActionMaterial;
    public Material invalidActionMaterial;

    // Plant status variables.
    internal bool holeDug = false;          // Has a hole been dug for the plant's seed?
    internal bool seedPlanted = false;      // Has the seed been planted?
    internal bool watered = false;          // Has the plant been watered today?

    // Growth stage of the plant.
    internal string stage = "unplanted";

    // Called when hovering over the pot.
    public void Hover()
    {
        // If the player isn't holding a tool, don't highlight the pot. Just let them look at it.
        if (grabSystem.PickedItemName().Equals(""))
            return;

        // Assume that the player is trying to perform an invalid action.
        bool validAction = false;

        // Player can use the trowel if they haven't dug a hole for the seed yet.
        if (grabSystem.PickedItemName().Equals("Trowel"))
            validAction = !holeDug;
        // Player can use the seeds if they haven't planted the milkweed yet.
        else if (grabSystem.PickedItemName().Equals("Seeds"))
            validAction = holeDug && !seedPlanted;
        // Player can use the watering can if they haven't watered the plant yet today.
        else if (grabSystem.PickedItemName().Equals("Watering Can"))
            validAction = holeDug && seedPlanted && !watered;

        // If the player can perform the action, highlight the pot with the valid action material.
        if (validAction)
            GetComponent<MeshRenderer>().material = validActionMaterial;
        // Otherwise, highlight the pot with the invalid action material.
        else
            GetComponent<MeshRenderer>().material = invalidActionMaterial;
    }

    // Called when the player presses the trigger while hovering over the pot.
    public void Activate() {
        // If the player is holding the trowel and a hole has not yet been dug for the seed, dig a hole.
        if (grabSystem.PickedItemName().Equals("Trowel"))
        {
            if (!holeDug)
            {
                DigHole();
            }
        }
        // If the player is holding the seeds and a seed has not yet been planted, plant a seed.
        else if (grabSystem.PickedItemName().Equals("Seeds"))
        {
            if (holeDug && !seedPlanted)
            {
                PlantSeed();
            }
        }
        // If the player is holding the watering can and the plant has been planted, water it.
        else if (grabSystem.PickedItemName().Equals("Watering Can"))
        {
            if (holeDug && seedPlanted)
            {
                watered = true;
            }
        }
        // Drop any invalid items.
        else
        {
            grabSystem.DropItem();
        }
    }

    // Digs a hole in the dirt and allows the player to plant a seed.
    private void DigHole()
    {
        holeDug = true;
        transform.Find("SeedHole").gameObject.SetActive(true);  // Activates hole visual
    }

    // Plants a seed in the dirt and allows the plant to start growing when Grow() is called.
    private void PlantSeed()
    {
        seedPlanted = true;
        stage = "planted";
        transform.Find("SeedHole").gameObject.SetActive(false); // Deactivates hole visual
        transform.Find("SeedMound").gameObject.SetActive(true); // Activates mound of dirt visual
    }

    // Messy method that advances the plant's growth stage by deactivating and activating different plant models.
    public void Grow()
    {
        // Plant only grows if watered.
        if (watered)
        {
            // Grows to a shoot.
            if (stage.Equals("planted"))
            {
                stage = "shoot";
                transform.Find("SeedMound").gameObject.SetActive(false);
                transform.Find("milkweed_shoot").gameObject.SetActive(true);
            }
            // Grows to a slightly larger plant with leaves.
            else if (stage.Equals("shoot"))
            {
                stage = "preflower1";
                transform.Find("milkweed_shoot").gameObject.SetActive(false);
                transform.Find("milkweed_preflower1").gameObject.SetActive(true);
            }
            // Grows to a full-sized plant with leaves but no flowers.
            else if (stage.Equals("preflower1"))
            {
                stage = "preflower2";
                transform.Find("milkweed_preflower1").gameObject.SetActive(false);
                transform.Find("milkweed_preflower2").gameObject.SetActive(true);
            }
            // Grows to a full-sized adult plant with flowers.
            else if (stage.Equals("preflower2"))
            {
                stage = "adult";
                transform.Find("milkweed_preflower2").gameObject.SetActive(false);
                transform.Find("milkweed_adult").gameObject.SetActive(true);
            }
            // Adds a butterfly to the plant.
            else if (stage.Equals("adult"))
            {
                stage = "adult1";
                transform.Find("butterfly1").gameObject.SetActive(true);
            }
            // Adds a second butterfly.
            else if (stage.Equals("adult1"))
            {
                stage = "adult2";
                transform.Find("butterfly2").gameObject.SetActive(true);
            }
            // Adds a third butterfly.
            else if (stage.Equals("adult2"))
            {
                stage = "adult3";
                transform.Find("butterfly3").gameObject.SetActive(true);
            }
        }

        // Plant requires more water to grow again.
        watered = false;
    }
}
