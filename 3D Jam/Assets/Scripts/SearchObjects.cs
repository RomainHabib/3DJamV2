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

        if(!searchingObject.keepTime)
        {
            searchingObject.timeSearched = 0;
        }

        // TODO
        timer.fillAmount = searchingObject.timeSearched / searchingObject.timeToSearch;

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
            Debug.Log(searchingObject);
            searchingObject.timeSearched += refreshTime;

            timer.fillAmount = searchingObject.timeSearched / searchingObject.timeToSearch;

            if (searchingObject.timeSearched >= searchingObject.timeToSearch)
            {
                searchingObject.SpawnItem();

                StopSearching();
            }
        }
    }
}
