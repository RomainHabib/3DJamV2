using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AchievementsMenu : MonoBehaviour
{
    private List<Achievements> achievements;
    public List<GameObject> achievementsGO;
    public Text descText;
      


    // Start is called before the first frame update
    void Start()
    {
        achievements = new List<Achievements>();
        object[] objects = Resources.LoadAll("Achievements", typeof(Achievements));
        for (int i = 0; i < objects.Length; i++)
        {
            achievements.Add((Achievements)objects[i]);
        }

        descText.text = "";
        for(int i = 0; i < achievements.Count; i++)
        {
            achievementsGO[i].transform.GetChild(0).gameObject.GetComponent<AchievementsBgMenu>().description = achievements[i].description;
            achievementsGO[i].transform.GetChild(2).gameObject.GetComponent<AchievementsTextMenu>().description = achievements[i].description;
            print(achievementsGO[i].transform.GetChild(0).gameObject.GetComponent<AchievementsBgMenu>().description);

            GameObject Image = achievementsGO[i].transform.GetChild(1).gameObject;
            GameObject AchievText = achievementsGO[i].transform.GetChild(2).gameObject;

            Image.GetComponent<Image>().sprite = achievements[i].image;
            AchievText.GetComponent<Text>().text = achievements[i].title;

            if(achievements[i].achieved == true)
            {
                achievementsGO[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().color = new Color32(18,231,0,255);

            }

        }
    }

    // Update is called once per frame





}
