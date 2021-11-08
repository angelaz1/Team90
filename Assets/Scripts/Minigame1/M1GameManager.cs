using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M1GameManager : MonoBehaviour
{
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI timerText;
    public GameObject winScreen;

    public int gameTime = 60;

    float numCorrect = 0;
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

    public void AddCorrect()
    {
        numCorrect++;
        numTotal++;
        UpdateText();
    }

    public void AddIncorrect()
    {
        numTotal++;
        UpdateText();
    }

    void UpdateText()
    {
        totalText.text = $"# Papers Classified: {numTotal}";
        string accuracyStr = numTotal == 0 ? "0.00" : string.Format("{0:0.00}", numCorrect / numTotal * 100f);
        accuracyText.text = $"Accuracy: {accuracyStr}%";
    }

    public void ExitToMain()
    {
        GameObject sManagerObj = GameObject.Find("SelectionManager");
        if (sManagerObj)
        {
            sManagerObj.GetComponent<SelectionManager>().FinishMinigame(0, 5);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Selection");
    }
}
