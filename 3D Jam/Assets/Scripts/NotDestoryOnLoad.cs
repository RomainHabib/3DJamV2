using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDestoryOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
