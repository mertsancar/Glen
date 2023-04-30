using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public SkillType skillType;
    // private Skill skill;
    //
    // private void Awake()
    // {
    //     switch (skillType)
    //     {
    //         case SkillType.Ignis:
    //             skill = GameController.skillsData.
    //             break;
    //         case SkillType.Aqua:
    //             break;
    //         case SkillType.Terra:
    //             break;
    //         case SkillType.Animus:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            var enemy = col.gameObject.GetComponent<Enemy>();
            enemy.isUnderAttack = true;
            enemy.TakeDamage(GameController.instance.currentSkill.damage);
            Destroy(gameObject);
        }
        // if (col.gameObject.CompareTag("Player"))
        // {
        //     var player = col.gameObject.GetComponent<Player>();
        //     player.isUnderAttack = true;
        //     player.TakeDamage(GameController.instance.currentSkill.damage);
        //     Destroy(gameObject);
        // }
        
    }
}
