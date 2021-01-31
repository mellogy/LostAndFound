using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldController : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI objLeftText;
    [SerializeField]
    private TextMeshProUGUI timeSpentText;

    private List<GameObject> toLayer = new List<GameObject>();
    private GameObject player;
    private int updateInterval = 10;
    private float timer;
    private int objCount;

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

        objCount = GameObject.FindGameObjectsWithTag("LostItem").Length;
        timer += Time.deltaTime;

        if (objCount == 0 )
        {
            print("You win!!");
        }

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

    private void OnGUI()
    {
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer % 60;

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeSpentText.text = string.Concat("Time: ", niceTime);
        objLeftText.text = string.Concat("Lost Objects Left: ", objCount.ToString());

    }
}
