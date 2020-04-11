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
    [SerializeField] private string searchableTag = "Searchable";
    [SerializeField] private string listenTag = "Listenable";
    [SerializeField] private RaycastHit lastRay;

    [Header("Interact Prefs")]
    [SerializeField] private Text interactText;
    [SerializeField] private GameObject interactHud;

    [HideInInspector] public bool visionLock = false;

    private void Start()
    {
        //--- Pour lock le curseur dans l'écran ---//
        if (lockMouse && !playerBody.GetComponent<PlayerController>().isInspecting)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        interactHud.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!playerBody.GetComponent<PlayerController>().isInspecting || visionLock == true)
        {
            Look();
            LineVision();
        }
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
                if (lastRay.transform != null && lastRay.transform.GetComponent<SelectionFeedback>() != null)
                {
                    lastRay.transform.GetComponent<SelectionFeedback>().isHovered = false;
                }
                lastRay = hit;

                if (hit.transform.GetComponent<SelectionFeedback>() != null)
                {
                    hit.transform.GetComponent<SelectionFeedback>().isHovered = true;
                }

                Debug.Log("Facing : " + hit.transform.name);

                ShowInteractHUD("Interact : " + hit.transform.name);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    playerBody.GetComponent<Inventory>().PickUp(selection.gameObject);
                }
            } else if (selection.CompareTag(searchableTag))
            {
                SearchObjects playerSearch = playerBody.GetComponent<SearchObjects>();
                Searchable searchObject = selection.gameObject.GetComponentInParent<Searchable>();


                if (searchObject.searched == false)
                {
                    ShowInteractHUD("Search : " + searchObject.gameObject.name);
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        var searchSound = searchObject.GetComponent<AudioSound>();

                        if(searchSound != null)
                        {
                        searchObject.GetComponent<AudioSound>().PlaySound();
                        }

                        playerSearch.Search(searchObject);
                    }
                } else if (IsInteractHUDActive())
                {
                    HideInteractHUD();
                }

            }
            else if (selection.CompareTag(listenTag))
            {
                LaunchDialogue(selection);
            }

            else
            {
                if (IsInteractHUDActive())
                {
                    HideInteractHUD();
                }
            }

            // Si on ne regarde plus l'objet qu'on cherche
            if (playerBody.GetComponent<SearchObjects>().IsSearching() &&
                playerBody.GetComponent<SearchObjects>().GetSearchingObject() != selection.gameObject.GetComponentInParent<Searchable>())
            {
                playerBody.GetComponent<SearchObjects>().StopSearching();
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }

        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayDistance, Color.green);

            if (playerBody.GetComponent<SearchObjects>().IsSearching())
            {
                playerBody.GetComponent<SearchObjects>().StopSearching();
            }

            if (lastRay.transform != null && lastRay.transform.GetComponent<SelectionFeedback>() != null)
            {
                lastRay.transform.GetComponent<SelectionFeedback>().isHovered = false;
            }

            HideInteractHUD();
        }
    }

    private void LaunchDialogue(Transform selection)
    {
        if(selection.GetComponent<DialogueMur>().isRunning == true)
        {
            //HideInteractHUD();
            // return;
            ShowInteractHUD("Next Dialogue");

            if(DialogueManager.Instance.textSO.nextDialogue == null)
            {
                HideInteractHUD();
                return;
            }


            if (Input.GetKeyDown(KeyCode.F))
            {

                DialogueManager.Instance.DisplayDialogue(DialogueManager.Instance.textSO.nextDialogue.name);
                selection.GetComponent<DialogueMur>().isRunning = true;
            }
        }
        else
        {
            ShowInteractHUD("Listen Dialogue");
            if (Input.GetKeyDown(KeyCode.F))
            {
                DialogueManager.Instance.DisplayDialogue(selection.GetComponent<DialogueMur>().nomDuDialogue);
                selection.GetComponent<DialogueMur>().isRunning = true;

            }
        }
        
        
        // Lancer le dialogue
    }

    private void ShowInteractHUD(string text)
    {
        interactHud.SetActive(true);
        interactText.text = text;
    }

    private void HideInteractHUD()
    {
        interactHud.SetActive(false);
    }

    private bool IsInteractHUDActive()
    {
        return interactHud.activeSelf;
    }
}
