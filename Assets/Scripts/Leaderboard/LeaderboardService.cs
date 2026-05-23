using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
public static class LeaderboardService
{
    static string path = Application.persistentDataPath + "/Leaderboard.json";
    public static LeaderboardList leaderboard;
    public static List<LeaderboardEntry> leaderboardAsList => leaderboard == null ? new List<LeaderboardEntry>() : leaderboard.leaderboardEntries;
    public static void SaveToLeaderboard(LeaderboardEntry entry) 
    {
        if (leaderboard == null)
        {
            leaderboard = new LeaderboardList();
            leaderboard.leaderboardEntries = new List<LeaderboardEntry>();
        }
        leaderboard.leaderboardEntries.Add(entry);
        leaderboard.leaderboardEntries = leaderboard.leaderboardEntries.OrderByDescending(val => val.score).ToList();
        string jsonString = JsonUtility.ToJson(leaderboard, true);

        // Ensure directory exists
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }

        File.WriteAllText(path, jsonString);
    }
    public static void LoadLeaderboard() 
    {
        // Ensure directory exists
        if (!Directory.Exists(Application.persistentDataPath))
        {
            Directory.CreateDirectory(Application.persistentDataPath);
        }

        string json = File.Exists(path) ? File.ReadAllText(path) : "{}";
        leaderboard = JsonUtility.FromJson<LeaderboardList>(json);
        if(leaderboard == null) 
        {
            leaderboard = new LeaderboardList();
            leaderboard.leaderboardEntries = new List<LeaderboardEntry>();
        }
        else
        {
            leaderboard.leaderboardEntries = leaderboard.leaderboardEntries.OrderByDescending(val => val.score).ToList();
        }
    }
}
[Serializable]
public class LeaderboardEntry 
{
    public string name;
    public int score;

    public LeaderboardEntry(string name, int score) { this.name = name; this.score = score; }
}
[Serializable]
public class LeaderboardList 
{
    public List<LeaderboardEntry> leaderboardEntries;
}