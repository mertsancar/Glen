using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollowBehaviour : StateMachineBehaviour
{
    private Transform _playerPos;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distance = Vector2.Distance(_playerPos.position, animator.transform.position);
        
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, _playerPos.position, 1f * Time.deltaTime);
        
        if (distance <= 1f)
        {
            animator.SetBool(EnemyAIStates.IsAttacking, true);
        }
        else if (distance > 5f)
        {
            animator.SetBool(EnemyAIStates.IsFollowing, false);
            animator.SetBool(EnemyAIStates.IsPatrolling, true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var enemy = animator.GetComponent<Enemy>();
        enemy.isUnderAttack = false;
        
        animator.SetBool(EnemyAIStates.IsPatrolling, true);
    }


}
