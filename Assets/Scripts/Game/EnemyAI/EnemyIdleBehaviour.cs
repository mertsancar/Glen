using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    private Enemy enemy;
    private GameObject enemyEyes;
    private Transform playerTransform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = animator.GetComponent<Enemy>();
        enemyEyes = GameObject.Find("Eyes");
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var hit = Physics2D.Raycast(enemyEyes.transform.position, Vector2.right, 5f);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player") && !playerTransform.GetComponent<Player>().isStealth)
            {
                animator.SetBool(EnemyAIStates.IsFollowing, true);
            }
        }
 
        if (enemy.isUnderAttack)
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
