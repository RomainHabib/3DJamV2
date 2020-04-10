using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour
{
    [Header("Object")]
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    [Header("A la mano")]
    public float timeToSearch;
    public bool keepTime;

    [Header("With object")]
    public GameObject objectToHave;
    public float timeToSearchObj;
    public bool keepTimeObj;

    [HideInInspector]
    public bool searched = false;
    [HideInInspector]
    public float timeSearched = 0.0f;

    public void SpawnItem()
    {
        if (objectToSpawn != null) {
            GameObject instantiate = Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
            instantiate.name = objectToSpawn.name;

            if (Inventory.instance.inHand == null && objectToSpawn.CompareTag("Prop"))
            {
                Inventory.instance.PickUp(instantiate);
            }
        }
        searched = true;
    }

    public float TimeToSearch()
    {
        if(GotTheItem())
        {
            return timeToSearchObj;
        }
        return timeToSearch;
    }

    public bool KeepTime()
    {
        if (GotTheItem())
        {
            return keepTimeObj;
        }
        return keepTime;
    }

    public bool GotTheItem()
    {
        if(objectToHave != null && Inventory.instance.inHand != null && objectToHave.name == Inventory.instance.inHand.name)
        {
            return true;
        }

        return false;
    }
}
