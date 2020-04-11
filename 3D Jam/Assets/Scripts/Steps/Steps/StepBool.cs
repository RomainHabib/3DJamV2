using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBool : Step
{
    public bool verified;

    public override bool Check()
    {
        if (verified)
        {
            Ended = true;
            return true;
        }
        return false;
    }

    public void True()
    {
        verified = true;
    }

    public void False()
    {
        verified = false;
    }
}
