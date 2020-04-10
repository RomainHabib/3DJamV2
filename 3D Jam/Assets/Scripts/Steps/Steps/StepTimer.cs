using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTimer : Step
{
    public bool verified;
    public float time;

    private void OnEnable()
    {
        StartCoroutine("StartTimer");
        Debug.Log("Explosion imminente");
    }

    public override bool Check()
    {
        if (verified)
        {
            Ended = true;
            return true;
        }
        return false;
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(time);
        verified = true;
    }

}
