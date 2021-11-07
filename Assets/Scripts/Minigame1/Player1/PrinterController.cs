using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }

public class PrinterController : MonoBehaviour
{
    public GameObject paperObject;
    public float spawnTimeMin;
    public float spawnTimeMax;

    public ActionsController actionsController;
    public PapersController papersController;

    List<Direction> currentActions = new List<Direction>(); // List of directions that need to be fulfilled to do current action
    List<Direction> passPaperActions = new List<Direction>();

    bool paperSpawned = false;
    bool printerBroken = false;
    bool keyDown = false;

    int minSteps = 3;
    int maxSteps = 7;
    int currMaxSteps;

    void Start()
    {
        passPaperActions.Add(Direction.Up);
        passPaperActions.Add(Direction.Right);
        currMaxSteps = minSteps + 1;

        StartCoroutine(SpawnPapers());
    }

    IEnumerator SpawnPapers()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));
            int rng = Random.Range(0, 10);
            if (rng > 8)
            {
                BreakPrinter();
            }
            else
            {
                SpawnPaper();
            }

            while (paperSpawned || printerBroken)
            { 
                yield return null;
            }
        }
    }

    void SpawnPaper()
    {
        paperObject.SetActive(true);
        paperSpawned = true;

        //currentActions.AddRange(passPaperActions);
        Direction randDir = (Direction)Random.Range(0, 4);
        currentActions.Add(randDir);
        currentActions.Add(Direction.Right);

        // Display actions
        actionsController.DisplayActions(currentActions);
    }

    void BreakPrinter()
    {
        printerBroken = true;

        int numActions = Random.Range(minSteps, currMaxSteps);
        for (int i = 0; i < numActions; i++)
        {
            Direction randDir = (Direction)Random.Range(0, 4);
            currentActions.Add(randDir);
        }
        currMaxSteps = Mathf.Min(currMaxSteps + 1, maxSteps);

        // Display actions
        actionsController.DisplayActions(currentActions);
    }

    // Update is called once per frame
    void Update()
    {
        if (paperSpawned || printerBroken)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") > 0) HandleAction(Direction.Up);
                else if (Input.GetAxis("Horizontal") < 0) HandleAction(Direction.Left);
                else if (Input.GetAxis("Vertical") < 0) HandleAction(Direction.Down);
                else if (Input.GetAxis("Horizontal") > 0) HandleAction(Direction.Right);
            }
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) keyDown = false;

            //if (Input.GetKeyDown(KeyCode.W)) HandleAction(Direction.Up);
            //else if (Input.GetKeyDown(KeyCode.A)) HandleAction(Direction.Left);
            //else if (Input.GetKeyDown(KeyCode.S)) HandleAction(Direction.Down);
            //else if (Input.GetKeyDown(KeyCode.D)) HandleAction(Direction.Right);
        }
    }

    void HandleAction(Direction dir)
    {
        keyDown = true;

        if (currentActions[0] == dir)
        {
            currentActions.RemoveAt(0);
            actionsController.CompletedAction();
        }
        else 
        {
            actionsController.IncorrectAction();
        }

        if (currentActions.Count == 0)
        {
            if (printerBroken)
            {
                // Fix printer
                Invoke(nameof(FixPrinter), 0.2f);
            }
            else
            {
                // Pass paper
                Invoke(nameof(PassPaper), 0.2f);
            }
        }
    }

    void PassPaper()
    {
        paperObject.SetActive(false);
        paperSpawned = false;

        // Pass paper to player 2
        papersController.SpawnPaper();

        // Update actions display
        actionsController.HideActions();
    }

    void FixPrinter()
    {
        printerBroken = false;

        // Update actions display
        actionsController.HideActions();
    }
}
