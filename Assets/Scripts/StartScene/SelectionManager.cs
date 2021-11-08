using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    int numMinigames = 2;
    bool[] finishedMinigames;
    int[] minigameScores;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        finishedMinigames = new bool[numMinigames];
        minigameScores = new int[numMinigames];
    }

    public int GetNumMinigames()
    {
        return numMinigames;
    }

    public bool AllMinigamesCompleted()
    {
        foreach (bool finished in finishedMinigames)
        {
            if (!finished) return false;
        }
        return true;
    }

    public void FinishMinigame(int index, int score)
    {
        finishedMinigames[index] = true;
        minigameScores[index] = score;
    }
}
