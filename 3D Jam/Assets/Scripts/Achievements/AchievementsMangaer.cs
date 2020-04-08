using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsMangaer : MonoBehaviour
{
    [Header("Achievements")]
    public List<Achievements> achievements;

    [Header("GameObjects")]
    public GameObject parent;
    public Image image;
    public Text text;

    void Start()
    {
        ShowAchievement(achievements[0]);
    }

    void Update()
    {
        
    }

    public void ShowAchievement(Achievements achievement)
    {
        text.text = achievement.title;
        image.sprite = achievement.image;
        parent.SetActive(true);
    }
}
