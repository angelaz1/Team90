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

    float numCorrect = 0;
    float numTotal = 0;

    public override void StartOtherGameObjects()
    {
        printerController.StartSpawningPapers();
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

    public override void ExitToMain()
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
