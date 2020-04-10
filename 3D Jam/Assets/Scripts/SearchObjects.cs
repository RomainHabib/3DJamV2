using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchObjects : MonoBehaviour
{
    public Image timer;

    public float refreshTime;

    private Searchable searchingObject = null;

    public void Search(Searchable targ)
    {
        searchingObject = targ;

        if(!searchingObject.KeepTime())
        {
            searchingObject.timeSearched = 0;
        }

        timer.fillAmount = searchingObject.timeSearched / searchingObject.TimeToSearch();

        timer.gameObject.SetActive(true);

        StartCoroutine("Searching");
    }

    public bool IsSearching()
    {
        if (searchingObject == null)
            return false;

        return true;
    }

    public Searchable GetSearchingObject()
    {
        return searchingObject;
    }

    public void StopSearching()
    {
        searchingObject = null;
        timer.gameObject.SetActive(false);
        StopCoroutine("Searching");
    }

    IEnumerator Searching()
    {
        while (searchingObject != null)
        {
            yield return new WaitForSeconds(refreshTime);
            searchingObject.timeSearched += refreshTime;

            timer.fillAmount = searchingObject.timeSearched / searchingObject.TimeToSearch();

            if (searchingObject.timeSearched >= searchingObject.TimeToSearch())
            {
                searchingObject.SpawnItem();

                StopSearching();
            }
        }
    }
}
