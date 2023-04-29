using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill Tree Item", menuName = "Skill Tree/Skill Tree Item")]
public class SkillTreeItem : ScriptableObject
{
    public List<SkillData> skillData;
    
}

[Serializable]
public struct SkillData
{
    public string skillTypeName;
    public List<Skill> skills;
}


[Serializable]
public struct Skill
{
    public string name;
    public string description;
    public SkillType skillType;
    public string stageCount;
    public int price;
}

public enum SkillType
{
    Ignis,
    Aqua,
    Terra,
    Animus
}
