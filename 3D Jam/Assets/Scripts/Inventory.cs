using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
   // [SerializeField] public List<GameObject> playerInventory;
    [SerializeField] public GameObject playerHand;

    public bool pickupCooldown;

    [Header("Inventory Prefs")]
    public GameObject inHand;
    [SerializeField] private GameObject stockedOne;

    [SerializeField] private Image inventoryTab;
    [SerializeField] private Text inventoryName;


    // Start is called before the first frame update
    void Start()
    {
        pickupCooldown = false;
        inventoryTab.gameObject.SetActive(false);
        inventoryName.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        RefreshInventory();
        SwapItem();
    }

    public void PickUp(GameObject toPick)
    {
        if(inHand == null)
        {
            inHand = toPick;
            StartCoroutine(PickupCooldown());
        }

        else if (inHand != null)
        {

            if(stockedOne == null && !pickupCooldown)
            {
                stockedOne = inHand;
                inHand = toPick;
                stockedOne.SetActive(false);
            }
            else if(stockedOne != null)
            {
                Drop(inHand);
                inHand = toPick;
            }

            inHand.SetActive(true);

            StartCoroutine(PickupCooldown());
        }
    }

    public void Drop(GameObject toDrop)
    {
        toDrop.transform.parent = GameObject.FindGameObjectWithTag("PropContainer").transform;
        toDrop.transform.position = new Vector3(playerHand.transform.position.x, 2, playerHand.transform.forward.z - 0.3f);
    }

    //--- Gère le changement de la main à l'inventaire ---//
    void SwapItem()
    {
        GameObject temp;

        if (Input.GetKeyDown(KeyCode.Tab) && stockedOne != null)
        {
        temp = inHand;
        inHand = stockedOne;
        stockedOne = temp;

        stockedOne.SetActive(false);
        inHand.SetActive(true);
        }
    }

    //--- Gère l'affichage dans l'inventaire et dans la main du joueur ---//
    void RefreshInventory()
    {
        if(inHand != null)
        {
            inHand.transform.position = playerHand.transform.position;
            inHand.transform.parent = playerHand.transform;
            inHand.transform.rotation = Quaternion.identity;
            inHand.transform.localScale = inHand.GetComponent<SelectionFeedback>().size;

            inHand.SetActive(true);
        }

        if (stockedOne != null)
        {
            inventoryTab.GetComponent<Image>().sprite = stockedOne.GetComponent<SelectionFeedback>().inventoryPreview;
            inventoryName.text = stockedOne.name;
            inventoryTab.gameObject.SetActive(true);
        }
        else if(stockedOne == null)
        {
            inventoryTab.gameObject.SetActive(false);
            inventoryName.text = "";
        }

        if (Input.GetKeyDown(KeyCode.R) && inHand != null)
        {
            Drop(inHand);
            inHand = stockedOne;
            if(stockedOne != null)
            {
            stockedOne = null;
            }
        }
    }

    public IEnumerator PickupCooldown()
    {
        pickupCooldown = true;
        yield return new WaitForSeconds(1.0f);
        pickupCooldown = false;
    }
}
