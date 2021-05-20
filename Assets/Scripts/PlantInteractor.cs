using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractor : MonoBehaviour
{
    public SimpleGrabSystem grabSystem;

    public Material validActionMaterial;
    public Material invalidActionMaterial;

    internal bool holeDug = false;
    internal bool seedPlanted = false;
    internal bool watered = false;

    internal string stage = "unplanted";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hover()
    {
        if (grabSystem.PickedItemName().Equals(""))
            return;

        bool validAction = false;

        if (grabSystem.PickedItemName().Equals("Trowel"))
            validAction = !holeDug;
        else if (grabSystem.PickedItemName().Equals("Seeds"))
            validAction = holeDug && !seedPlanted;
        else if (grabSystem.PickedItemName().Equals("Watering Can"))
            validAction = holeDug && seedPlanted && !watered;

        if (validAction)
            GetComponent<MeshRenderer>().material = validActionMaterial;
        else
            GetComponent<MeshRenderer>().material = invalidActionMaterial;
    }

    public void Activate() {
        if (grabSystem.PickedItemName().Equals("Trowel"))
        {
            if (!holeDug)
            {
                DigHole();
            }
        }
        else if (grabSystem.PickedItemName().Equals("Seeds"))
        {
            if (holeDug && !seedPlanted)
            {
                PlantSeed();
            }
        }
        else if (grabSystem.PickedItemName().Equals("Watering Can"))
        {
            if (holeDug && seedPlanted)
            {
                watered = true;
            }
        }
        else
        {
            grabSystem.DropItem();
        }
    }

    private void DigHole()
    {
        holeDug = true;
        transform.Find("SeedHole").gameObject.SetActive(true);
    }

    private void PlantSeed()
    {
        seedPlanted = true;
        stage = "planted";
        transform.Find("SeedHole").gameObject.SetActive(false);
        transform.Find("SeedMound").gameObject.SetActive(true);
    }

    public void Grow()
    {
        if (watered)
        {
            if (stage.Equals("planted"))
            {
                stage = "shoot";
                transform.Find("SeedMound").gameObject.SetActive(false);
                transform.Find("milkweed_shoot").gameObject.SetActive(true);
            }
            else if (stage.Equals("shoot"))
            {
                stage = "preflower1";
                transform.Find("milkweed_shoot").gameObject.SetActive(false);
                transform.Find("milkweed_preflower1").gameObject.SetActive(true);
            }
            else if (stage.Equals("preflower1"))
            {
                stage = "preflower2";
                transform.Find("milkweed_preflower1").gameObject.SetActive(false);
                transform.Find("milkweed_preflower2").gameObject.SetActive(true);
            }
            else if (stage.Equals("preflower2"))
            {
                stage = "adult";
                transform.Find("milkweed_preflower2").gameObject.SetActive(false);
                transform.Find("milkweed_adult").gameObject.SetActive(true);
            }
            else if (stage.Equals("adult"))
            {
                stage = "adult1";
                transform.Find("butterfly1").gameObject.SetActive(true);
            }
            else if (stage.Equals("adult1"))
            {
                stage = "adult2";
                transform.Find("butterfly2").gameObject.SetActive(true);
            }
            else if (stage.Equals("adult2"))
            {
                stage = "adult3";
                transform.Find("butterfly3").gameObject.SetActive(true);
            }
        }

        watered = false;
    }
}
