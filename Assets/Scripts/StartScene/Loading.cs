using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public GameObject IMG_Loading;
    public Slider slider;
    public Text progressText;
    public float playerValue = 0f;
    public string sceneName;

   

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)
           || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            playerValue += 10f;
        }

        if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            playerValue += 10f;
        }

        Debug.Log("playerValue = " + playerValue);
        slider.value = playerValue;
        progressText.text = playerValue + "%";

        if (playerValue == 100)
        {
            LoadScene(sceneName); 
        }


    }

    public void PlayerPrepare(string sceneIndex)
    {
        sceneName = sceneIndex;
        IMG_Loading.SetActive(true);       
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }




}
