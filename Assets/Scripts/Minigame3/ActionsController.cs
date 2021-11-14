using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ActionsController : MonoBehaviour
{
    public GameObject upPrefab;
    public GameObject leftPrefab;
    public GameObject downPrefab;
    public GameObject rightPrefab;
    public GameObject fPrefab;
    public GameObject gPrefab;
    public GameObject placeholderPrefab;

    Dictionary<Action, GameObject> actionPrefabs = new Dictionary<Action, GameObject>();
    List<GameObject> actions = new List<GameObject>();

    int currentActionIndex = 0;

    UnityEvent doneDisplayingSeqActions;

    private void Start()
    {
        actionPrefabs.Add(Action.Up, upPrefab);
        actionPrefabs.Add(Action.Left, leftPrefab);
        actionPrefabs.Add(Action.Down, downPrefab);
        actionPrefabs.Add(Action.Right, rightPrefab);
        actionPrefabs.Add(Action.F, fPrefab);
        actionPrefabs.Add(Action.G, gPrefab);

        if (doneDisplayingSeqActions == null)
            doneDisplayingSeqActions = new UnityEvent();
    }

    public void AddListenerToSeqActions(UnityAction action)
    {
        doneDisplayingSeqActions.AddListener(action);
    }

    public void DisplayActions(List<Action> actions)
    {
        GameObject actionObj;

        foreach (Action act in actions)
        {
            actionPrefabs.TryGetValue(act, out actionObj);

            if (actionObj) this.actions.Add(Instantiate(actionObj, transform));
        }
    }

    public void DisplayActionsSequentially(List<Action> actions, float waitTime)
    {
        //SetPlaceholders(actions.Count);
        StartCoroutine(DisplayActionsSeq(actions, waitTime));
    }

    public void SetPlaceholders(int numActions)
    {
        for (int i = 0; i < numActions; i++)
        {
            actions.Add(Instantiate(placeholderPrefab, transform));
        }
    }

    IEnumerator DisplayActionsSeq(List<Action> newActions, float waitTime)
    {
        yield return new WaitForSeconds(1f);
        
        GameObject actionObj;

        for (int i = 0; i < actions.Count; i++)
        {
            yield return new WaitForSeconds(waitTime);
            actionPrefabs.TryGetValue(newActions[i], out actionObj);

            if (actionObj)
            {
                actions[i].GetComponent<Image>().sprite = actionObj.GetComponent<Image>().sprite;
                actions[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            } 
        }

        doneDisplayingSeqActions.Invoke();
    }

    public void ShowCorrectAction(Action action)
    {
        GameObject actionObj;
        actionPrefabs.TryGetValue(action, out actionObj);

        actions[currentActionIndex].GetComponent<Image>().sprite = actionObj.GetComponent<Image>().sprite;
        actions[currentActionIndex].GetComponent<Image>().color = Color.green;

        currentActionIndex++;
    }

    public void ShowIncorrectAction(Action action)
    {
        // Temporary as we use keyboard input
        GameObject actionObj;
        actionPrefabs.TryGetValue(action, out actionObj);

        actions[currentActionIndex].GetComponent<Image>().sprite = actionObj.GetComponent<Image>().sprite;
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
