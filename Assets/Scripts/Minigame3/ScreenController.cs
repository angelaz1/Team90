using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Action { Up, Down, Left, Right, F, G }

public class ScreenController : MonoBehaviour
{
    int currNumSteps = 3;
    List<Action> actions;

    void Start()
    {
        actions = new List<Action>();
    }

    void CreateGreeting()
    {
        for (int i = 0; i < currNumSteps; i++)
        {
            Action randDir = (Action)Random.Range(0, 6);
            actions.Add(randDir);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
