using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GardeMovement : MonoBehaviour
{
    [Header("Game Design")]
    [Space(10)]
    [Range(0.0f, 10.0f)] public float speed;

    public Transform A, B;
    public bool toA;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void fromAtoB()
    {
        if (toA)
        {
            pos = Vector3.MoveTowards(transform.position, A.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, A.position) < (Vector3.Distance(A.position, B.position) / 100))
            {
                toA = false;
            }
        }
        else
        {
            pos = Vector3.MoveTowards(transform.position, B.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, B.position) < (Vector3.Distance(A.position, B.position) / 100))
            {
                toA = true;
            }
        }

        GetComponent<Rigidbody>().MovePosition(pos);

       
    }
}
