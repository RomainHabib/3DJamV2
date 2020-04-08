using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    float internTime = 0f;

    [SerializeField] private float countdownTime;
    [SerializeField] private Text countdownText;

    void Start()
    {
        internTime = countdownTime;
    }

    // Update is called once per frame
    void Update()
    {
        CountdownUpdate();
    }

    void CountdownUpdate()
    {
        internTime -= 1 * Time.deltaTime;
        countdownText.text = internTime.ToString("0");

        if(internTime <= 0)
        {
            internTime = 0;
        }

    }
}
