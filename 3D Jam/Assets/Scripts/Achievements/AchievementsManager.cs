using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AchievementsManager : MonoBehaviour
{
    public class Achiev
    {
        public bool achieved;
        public Achievements achiev;

        public Achiev(bool achieved, Achievements achiev) {
            this.achieved = achieved;
            this.achiev = achiev;
        }
    }

    public static AchievementsManager instance;

    [Header("Achievements")]
    private List<Achievements> achievements;
    public AchievementsAdvancement advancement;

    private Dictionary<string, Achiev> achievs;

    [Header("Settings")]
    public float achievementTime;

    [Header("GameObjects")]
    public Animator parent;
    public Image image;
    public Text text;

    private bool cameraDestroyed;
    private bool microDestroyed;

    private void Awake()
    {
        instance = this;

        achievements = new List<Achievements>();
        object[] objects = Resources.LoadAll("Achievements", typeof(Achievements));
        for (int i = 0; i < objects.Length; i++)
        {
            achievements.Add((Achievements)objects[i]);
        }

    }

    void Start()
    {
        achievs = new Dictionary<string, Achiev>();
        for (int i = 0; i < achievements.Count; i++)
        {
            if (achievements[i] != null)
            {
                achievs.Add(achievements[i].title, new Achiev(false, achievements[i]));
            }
        }
    }

    public void ShowAchievement(string name)
    {
        Achiev achi = GetAchievement(name);
        if(achi != null && !achi.achiev.achieved)
        {
            ShowAchievement(achi.achiev);
            achi.achieved = true;
        } else
        {
            Debug.LogError("Achievement unknown : " + name);
        }
    }

    public void ShowAchievement(Achievements achievement)
    {
        achievement.achieved = true;
        text.text = achievement.title;
        image.sprite = achievement.image;
        parent.SetBool("Popin", true);
        StartCoroutine("AchievementPopout");
    }

    IEnumerator AchievementPopout()
    {
        yield return new WaitForSecondsRealtime(achievementTime);
        Debug.Log("Debug");
        parent.SetBool("Popin", false);
    }

    public Achiev GetAchievement(string title)
    {
        if (achievs.ContainsKey(title))
        {
            return achievs[title];
        }
        return null;
    }

    public void DestroyCamera()
    {
        cameraDestroyed = true;

        CameraAndMicroDestroyed();
    }

    public void DestroyMicro()
    {
        microDestroyed = true;

        CameraAndMicroDestroyed();
    }

    public void Win()
    {
        if (CameraAndMicroDestroyed())
        {
            CanvasManager.Instance.setWinMenu();
            ShowAchievement("La grande évasion");
        }
        else
        {
            CanvasManager.Instance.setAlmostWinMenu();
            ShowAchievement("Une évasion presque parfaite");
        }
    }

    private bool CameraAndMicroDestroyed()
    {
        if (microDestroyed && cameraDestroyed)
        {
            ShowAchievement("Je passe dans un tunnel là");
            return true;
        }
        return false;
    }

}
