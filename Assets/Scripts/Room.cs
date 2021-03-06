﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] ambientObjects;
    public GameObject[] hidingSpots;
    public GameObject[] NPCObjects;
    private List<Transform> ambientSpawnPoints = new List<Transform>();
    private List<Transform> hidingSpawnPoints = new List<Transform>();
    private List<Transform> npcSpawnPoints = new List<Transform>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "AmbientSpawn")
            {
                ambientSpawnPoints.Add(child);
            }
        }

        foreach (Transform child in ambientSpawnPoints)
        {
            GameObject toSpawn = ambientObjects[Random.Range(0, ambientObjects.Length)];
            if (!Physics2D.OverlapCircle(child.position, 1f))
            {
                Instantiate(toSpawn, child.position, Quaternion.identity);
            }
        }
    }
}
