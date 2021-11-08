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

    float numTotal = 0;

    public override void StartOtherGameObjects()
    {
        playerController.AllowMovement();
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

    public override void ExitToMain()
    {
        GameObject sManagerObj = GameObject.Find("SelectionManager");
        if (sManagerObj)
        {
            sManagerObj.GetComponent<SelectionManager>().FinishMinigame(1, 5);
        }
        Time.timeScale = 1;
        SceneManager.LoadScene("Selection");
    }
}
