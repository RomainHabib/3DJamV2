using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDes : MonoBehaviour
{
    public enum PropType
    {
        Prop,
        Newpaper,
        Key
    }

    public PropType type;
    public bool suspicious = true;

    public string itemName;
    [TextArea]
    public string itemDesc;
}
