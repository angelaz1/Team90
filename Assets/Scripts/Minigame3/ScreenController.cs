using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { Up, Down, Left, Right, F, G }

public class ScreenController : MonoBehaviour
{
    public ActionsController screenActionsController;
    public ActionsController playerActionsController;

    public float inBetweenWaitTime = 0.5f;
    public float afterWaitTime = 2f;

    int currNumSteps = 3;
    List<Action> currentActions;
    List<Action> currentGreeting;

    bool keyDown = false;
    bool acceptingInput = false;

    M3GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<M3GameManager>();
        currentActions = new List<Action>();
        currentGreeting = new List<Action>();
    }

    public void StartCreatingGreetings()
    {
        CreateGreeting();
        screenActionsController.AddListenerToSeqActions(FinishedDisplaying);
    }

    void FinishedDisplaying()
    {
        Invoke(nameof(StartListening), afterWaitTime);
    }

    void StartListening()
    {
        screenActionsController.HideActions();
        acceptingInput = true;
    }

    void CreateGreeting()
    {
        currentActions.Clear();
        currentGreeting.Clear();

        for (int i = 0; i < currNumSteps; i++)
        {
            Action randDir = (Action)Random.Range(0, 6);
            currentActions.Add(randDir);
            currentGreeting.Add(randDir);
        }

        DisplayGreeting();
        currNumSteps++;
    }

    void DisplayGreeting()
    {
        screenActionsController.DisplayActionsSequentially(currentGreeting, inBetweenWaitTime);
        playerActionsController.HideActions();
        playerActionsController.SetPlaceholders(currentGreeting.Count);

        currentActions.Clear();
        currentActions.AddRange(currentGreeting);
    }

    void Update()
    {
        if (acceptingInput)
        { 
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") > 0) HandleAction(Action.Up);
                else if (Input.GetAxis("Horizontal") < 0) HandleAction(Action.Left);
                else if (Input.GetAxis("Vertical") < 0) HandleAction(Action.Down);
                else if (Input.GetAxis("Horizontal") > 0) HandleAction(Action.Right);
            }
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) keyDown = false;

            if (Input.GetKeyDown(KeyCode.F)) HandleAction(Action.F);
            else if (Input.GetKeyDown(KeyCode.G)) HandleAction(Action.G);
        }
    }

    void HandleAction(Action act)
    {
        if (currentActions.Count == 0) return;

        keyDown = true;
        Debug.Log(currentActions[0]);

        if (currentActions[0] == act)
        {
            currentActions.RemoveAt(0);

            // Display correct action
            playerActionsController.ShowCorrectAction(act);
        }
        else
        {
            // Incorrect!
            acceptingInput = false;
            playerActionsController.ShowIncorrectAction(act);
            Invoke(nameof(DisplayGreeting), 0.2f);
        }

        if (currentActions.Count == 0)
        {
            acceptingInput = false;
            Invoke(nameof(GreetingCompleted), 0.2f);
        }
    }

    void GreetingCompleted()
    {
        gameManager.AddToScore();
        CreateGreeting();
    }
}
