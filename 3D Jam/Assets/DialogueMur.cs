using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueMur : MonoBehaviour
{
    // Start is called before the first frame update
    public string nomDuDialogue;
    public bool isRunning;
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(Player.transform.position, transform.position) > 3 && isRunning)
        {
            DialogueManager.Instance.DisplayDialogue("Vide");
            isRunning = false;
        }
    }
}
