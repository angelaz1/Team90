using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    static int numMinigames = 3;
    public static bool[] finishedMinigames;

  //  public GameObject[] checkUI_game;
  

    private static SelectionManager _instance;

    public static SelectionManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            finishedMinigames = new bool[numMinigames];
        }
    }

    public int GetNumMinigames()
    {
        return numMinigames;
    }
    public bool GetFinishedMinigames(int index)
    {
        return finishedMinigames[index];
    }
    public bool AllMinigamesCompleted()
    {
        foreach (bool finished in finishedMinigames)
        {
            if (!finished) return false;
        }
        return true;
    }

    //public int GetScore(int index)
    //{
    //    if (index >= numMinigames) return 0;
    //    return minigameScores[index];
    //}
    

    public void FinishMinigame(int index)
    {
        finishedMinigames[index] = true;
     //   checkUI_game[index].SetActive(true);
    }
    

    
}
