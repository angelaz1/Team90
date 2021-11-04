using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI accuracyText;

    float numCorrect = 0;
    float numTotal = 0;

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
}
