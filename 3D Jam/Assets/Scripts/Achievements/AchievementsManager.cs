﻿using System.Collections;
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
    public List<Achievements> achievements;

    private Dictionary<string, Achiev> achievs;

    [Header("Settings")]
    public float achievementTime;

    [Header("GameObjects")]
    public Animator parent;
    public Image image;
    public Text text;

    private void Awake()
    {
        instance = this;
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
        if(achi != null && !achi.achieved)
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
}
