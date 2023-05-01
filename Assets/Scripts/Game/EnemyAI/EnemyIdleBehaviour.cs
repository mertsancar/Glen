using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    private Enemy enemy;
    private Transform playerTransform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<Enemy>();
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = playerTransform.GetComponent<Player>();

        if (enemy.isUnderAttack)
        {
            animator.SetBool(EnemyAIStates.IsFollowing, true);
        }
        if (Vector2.Distance(playerTransform.position, animator.transform.position) <= 5f && !player.isStealth)
        {
            animator.SetBool(EnemyAIStates.IsFollowing, true);
        }

    }

}

public static class EnemyAIStates
{
    public static readonly int IsFollowing = Animator.StringToHash("isFollowing");
    public static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");
    public static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    

}
