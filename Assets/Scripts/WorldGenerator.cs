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
    [SerializeField]
    private GameObject[] hiddenObjects;
    [SerializeField]
    private GameObject worldBorder;

    private int hiddenOdds = 2;

    void Start()
    {
        for (int x = -worldWidth / 2 - 1; x < worldWidth / 2 + 2; x++)
        {
            for (int y = -worldHeight / 2 - 1; y < worldHeight / 2 + 2; y++)
            {
                GameObject roomToSpawn;

                if (x== -worldWidth / 2 - 1 || x== worldWidth / 2 + 1 || y== -worldHeight / 2 - 1 || y== worldHeight / 2 + 1)
                {
                    roomToSpawn = worldBorder;
                }
                else
                {
                    roomToSpawn = roomTemplates[Random.Range(0, roomTemplates.Length)];
                }
                
                Room newRoom = Instantiate(roomToSpawn, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity).GetComponent<Room>();
                newRoom.ambientObjects = ambientObjects;
            }
        }

        foreach (GameObject obj in hiddenObjects) {
            int rndX = Random.Range(-(worldWidth / 2 * roomWidth), worldWidth / 2 * roomWidth);
            int rndY = Random.Range(-(worldHeight / 2 * roomHeight), worldHeight / 2 * roomHeight);
            //make sure this object doesn't overlap with anything else
            while (Physics2D.OverlapCircle(new Vector2(rndX, rndY), 1.5f))
            {
                rndX = Random.Range(-(worldWidth / 2 * roomWidth), worldWidth / 2 * roomWidth);
                rndY = Random.Range(-(worldHeight / 2 * roomHeight), worldHeight / 2 * roomHeight);
            }

            GameObject NPC = Instantiate(
                NPCObjects[Random.Range(0, NPCObjects.Length)],
                new Vector3(rndX, rndY, 0),
                Quaternion.identity
                );

            rndX = Random.Range(-(worldWidth / 2 * roomWidth), worldWidth / 2 * roomWidth);
            rndY = Random.Range(-(worldHeight / 2 * roomHeight), worldHeight / 2 * roomHeight);

            //make sure this object doesn't overlap with anything else
            while (Physics2D.OverlapCircle(new Vector2(rndX, rndY), 1.5f))
            {
                rndX = Random.Range(-(worldWidth / 2 * roomWidth), worldWidth / 2 * roomWidth);
                rndY = Random.Range(-(worldHeight / 2 * roomHeight), worldHeight / 2 * roomHeight);
            }

            GameObject hiddenObject = Instantiate(
                obj,
                new Vector3(rndX, rndY, 0),
                Quaternion.identity
                );

            NPC.GetComponent<NPCController>().myObject = hiddenObject;
            hiddenObject.GetComponent<LostItem>().owner = NPC;

            //roll a dice to decide if this object will be hidden or not
            int d = Random.Range(0, hiddenOdds + 1);
            if (d==hiddenOdds)
            {
                GameObject hidingSpot = hidingSpots[Random.Range(0, hidingSpots.Length)];
                GameObject h = Instantiate(hidingSpot, new Vector3(rndX, rndY, 0), Quaternion.identity);
                h.GetComponentInChildren<SpriteRenderer>().sortingOrder = 12;
            }
        }
    }
}
