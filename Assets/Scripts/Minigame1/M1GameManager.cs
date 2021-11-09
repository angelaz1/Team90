using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M1GameManager : GameManager
{
    [Header("Minigame 1 Variables")]
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI accuracyText;

    public PrinterController printerController;
    public PapersController papersController;

    public override void StartOtherGameObjects()
    {
        printerController.StartSpawningPapers();
    }

    public override void UpdateScoreText()
    {
        totalText.text = $"Papers Sorted: {currentScore} / {maxScore}";
    }

    public override void DoOnWin()
    {
        papersController.WinGame();
    }

    public override void ExitToMain()
    {
        GameObject sManagerObj = GameObject.Find("SelectionManager");
        if (sManagerObj)
        {
            sManagerObj.GetComponent<SelectionManager>().FinishMinigame(0, ComputeScore());
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Selection");
    }
}
