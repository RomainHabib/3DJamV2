using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionFeedback : MonoBehaviour
{
    public bool isHovered;

    public Vector3 size;

    [Header("Materials Prefs")]
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material selectMaterial;
    public Sprite inventoryPreview;


    [SerializeField] GameObject player;

    private void Start()
    {
        size = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Selection();
        Hover();
    }

    void Hover()
    {
        if (isHovered)
        {
            transform.GetComponent<Renderer>().material = selectMaterial;
        }

        else if (!isHovered)
        {
            transform.GetComponent<Renderer>().material = normalMaterial;
        }
    }

    //void Selection()
    //{
    //    if(selectionTransform != null)
    //    {
    //        var selectionRenderer = selectionTransform.GetComponent<Renderer>();
    //        selectionRenderer.material = normalMaterial;
    //        selectionTransform = null;
    //    }

    //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if(Physics.Raycast(ray, out hit, rayDistance))
    //    {
    //        var selection = hit.transform;

    //        if (selection.CompareTag(selectableTag) && selection.gameObject != player.GetComponent<Inventory>().inHand)
    //        {
    //            Debug.Log("Facing : " + hit.transform.name);

    //            var selectionRenderer = selection.GetComponent<Renderer>();
    //            if (selectionRenderer != null)
    //            {
    //                selectionRenderer.material = selectMaterial;
    //            }

    //            interactHud.SetActive(true);
    //            interactText.text = "Interact : " + hit.transform.name;
    //            selectionTransform = selection;

    //            if (Input.GetKeyDown(KeyCode.F) && selection.gameObject != player.GetComponent<Inventory>().inHand)
    //            {
    //                player.GetComponent<Inventory>().PickUp(selection.gameObject);
    //            }
    //        }
    //        Debug.DrawLine(ray.origin, hit.point, Color.red);
    //    }
    //    else
    //    {
    //        interactText.text = ""; 
    //        interactHud.SetActive(false);
    //        transform.GetComponent<Renderer>().material = normalMaterial;
    //        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);
    //    }
    //}
}

