using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    bool dragging = false;
    Rigidbody rb;

    public GameObject targetObject;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();       
    }

    private void FixedUpdate()
    {
        Rotate();
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
    }

    //void ChangeObject()
    //{
    //    if(targetObject != null)
    //    {
    //        if (Input.GetKeyDown(KeyCode.T))
    //        {
    //            this.gameObject = targetObject.gameObject;
    //        }
    //    }
    //}

    private void OnMouseDrag()
    {
        dragging = true;
    }

}
