using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    static int numMinigames = 3;
    public static bool[] finishedMinigames;

    private static SelectionManager _instance;
    public static SelectionManager Instance { get { return _instance; } }

    private string teamName = "";
    private Leaderboard currentLeaderboard;

    int totalTime = 0;

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

    public void FinishMinigame(int index)
    {
        finishedMinigames[index] = true;
    }

    public void ClearMinigames()
    {
        finishedMinigames = new bool[numMinigames];
        totalTime = 0;
    }

    public void SetTeamName(string newName)
    {
        Debug.Log("Team Name: " + newName);
        teamName = newName;
    }


    public void WriteToLeaderboard(int minigameIndex, int time)
    {
        totalTime += time;

        string leaderboardFileName = $"minigame{minigameIndex}.json";
        string path = Application.persistentDataPath + "/" + leaderboardFileName;
        Debug.Log(path);

        currentLeaderboard = new Leaderboard();
        List<Score> newScoresList = new List<Score>();

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            reader.Close();

            currentLeaderboard = JsonUtility.FromJson<Leaderboard>(text);
            newScoresList = new List<Score>(currentLeaderboard.scores);
        }

        Score newScore = new Score()
        {
            teamName = teamName,
            time = time
        };
        newScoresList.Add(newScore);

        currentLeaderboard.scores = newScoresList.ToArray();
        string json = JsonUtility.ToJson(currentLeaderboard);

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Close();
    }

    public LeaderboardInfo GetCurrentLeaderboardInfo(int maxGrab)
    {
        Score[] scores = currentLeaderboard.scores;

        Score[] topScores = new Score[maxGrab];
        int currentCount = 0;
        int currentMax = 999;

        foreach (Score score in scores)
        {
            if (score.time > currentMax) continue;

            int i;
            for (i = 0; i < currentCount; i++)
            {
                if (score.time < topScores[i].time) break;
            }

            // Shift everything down by one
            for (int j = maxGrab - 1; j > i; j--)
            {
                topScores[j] = topScores[j - 1];
            }

            // Insert new score
            topScores[i] = score;
            currentMax = currentCount < maxGrab ? 999 : topScores[maxGrab - 1].time;
            currentCount = Mathf.Min(currentCount + 1, maxGrab);
        }

        LeaderboardInfo currentInfo = new LeaderboardInfo();
        string[] teamNames = new string[maxGrab];
        int[] teamTimes = new int[maxGrab];

        for (int i = 0; i < currentCount; i++)
        {
            if (topScores[i].teamName == teamName) teamNames[i] = $"*{topScores[i].teamName}*";
            else teamNames[i] = topScores[i].teamName;

            teamTimes[i] = topScores[i].time;
        }

        currentInfo.teamNames = teamNames;
        currentInfo.teamTimes = teamTimes;
        currentInfo.maxGrab = maxGrab;

        return currentInfo;
    }

    public LeaderboardInfo GetFinalScores(int maxGrab)
    {
        string leaderboardFileName = $"finalScores.json";
        string path = Application.persistentDataPath + "/" + leaderboardFileName;

        currentLeaderboard = new Leaderboard();
        List<Score> newScoresList = new List<Score>();

        if (File.Exists(path))
        {
            StreamReader reader = new StreamReader(path);
            string text = reader.ReadToEnd();
            reader.Close();

            currentLeaderboard = JsonUtility.FromJson<Leaderboard>(text);
            newScoresList = new List<Score>(currentLeaderboard.scores);
        }

        Score newScore = new Score()
        {
            teamName = teamName,
            time = totalTime
        };
        newScoresList.Add(newScore);

        currentLeaderboard.scores = newScoresList.ToArray();
        string json = JsonUtility.ToJson(currentLeaderboard);

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Close();

        return GetCurrentLeaderboardInfo(maxGrab);
    }
}
