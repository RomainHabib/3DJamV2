using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InspectManager : MonoBehaviour
{

    private GameObject player;

    public GameObject inspectHud;
    public GameObject target;

    public Text inspectName;
    public Text inspectDesc;

    private GameObject inspected;

    public GameObject[] toDesactivate;

    public bool inspectCooldown;


    void Start()
    {
        inspectCooldown = false;
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Inspect();
    }

    void Inspect()
    {

        if (player.GetComponent<Inventory>().inHand != null && player.GetComponent<PlayerController>().isInspecting == false && !inspectCooldown)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                StartCoroutine(InspectCooldown());

                player.GetComponent<PlayerController>().isInspecting = true;
                Debug.Log("Inspecting now...");


                player.GetComponent<Inventory>().inHand.GetComponent<Rotation>().enabled = true;

                inspected = Instantiate(player.GetComponent<Inventory>().inHand.gameObject, target.transform.parent);

                if(inspected.GetComponent<ItemDes>().type == ItemDes.PropType.Newpaper)
                {
                    inspected.transform.rotation = Quaternion.Euler(-90, 0, 0);
                }

                inspected.transform.position = target.transform.position;
                inspected.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;


                for (int i = 0; i < toDesactivate.Length; i++)
                {
                    toDesactivate[i].SetActive(false);
                }

                if(inspected.GetComponent<ItemDes>().itemName != null && inspected.GetComponent<ItemDes>().itemDesc != null)
                {
                inspectName.text = inspected.GetComponent<ItemDes>().itemName;
                inspectDesc.text = inspected.GetComponent<ItemDes>().itemDesc;
                }

                inspectHud.SetActive(true);
            }
        }

        if (player.GetComponent<PlayerController>().isInspecting == true && !inspectCooldown)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                player.GetComponent<PlayerController>().isInspecting = false;
                Debug.Log("No longer inspecting");


                player.GetComponent<Inventory>().inHand.GetComponent<Rotation>().enabled = true;

                for (int i = 0; i < toDesactivate.Length; i++)
                {
                    toDesactivate[i].SetActive(true);
                }

                inspectHud.SetActive(false);
                Destroy(inspected);
            }
        }
    }
    public IEnumerator InspectCooldown()
    {
        inspectCooldown = true;
        yield return new WaitForSeconds(0.5f);
        inspectCooldown = false;
    }

}
