using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Step : MonoBehaviour
{
    public bool Ended = false;

    public abstract bool Check();

    public void Reset()
    {
        Ended = false;
    }
}
