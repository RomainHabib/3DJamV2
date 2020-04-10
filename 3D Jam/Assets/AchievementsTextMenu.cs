using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementsTextMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public string description;
    public Text descText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message

        // print("mouse over");
    }

    public void OnPointerEnter(PointerEventData data)
    {
        descText.text = description;
    }
    public void OnPointerExit(PointerEventData data)
    {
        // descText.text = "";
    }
}
