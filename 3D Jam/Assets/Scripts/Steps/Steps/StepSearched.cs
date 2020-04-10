using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSearched : Step
{
    public override bool Check()
    {
        if (GetComponent<Searchable>() != null && GetComponent<Searchable>().searched)
        {
            Ended = true;
            return true;
        }
        return false;
    }
}
