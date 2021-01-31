using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private int worldWidth = 24;
    [SerializeField]
    private int worldHeight = 24;
    [SerializeField]
    private int roomWidth = 20;
    [SerializeField]
    private int roomHeight = 20;
    [SerializeField]
    private GameObject[] roomTemplates;
    [SerializeField]
    private GameObject[] ambientObjects;
    [SerializeField]
    private GameObject[] hidingSpots;
    [SerializeField]
    private GameObject[] NPCObjects;

    void Start()
    {
        for (int x = -worldWidth / 2; x < worldWidth / 2; x++)
        {
            for (int y = -worldHeight / 2; y < worldHeight / 2; y++)
            {
                GameObject roomToSpawn = roomTemplates[Random.Range(0, roomTemplates.Length)];
                Room newRoom = Instantiate(roomToSpawn, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity).GetComponent<Room>();
                newRoom.ambientObjects = ambientObjects;
                newRoom.hidingSpots = hidingSpots;
                newRoom.NPCObjects = NPCObjects;
            }
        }
    }
}
