using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public float rayMaxDistance;
    public float leftOff = -35f;
    public float rightOff = 35f;

    void Update()
    {
        UpdateRay();
    }

    void UpdateRay()
    {
        Vector3 defaultAngle = transform.forward;

        //--- Pour calculer les angles suivants un offset ---//
        Quaternion spreadAngleLeft = Quaternion.AngleAxis(leftOff, new Vector3(0, 1, 0));
        Quaternion spreadAngleRight = Quaternion.AngleAxis(rightOff, new Vector3(0, 1, 0));
        Vector3 leftAngle = spreadAngleLeft * defaultAngle;
        Vector3 rightAngle = spreadAngleRight * defaultAngle;

        Ray ray = new Ray(transform.position, transform.forward);
        Ray leftRay = new Ray(transform.position, leftAngle);
        Ray rightRay = new Ray(transform.position, rightAngle);


        RaycastHit hitInfo;

        //--- Conditions semblables à tous les raycast ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance) || Physics.Raycast(leftRay, out hitInfo, rayMaxDistance) || Physics.Raycast(rightRay, out hitInfo, rayMaxDistance))
        {
        }

        //--- Pour le raycast de face ---//
        if (Physics.Raycast(ray, out hitInfo, rayMaxDistance))
        {
            //--- Si l'objet dans le Ray est un Prop ---//
            if (hitInfo.transform.CompareTag("Prop"))
            {
                //hitInfo.transform.GetComponent<Rigidbody>().AddForce(0,0,100);
                Debug.Log("Facing : " + hitInfo.transform.name);
            }

            Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayMaxDistance, Color.green);
        }

        /*
        //--- Pour le raycast de gauche ---//
        if (Physics.Raycast(leftRay, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(leftRay.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * rayMaxDistance, Color.green);
        }

        //--- Pour le raycast de droite ---//
        if (Physics.Raycast(rightRay, out hitInfo, rayMaxDistance))
        {
            Debug.DrawLine(rightRay.origin, hitInfo.point, Color.red);
        }
        else
        {
            Debug.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * rayMaxDistance, Color.green);
        }
        */
    }
}
