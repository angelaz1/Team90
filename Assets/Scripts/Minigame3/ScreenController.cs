using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { Up, Down, Left, Right, F, G }

public class ScreenController : MonoBehaviour
{
    public ActionsController screenActionsController;
    public ActionsController playerActionsController;

    public GameObject player1;
    public GameObject player2;

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
        SetAnimatorsToFaceScreen(false);
        screenActionsController.HideActions();
        acceptingInput = true;
    }

    void CreateGreeting()
    {
        currentActions.Clear();
        currentGreeting.Clear();

        for (int i = 0; i < currNumSteps; i++)
        {
            // Balance actions between two players
            int player = Random.Range(0, 2);

            Action randAction;
            if (player == 0) randAction = (Action)Random.Range(0, 4);
            else randAction = (Action)Random.Range(4, 6);

            currentActions.Add(randAction);
            currentGreeting.Add(randAction);
        }

        DisplayGreeting();
        currNumSteps++;
    }

    void DisplayGreeting()
    {
        SetAnimatorsToFaceScreen(true);
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

        if (currentActions[0] == act)
        {
            currentActions.RemoveAt(0);

            // Display correct action
            playerActionsController.ShowCorrectAction(act);
        }
        else
        {
            acceptingInput = false;
            playerActionsController.ShowIncorrectAction(act);

            // TODO: Incorrect animations + sounds

            Invoke(nameof(DisplayGreeting), 1f);
        }

        if (currentActions.Count == 0)
        {
            acceptingInput = false;

            // TODO: Success animations + sounds

            Invoke(nameof(GreetingCompleted), 1f);
        }
    }

    void GreetingCompleted()
    {
        gameManager.AddToScore();
        CreateGreeting();
    }

    void SetAnimatorsToFaceScreen(bool value)
    {
        Camera.main.gameObject.GetComponent<Animator>().SetBool("IsZoomed", value);
        player1.GetComponent<Animator>().SetBool("IsFacingScreen", value);
        player2.GetComponent<Animator>().SetBool("IsFacingScreen", value);
    }
}
