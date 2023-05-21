using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    
    public Player player;
    
    public static List<SkillData> skillsData;
    [NonSerialized] public Transform currentSkillPrefab;
    [NonSerialized] public Skill currentSkill;
    

    [Header("Health Bar")] 
    public Transform healthPrefab;
    public Transform healthBarLayout;


    public List<GameObject> currentSkillHud;

    
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        EventManager.instance.AddListener(EventName.GameStart, Init);
        EventManager.instance.TriggerEvent(EventName.GameStart);
    }

    private void Init()
    {
        skillsData = Resources.Load<SkillTreeItem>("Skills/Skill Tree").skillData;
        currentSkill = skillsData[0].skills[0];
        currentSkillPrefab = skillsData[0].skills[0].skillPrefab;

        UpdateHealthBar();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ChangeCurrentSKill(skillsData.Find((skill) => skill.skillType == SkillType.Ignis));
            UpdateCurrentSkillHud(0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            ChangeCurrentSKill(skillsData.Find((skill) => skill.skillType == SkillType.Aqua));
            UpdateCurrentSkillHud(1);
        }
        if (Input.GetKey(KeyCode.Escape)) 
        {
            EventManager.instance.TriggerEvent(EventName.ShowScreenRequested, typeof(PauseScreen), null);
        }
    }

    public void ChangeCurrentSKill(SkillData newSkillData)
    {
        currentSkillPrefab = newSkillData.skills[0].skillPrefab;
        currentSkill = newSkillData.skills[0];
    }

    public void UpdateHealthBar()
    {
        ResetHealthBar();
        for (int i = 0; i < player.GetHealth(); i++)
        {
            Instantiate(healthPrefab, healthBarLayout);
        }
    }

    private void ResetHealthBar()
    {
        foreach (Transform child in healthBarLayout.transform) {
            Destroy(child.gameObject);
        }
    }

    private void UpdateCurrentSkillHud(int index)
    {
        foreach (var o in currentSkillHud)
        {
            o.gameObject.SetActive(false);
        }
        
        currentSkillHud[index].gameObject.SetActive(true);
        
    }

   
}
