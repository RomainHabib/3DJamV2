using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform MurGaucheAvant, MurGaucheArriere;
    public float minDistance;
    public bool reset, reset1;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, MurGaucheAvant.position) < minDistance) && reset)
        {

            DialogueManager.Instance.DisplayDialogue("Dialogue_01");
            reset = false;
        }
        else if ((Vector3.Distance(transform.position, MurGaucheAvant.position) > minDistance))
        {
            reset = true;
        }
        

        if ((Vector3.Distance(transform.position, MurGaucheArriere.position) < minDistance) && reset1)
        {

            DialogueManager.Instance.DisplayDialogue("Oklm");
            reset1 = false;
        }
        else if ((Vector3.Distance(transform.position, MurGaucheArriere.position) > minDistance))
        {
            reset1 = true;
        }



    }
}