using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class M2GameManager : MonoBehaviour
{
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI timerText;
    public GameObject winScreen;

    public int gameTime = 60;

    float numTotal = 0;

    private void Start()
    {
        winScreen.SetActive(false);
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (gameTime > 0)
        { 
            UpdateTimerText();
            yield return new WaitForSeconds(1f);
            gameTime--;
        }
        UpdateTimerText();
        EndGame();
    }

    void EndGame()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    void UpdateTimerText()
    {
        string minutes = string.Format("{0:00}", gameTime / 60);
        string seconds = string.Format("{0:00}", gameTime % 60);
        timerText.text = $"Time: {minutes}:{seconds}";
    }

    public void AddTotal()
    {
        numTotal++;
        UpdateText();
    }

    void UpdateText()
    {
        totalText.text = $"# Boxes Delivered: {numTotal}";
    }
}
