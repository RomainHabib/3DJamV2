using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemDes : MonoBehaviour
{ 
    public enum PropType
    {
        Newpaper,
        Prop,
        Key
    }

    public PropType type;

    public string itemName;
    [TextArea]
    public string itemDesc;

}
