using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    
    private List<GameObject> toLayer = new List<GameObject>();
    private GameObject player;
    private int updateInterval = 10;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        getLayeredObjects();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % updateInterval == 0)
        {
            getLayeredObjects();
        }
        updateLayeredSprites();
    }

    void getLayeredObjects()
    {
        toLayer.Clear(); //empty the list first
        GameObject[] ambient = GameObject.FindGameObjectsWithTag("Ambient");
        foreach (GameObject obj in ambient)
        {
            toLayer.Add(obj);
        }
    }

    void updateLayeredSprites()
    {
        foreach (GameObject obj in toLayer)
        {
            float py = player.transform.position.y;
            float oy = obj.transform.position.y;
            SpriteRenderer[] sprites = obj.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spr in sprites) 
            {
                if (py<oy)
                {
                    //player is behind it
                    spr.sortingOrder = -Mathf.Abs(spr.sortingOrder); //I know this looks weird but it makes sure the sorting order is negative
                }
                else
                {
                    spr.sortingOrder = Mathf.Abs(spr.sortingOrder);
                }
            }
        }
    }
}
