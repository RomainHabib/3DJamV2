using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Inventory : MonoBehaviour
{
   // [SerializeField] public List<GameObject> playerInventory;
   public static Inventory instance;

    [SerializeField] public GameObject playerHand;

    public bool pickupCooldown;

    [Header("Inventory Prefs")]
    public GameObject inHand;
    [SerializeField] private GameObject stockedOne;

    [SerializeField] private Image inventoryTab;
    [SerializeField] private Text inventoryName;

    private float handScale;

    private void Awake()
    {
        instance = this;
    }

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
        toPick.GetComponent<Collider>().enabled = false;

        if(inHand == null)
        {
            handScale = toPick.transform.lossyScale.y;

            inHand = toPick;
            inHand.transform.rotation = Quaternion.identity;
            StartCoroutine(PickupCooldown());
        }

        else if (inHand != null)
        {
            handScale = toPick.transform.lossyScale.y;
            if (stockedOne == null && !pickupCooldown)
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

            inHand.transform.rotation = Quaternion.identity;
            inHand.SetActive(true);

            StartCoroutine(PickupCooldown());
        }
    }

    public void Drop(GameObject toDrop)
    {
        toDrop.transform.parent = GameObject.FindGameObjectWithTag("PropContainer").transform;
        toDrop.transform.position = playerHand.transform.position;
        toDrop.GetComponent<Rigidbody>().velocity = Vector3.zero;
        toDrop.GetComponent<Collider>().enabled = true;
    }

    public void DestroyHand()
    {
        Destroy(inHand);
    }

    //--- Gère le changement de la main à l'inventaire ---//
    void SwapItem()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && (inHand != null || stockedOne != null))
        {
            GameObject temp;

            temp = inHand;
            inHand = stockedOne;
            stockedOne = temp;

            if (stockedOne != null)
            {
                stockedOne.SetActive(false);
            }

            if (inHand != null)
            {
                inHand.SetActive(true);
                ScaleInHand();
            }
        }
    }

    //--- Gère l'affichage dans l'inventaire et dans la main du joueur ---//
    void RefreshInventory()
    {
        if(inHand != null)
        {
            inHand.transform.position = playerHand.transform.position;
            inHand.transform.parent = playerHand.transform;
            //inHand.transform.localScale = inHand.GetComponent<SelectionFeedback>().size;

            inHand.SetActive(true);
        }

        if (stockedOne != null)
        {
            if (stockedOne.GetComponent<SelectionFeedback>() != null)
            {
                inventoryTab.GetComponent<Image>().sprite = stockedOne.GetComponent<SelectionFeedback>().inventoryPreview;
            }
            inventoryName.text = stockedOne.name;
            inventoryTab.gameObject.SetActive(true);
        }
        else if(stockedOne == null)
        {
            inventoryTab.gameObject.SetActive(false);
            inventoryName.text = "";
        }

        if (Input.GetKeyDown(KeyCode.R) && inHand != null && GetComponent<PlayerController>().isInspecting == false)
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

        GetComponent<AudioSound>().PlaySound();

        yield return new WaitForSeconds(1.0f);
        pickupCooldown = false;
    }

    public void ScaleInHand()
    {
        if (inHand != null)
        {
            inHand.transform.localScale = new Vector3(inHand.transform.localScale.x, inHand.transform.localScale.y / (inHand.transform.lossyScale.y / handScale), inHand.transform.localScale.z);
        }
    }
}
