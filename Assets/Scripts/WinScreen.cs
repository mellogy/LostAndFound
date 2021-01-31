using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeSpentText;

    private float timer;

     void Start()
    {
        timer = Time.time;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "Title");
    }

    private void OnGUI()
    {
        
        float minutes = Mathf.Floor(timer / 60);
        float seconds = timer - (minutes*60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeSpentText.text = string.Concat("You found everything in ", niceTime, "!");

    }
}
