using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public Image p1Check;
    public Image p2Check;

    Color checkDefaultColor = new Color(255, 255, 255, 40);

    bool p1keyDown = false;
    bool p2keyDown = false;

    public TextMeshProUGUI teamNamesText;
    public TextMeshProUGUI teamTimesText;

    void Update()
    {
        if (!p1keyDown)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                p1keyDown = true;
            }
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            p1keyDown = false;
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.G))
        {
            p2keyDown = true;
        }
        else 
        {
            p2keyDown = false;
        }

        if (p1keyDown && p2keyDown) 
        {
            // Play a sound
            Invoke(nameof(ReturnToSelection), 0.5f);
        }

        UpdateChecksUI();
    }

    void UpdateChecksUI()
    {
        p1Check.color = p1keyDown ? Color.green : checkDefaultColor;
        p2Check.color = p2keyDown ? Color.green : checkDefaultColor;
    }

    void ReturnToSelection()
    {
        SceneManager.LoadScene("Selection");
    }

    public void SetLeaderboardText(LeaderboardInfo info)
    {
        teamNamesText.text = ""; teamTimesText.text = "";

        for (int i = 0; i < info.maxGrab; i++)
        {
            teamNamesText.text += info.teamNames[i] + "\n";
            teamTimesText.text += (1000 - info.teamTimes[i]) + "\n";
        }
    }
}
