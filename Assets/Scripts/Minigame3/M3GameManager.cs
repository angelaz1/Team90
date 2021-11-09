using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M3GameManager : GameManager
{
    [Header("Minigame 3 Variables")]
    public TextMeshProUGUI totalText;

    //public PrinterController printerController;
    //public PapersController papersController;

    public override void StartOtherGameObjects()
    {
        //printerController.StartSpawningPapers();
    }

    public override void UpdateScoreText()
    {
        totalText.text = $"Greetings Memorized: {currentScore} / {maxScore}";
    }

    public override void DoOnWin()
    {
        //papersController.WinGame();
    }

    public override void ExitToMain()
    {
        GameObject sManagerObj = GameObject.Find("SelectionManager");
        if (sManagerObj)
        {
            sManagerObj.GetComponent<SelectionManager>().FinishMinigame(2, ComputeScore());
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Selection");
    }
}
