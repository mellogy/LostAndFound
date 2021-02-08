using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject instructions;

    [SerializeField]
    private GameObject credits;

    private void Start()
    {
        instructions.SetActive(false);
        credits.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "Main");
    }

    public void ShowInstructions()
    {
        instructions.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }
    public void ShowAbout()
    {
        credits.SetActive(true);
    }

    public void HideAbout()
    {
        credits.SetActive(false);
    }
}
