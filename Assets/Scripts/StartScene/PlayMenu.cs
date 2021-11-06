using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    public void PlayGame1()
    {
        SceneManager.LoadScene("Minigame1");
    }
    public void PlayGame2()
    {
        SceneManager.LoadScene("Minigame2");
    }
    public void PlayGame3()
    {
        SceneManager.LoadScene("Minigame3");
    }
    public void BackToMainMenu()
    {

    }
}
