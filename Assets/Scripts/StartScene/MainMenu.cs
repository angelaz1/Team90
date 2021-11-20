using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        GameObject sM = GameObject.Find("SelectionManager");
        if (sM)
        {
            sM.GetComponent<SelectionManager>().ClearMinigames();
        }
    }

    public void GoToPreStory()
    {
        SceneManager.LoadScene("prestory");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
