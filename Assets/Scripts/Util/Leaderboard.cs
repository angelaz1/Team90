using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    public string teamName;
    public int time;
}

[Serializable]
public class Leaderboard
{
    public Score[] scores;
}

[Serializable]
public class LeaderboardInfo
{
    public string[] teamNames;
    public int[] teamTimes;
    public int maxGrab;
}
