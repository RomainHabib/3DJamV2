using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsMangaer : MonoBehaviour
{
    [Header("Achievements")]
    public List<Achievements> achievements;

    [Header("Settings")]
    public float achievementTime;

    [Header("GameObjects")]
    public Animator parent;
    public Image image;
    public Text text;

    void Start()
    {
        ShowAchievement(achievements[0]);
    }

    public void ShowAchievement(Achievements achievement)
    {
        text.text = achievement.title;
        image.sprite = achievement.image;
        parent.SetBool("Popin", true);
        StartCoroutine("AchievementPopout");
    }

    IEnumerator AchievementPopout()
    {
        yield return new WaitForSeconds(achievementTime);
        Debug.Log("Debug");
        parent.SetBool("Popin", false);
    }
}
