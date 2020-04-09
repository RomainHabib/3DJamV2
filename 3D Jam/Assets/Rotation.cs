using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 15f;
    bool dragging = false;
    Rigidbody rb;

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if(player.GetComponent<PlayerController>().isInspecting == true)
        {
        Rotate();
        }
    }

    void Rotate()
    {
        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }
        if (Input.GetMouseButton(0))
        {
            dragging = true;
        }
    }

    //private void OnMouseDrag()
    //{
    //    dragging = true;
    //}

}
