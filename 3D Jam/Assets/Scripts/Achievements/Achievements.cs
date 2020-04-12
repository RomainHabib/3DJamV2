using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New achievemnt", menuName = "Achievements/Achievement", order = 0)]
public class Achievements : ScriptableObject
{
    public string title;

    [TextArea]
    public string description;

    public Sprite image;
    public bool achieved;
}
