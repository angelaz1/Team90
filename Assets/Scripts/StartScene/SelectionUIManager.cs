using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionUIManager : MonoBehaviour
{
    public Button[] minigameButtons;
    public GameObject continueButton;

    SelectionManager selectionManager;

    void Start()
    {
        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        int numMinigames = selectionManager.GetNumMinigames();

        for (int i = 0; i < minigameButtons.Length; i++)
        {
            minigameButtons[i].interactable = i < numMinigames;
        }

        //TODO: Show scores

        continueButton.SetActive(selectionManager.AllMinigamesCompleted());
    }

}
