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

    //float numCorrect = 0;
    //int numTotal = 0;

    public override void StartOtherGameObjects()
    {
        printerController.StartSpawningPapers();
    }

    public void AddCorrect()
    {
        //numCorrect++;
        currentScore++;
        UpdateText();
    }

    public void AddIncorrect()
    {
        //numTotal++;
        UpdateText();
    }

    void UpdateText()
    {
        totalText.text = $"# Papers Sorted: {currentScore}";
        //string accuracyStr = numTotal == 0 ? "0.00" : string.Format("{0:0.00}", numCorrect / numTotal * 100f);
        //accuracyText.text = $"Accuracy: {accuracyStr}%";
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
