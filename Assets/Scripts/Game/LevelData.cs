using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    public string levelName;
    public Difficulty difficulty;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
    Extreme
}
