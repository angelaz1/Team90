using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ChangeEnding : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public bool isPlayerStarted = false;
    public GameObject winScreen;

    private void Start()
    {
        winScreen.SetActive(false);
    }

    void Update()
    {
        if (isPlayerStarted == false && VideoPlayer.isPlaying == true)
        {
            // When the player is started, set this information
            isPlayerStarted = true;
        }
        if (isPlayerStarted == true && VideoPlayer.isPlaying == false)
        {
            // Wehen the player stopped playing, hide it
            VideoPlayer.gameObject.SetActive(false);
            winScreen.SetActive(true);
            //SceneManager.LoadScene("Start");
        }
    }
}
