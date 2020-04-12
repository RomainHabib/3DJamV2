using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New achievement advancement", menuName = "Achievements/Achievement advancement", order = 0)]
public class AchievementsAdvancement : ScriptableObject
{
    public class ObjectToFind
    {
        public bool found;
        public GameObject gameObject;

        public ObjectToFind(GameObject gameObject, bool found)
        {
            this.gameObject = gameObject;
            this.found = found;
        }
    }

    public int caughtCounter;

    public List<ObjectToFind> props;

    public List<ObjectToFind> dialogues;

    private void Awake()
    {
        if (props == null)
        {
            props = new List<ObjectToFind>();
        }

        GameObject[] objectList = GameObject.FindGameObjectsWithTag("Prop");
        for (int i = 0; i < objectList.Length; i++)
        {
            if(ObjectSearch(props, objectList[i]) != -1) {
                props.Add(new ObjectToFind(objectList[i], false));
            }
        }

        // ----

        if (dialogues == null)
        {
            dialogues = new List<ObjectToFind>();
        }

        objectList = GameObject.FindGameObjectsWithTag("Listenable");
        for (int i = 0; i < objectList.Length; i++)
        {
            if (ObjectSearch(dialogues, objectList[i]) != -1)
            {
                dialogues.Add(new ObjectToFind(objectList[i], false));
            }
        }
    }

    public void ObjectFound(List<ObjectToFind> list, GameObject prop)
    {
        int ObjectId = ObjectSearch(list, prop);

        if (ObjectId != -1 && !list[ObjectId].found)
        {
            list[ObjectId].found = true;

            VerifyDialogues();
            VerifyProps();
        }
    }

    public void Caught()
    {
        caughtCounter++;

        switch(caughtCounter) {
            case 1:
                AchievementsManager.instance.ShowAchievement("Spotted");
                break;
            case 5:
                AchievementsManager.instance.ShowAchievement("Loin derrière");
                break;
            case 10:
                AchievementsManager.instance.ShowAchievement("T'es pas net Baptiste");
                break;
            case 20:
                AchievementsManager.instance.ShowAchievement("Are u even trying ?");
                break;
        }
    }

    private void VerifyProps()
    {
        for (int i = 0; i < props.Count; i++)
        {
            if(!props[i].found)
            {
                return;
            }
        }

        AchievementsManager.instance.ShowAchievement("C'est tombé du camion");
    }

    private void VerifyDialogues()
    {
        for (int i = 0; i < dialogues.Count; i++)
        {
            if (!dialogues[i].found)
            {
                return;
            }
        }

        AchievementsManager.instance.ShowAchievement("Commère professionnel");
    }

    private int ObjectSearch(List<ObjectToFind> list, GameObject obj)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].gameObject == obj)
            {
                return i;
            }
        }
        return -1;
    }

}
