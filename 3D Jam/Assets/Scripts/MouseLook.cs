using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;

    public bool lockMouse;

    [Header("Vision Prefs")]
    [SerializeField] private float rayDistance;
    [SerializeField] private string selectableTag = "Prop";
    [SerializeField] private RaycastHit lastRay;

    [Header("Interact Prefs")]
    [SerializeField] private Text interactText;
    [SerializeField] private GameObject interactHud;

    private void Start()
    {
        //--- Pour lock le curseur dans l'écran ---//
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        interactHud.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        Look();
        LineVision();   
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void LineVision()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            var selection = hit.transform;

            if (selection.CompareTag(selectableTag) && selection.gameObject != playerBody.GetComponent<Inventory>().inHand)
            {
                if (lastRay.transform != null)
                {
                    lastRay.transform.GetComponent<SelectionFeedback>().isHovered = false;
                }
                lastRay = hit;

                hit.transform.GetComponent<SelectionFeedback>().isHovered = true;

                Debug.Log("Facing : " + hit.transform.name);

                interactHud.SetActive(true);
                interactText.text = "Interact : " + hit.transform.name;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    playerBody.GetComponent<Inventory>().PickUp(selection.gameObject);
                }
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);

            if (lastRay.transform != null)
            {
                lastRay.transform.GetComponent<SelectionFeedback>().isHovered = false;
            }

            interactText.text = "";
            interactHud.SetActive(false);
        }
    }
}
