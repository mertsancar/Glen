using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PersistenceManager 
{
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    
    public static void SetHighScore(int value)
    {
        PlayerPrefs.SetInt("HighScore", value);
    }
}   
