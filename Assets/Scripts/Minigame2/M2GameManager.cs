using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class M2GameManager : GameManager
{
    [Header("Minigame 2 Variables")]
    public TextMeshProUGUI totalText;

    public M2PlayerController playerController;

    public override void StartOtherGameObjects()
    {
        playerController.AllowMovement();
    }

    public override void UpdateScoreText()
    {
        totalText.text = $"Boxes Delivered: {currentScore} / {maxScore}";
    }

    public override void DoOnWin()
    {
        return;
    }

    public override void ExitToMain()
    {
        GameObject sManagerObj = GameObject.Find("SelectionManager");
        if (sManagerObj)
        {
            sManagerObj.GetComponent<SelectionManager>().FinishMinigame(1, ComputeScore());
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Selection");
    }

    
}
