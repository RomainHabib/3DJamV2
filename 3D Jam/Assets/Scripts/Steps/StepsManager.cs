using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StepsManager : MonoBehaviour
{
    public bool debugger;

    [System.Serializable]
    public class StepList
    {
        public string name;
        public List<Step> steps;
        public List<UnityEvent> callbacks;
    }

    [SerializeField]
    public List<StepList> steps;


    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < steps.Count; i++)
        {
            CheckSteps(i);
        }
    }

    private void CheckSteps(int stepsIndex)
    {
        for (int i = 0; i < steps[stepsIndex].steps.Count; i++)
        {
            if (!steps[stepsIndex].steps[i].Ended) // Si la steps n'est pas finie
            {
                if (steps[stepsIndex].steps[i].Check()) // Verifie si elle est finie
                {
                    steps[stepsIndex].callbacks[i].Invoke();
                }
                return;
            }
        }
    }

}
