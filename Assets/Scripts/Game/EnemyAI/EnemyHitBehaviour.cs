using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBehaviour : StateMachineBehaviour
{
    private Transform _playerPos;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distance = Vector2.Distance(_playerPos.position, animator.transform.position);
        
        Debug.Log("Enemy attacking!");

        if (distance > 1f)
        {
            animator.SetBool(EnemyAIStates.IsAttacking, false);
        }
    }
    
}
