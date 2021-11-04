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

    void Start()
    {
        passPaperActions.Add(Direction.Up);
        passPaperActions.Add(Direction.Right);

        StartCoroutine(SpawnPapers());
    }

    IEnumerator SpawnPapers()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));
            SpawnPaper();
            while (paperSpawned)
            { 
                yield return null;
            }
        }
    }

    void SpawnPaper()
    {
        paperObject.SetActive(true);
        paperSpawned = true;

        currentActions.AddRange(passPaperActions);

        // Display actions
        actionsController.DisplayActions(currentActions);
    }

    // Update is called once per frame
    void Update()
    {
        if (paperSpawned)
        {
            if (Input.GetKeyDown(KeyCode.W)) HandleAction(Direction.Up);
            else if (Input.GetKeyDown(KeyCode.A)) HandleAction(Direction.Left);
            else if (Input.GetKeyDown(KeyCode.S)) HandleAction(Direction.Down);
            else if (Input.GetKeyDown(KeyCode.D)) HandleAction(Direction.Right);
        }
    }

    void HandleAction(Direction dir)
    {
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
                printerBroken = false;
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
}
