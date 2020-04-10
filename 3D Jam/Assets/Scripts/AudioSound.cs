using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : MonoBehaviour
{
    public AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        if (audio != null)
        {
            audio.Play();
        }
    }
}
