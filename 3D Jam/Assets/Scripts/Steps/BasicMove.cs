using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Rigidbody rgBd = GetComponent<Rigidbody>();
        if (Input.GetAxis("Horizontal") != 0)
        {
            rgBd.MovePosition(new Vector3(rgBd.position.x, rgBd.position.y, rgBd.position.z + 0.1f * Input.GetAxisRaw("Horizontal")));
        }
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    rgBd.MovePosition(new Vector3(rgBd.position.x, rgBd.position.y, rgBd.position.z - 0.1f));
        //}
        if (Input.GetAxis("Vertical") != 0)
        {
            rgBd.MovePosition(new Vector3(rgBd.position.x + 0.1f * Input.GetAxisRaw("Vertical"), rgBd.position.y, rgBd.position.z));
        }
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    rgBd.MovePosition(new Vector3(rgBd.position.x + 0.1f, rgBd.position.y, rgBd.position.z));
        //}
    }

    public void Debugger(string text)
    {
        Debug.Log(text);
    }
}
