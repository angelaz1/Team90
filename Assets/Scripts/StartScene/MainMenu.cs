using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject inputScreen;
    public GameObject playerSelection;
    public TMP_InputField teamNameInput;

    SelectionManager selectionManager;

    private void Start()
    {
        inputScreen.SetActive(false);

        selectionManager = GameObject.Find("SelectionManager").GetComponent<SelectionManager>();
        selectionManager.ClearMinigames();
    }

    public void ShowInput()
    {
        inputScreen.SetActive(true);
        playerSelection.SetActive(false);
    }

    public void GoToPreStory()
    {
        selectionManager.SetTeamName(teamNameInput.text);
        SceneManager.LoadScene("prestory");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
