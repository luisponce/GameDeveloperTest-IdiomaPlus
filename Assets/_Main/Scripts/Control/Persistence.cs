using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence 
{
    private static Persistence singleton = null;

    private int wins = 0;
    private int loses = 0;
    private int played = 0;

    private const string winsKey = "wins";
    private const string losesKey = "loses";
    private const string playedKey = "played";

    public static Persistence Instance
    {
        get
        {
            if (singleton == null)
            {
                singleton = new Persistence();
            }
            return singleton;
        }
    }

    private Persistence()
    {
        wins = PlayerPrefs.GetInt(winsKey);
        loses = PlayerPrefs.GetInt(losesKey);
        played = PlayerPrefs.GetInt(playedKey);
    }

    public void IncreaseWins()
    {
        wins++;
    }

    public void IncreaseLoses()
    {
        loses++;
    }

    public void IncreasePlayed()
    {
        played++;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(winsKey, wins);
        PlayerPrefs.SetInt(losesKey, loses);
        PlayerPrefs.SetInt(playedKey, played);
    }

    public int Wins { get => wins; }
    public int Loses { get => loses; }
    public int Played { get => played; }
}
