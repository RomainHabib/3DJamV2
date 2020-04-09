using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New achievemnt", menuName = "Achievement", order = 0)]
public class Achievements : ScriptableObject
{
    public string title;
    public string description;

    public Sprite image;
}
