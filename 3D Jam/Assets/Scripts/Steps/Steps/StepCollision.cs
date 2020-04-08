using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCollision : Step
{
    public Collider targetCollider;

    private bool isColliding = false;

    public override bool Check()
    {
        if (isColliding)
        {
            Ended = true;
            return true;
        }

        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == targetCollider.gameObject)
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == targetCollider.gameObject)
        {
            isColliding = false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == targetCollider.gameObject)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject == targetCollider.gameObject)
        {
            isColliding = false;
        }
    }
}
