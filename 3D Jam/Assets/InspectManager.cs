using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectManager : MonoBehaviour
{

    private GameObject player;

    public GameObject inspectHud;
    public GameObject target;

    public GameObject inspected;


    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Inspect();
    }

    void Inspect()
    {

        if(player.GetComponent<Inventory>().inHand != null && player.GetComponent<PlayerController>().isInspecting == false)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                player.GetComponent<PlayerController>().isInspecting = true;
                Debug.Log("Inspecting now...");

                inspectHud.SetActive(true);

                player.GetComponent<Inventory>().inHand.GetComponent<Rotation>().enabled = true;

                inspected = Instantiate(player.GetComponent<Inventory>().inHand.gameObject, target.transform.position, Quaternion.identity);
                inspected.transform.position = target.transform.position;
                inspected.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
        }

        if(player.GetComponent<PlayerController>().isInspecting == true)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                player.GetComponent<PlayerController>().isInspecting = false;
                Debug.Log("No longer inspecting");
            }
        }
    }

}
