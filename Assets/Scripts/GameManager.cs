using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    [Header("Game Manager Variables")]
    public TextMeshProUGUI timerText;
    public GameObject winScreen;

    public int gameTime = 60;

    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;

    [Header("SFX")]
    public AudioClip refereeClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        winScreen.SetActive(false);
        UpdateTimerText();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // Show a tutorial

        // Start countdown
        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "START!!";
        audioSource.clip = refereeClip;
        audioSource.Play();
        yield return new WaitForSeconds(1f);

        countdownText.text = "";
        countdownPanel.SetActive(false);

        StartOtherGameObjects();
        StartCoroutine(StartTimer());
    }

    public abstract void StartOtherGameObjects();

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

        // TODO: play clip based on score
        audioSource.clip = winClip;
        audioSource.Play();
    }

    void UpdateTimerText()
    {
        string minutes = string.Format("{0:00}", gameTime / 60);
        string seconds = string.Format("{0:00}", gameTime % 60);
        timerText.text = $"Time: {minutes}:{seconds}";
    }

    public abstract void ExitToMain();
}
