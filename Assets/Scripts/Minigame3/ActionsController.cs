using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionsController : MonoBehaviour
{
    public GameObject upPrefab;
    public GameObject leftPrefab;
    public GameObject downPrefab;
    public GameObject rightPrefab;
    public GameObject fPrefab;
    public GameObject gPrefab;

    Dictionary<Action, GameObject> actionPrefabs = new Dictionary<Action, GameObject>();
    List<GameObject> actions = new List<GameObject>();

    int currentActionIndex = 0;

    private void Start()
    {
        actionPrefabs.Add(Action.Up, upPrefab);
        actionPrefabs.Add(Action.Left, leftPrefab);
        actionPrefabs.Add(Action.Down, downPrefab);
        actionPrefabs.Add(Action.Right, rightPrefab);
        actionPrefabs.Add(Action.F, fPrefab);
        actionPrefabs.Add(Action.G, gPrefab);
    }

    public void DisplayActions(List<Action> actions)
    {
        foreach (Action act in actions)
        {
            GameObject actionObj;
            actionPrefabs.TryGetValue(act, out actionObj);

            if (actionObj) this.actions.Add(Instantiate(actionObj, transform));
        }
    }

    public void CompletedAction()
    {
        // Temporary as we use keyboard input
        actions[currentActionIndex].GetComponent<Image>().color = Color.green;

        currentActionIndex++;
    }

    public void IncorrectAction()
    {
        // Temporary as we use keyboard input
        actions[currentActionIndex].GetComponent<Image>().color = Color.red;
    }

    public void HideActions()
    {
        foreach (GameObject action in actions)
        {
            Destroy(action);
        }
        actions.Clear();
        currentActionIndex = 0;
    }
}
