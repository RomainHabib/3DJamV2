using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public void Play(string trigger)
    {
        Debug.Log("Animmmmmmmmm");
        GetComponent<Animator>().SetTrigger(trigger);
        Debug.Log("Animmmmmmmmm x2");
    }
}
