using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public Player player;
    [NonSerialized] public Transform currentSkillPrefab;
    
    [Header("Using Abilities")] 
    private List<SkillData> skillsData;
    
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        
        skillsData = Resources.Load<SkillTreeItem>("Skills/Skill Tree").skillData;
        currentSkillPrefab = skillsData[0].skills[0].skillPrefab;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ChangeCurrentSKill(skillsData.Find((skill) => skill.skillType == SkillType.Ignis));
        }
        if (Input.GetKey(KeyCode.E))
        {
            ChangeCurrentSKill(skillsData.Find((skill) => skill.skillType == SkillType.Aqua));
        }
    }

    public void ChangeCurrentSKill(SkillData newSkillData)
    {
        currentSkillPrefab = newSkillData.skills[0].skillPrefab;
    }

}
