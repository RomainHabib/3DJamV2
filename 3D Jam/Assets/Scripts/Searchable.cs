﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchable : MonoBehaviour
{
    [Header("Object")]
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    [Header("Settings")]
    public float timeToSearch;
    public bool keepTime;

    [HideInInspector]
    public bool searched = false;
    [HideInInspector]
    public float timeSearched = 0.0f;

    public void SpawnItem()
    {
        if (objectToSpawn != null) {
            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
        searched = true;
    }
}