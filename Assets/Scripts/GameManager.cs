using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    [Header("Game Manager Variables")]
    public GameObject winScreen;

    public int gameTime = 60;

    public GameObject tutorialPanel;
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;

    [Header("Scoring")]
    public int maxScore = 8;
    protected int currentScore = 0;
    public ScoringController scoring;

    [Header("SFX")]
    public AudioClip refereeClip;
    public AudioClip winClip;
    public AudioClip loseClip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        winScreen.SetActive(false);
        UpdateScoreText();
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        // Show a tutorial
        tutorialPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        tutorialPanel.SetActive(false);

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
    }

    public abstract void StartOtherGameObjects();

    public void AddToScore()
    {
        currentScore++;
        UpdateScoreText();
        if (currentScore >= maxScore)
        {
            EndGame();
        }
    }

    public abstract void UpdateScoreText();

    void EndGame()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        scoring.SetScore(ComputeScore());
        
        audioSource.clip = winClip;
        audioSource.Play();

        DoOnWin();
    }

    public abstract void DoOnWin();

    public int ComputeScore()
    {
        if (currentScore < maxScore / 5) return 1;
        if (currentScore < 2 * maxScore / 5) return 2;
        if (currentScore < 3 * maxScore / 5) return 3;
        if (currentScore < 4 * maxScore / 5) return 4;
        return 5;
    }

    public abstract void ExitToMain();
}
