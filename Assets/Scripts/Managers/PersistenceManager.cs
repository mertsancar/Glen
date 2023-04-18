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
    
    public static int GetCurrentLevelIndex()
    {
        return PlayerPrefs.GetInt("CurrentLevelIndex", 0);
    }
    
    public static void SetCurrentLevelIndex(int value)
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", value);
    }
    
    public static int GetIgnisPointCount()
    {
        return PlayerPrefs.GetInt("IgnisPointCount", 0);
    }
    
    public static void SetIgnisPointCount(int value)
    {
        PlayerPrefs.SetInt("IgnisPointCount", value);
    }
    
    public static int GetAquaPointCount()
    {
        return PlayerPrefs.GetInt("AquaPointCount", 0);
    }
    
    public static void SetAquaPointCount(int value)
    {
        PlayerPrefs.SetInt("AquaPointCount", value);
    }
    
    public static int GetTerraPointCount()
    {
        return PlayerPrefs.GetInt("TerraPointCount", 0);
    }
    
    public static void SetTerraPointCount(int value)
    {
        PlayerPrefs.SetInt("TerraPointCount", value);
    }
    
    public static int GetAnimusPointCount()
    {
        return PlayerPrefs.GetInt("AnimusPointCount", 0);
    }
    
    public static void SetAnimusPointCount(int value)
    {
        PlayerPrefs.SetInt("AnimusPointCount", value);
    }
}   
