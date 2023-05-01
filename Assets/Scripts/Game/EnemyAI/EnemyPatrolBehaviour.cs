using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : StateMachineBehaviour
{
    public Transform enemyTransform;
    
    public float speed;
    private bool isRight;
    private int randomSpot;
    private List<Vector2> patrolSpots;
    private Vector2 nearestSpot;
    private Transform playerTransform;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = animator.transform;

        var enemyPosition = enemyTransform.position;
        var rightPatrolSpot = new Vector2(enemyPosition.x + 3, enemyPosition.y);
        var leftPatrolSpot = new Vector2(enemyPosition.x - 3, enemyPosition.y);
        
        patrolSpots = new List<Vector2>() {rightPatrolSpot, leftPatrolSpot};
        var distanceToRightSpot = Vector2.Distance(enemyPosition, rightPatrolSpot);
        var distanceToLeftSpot = Vector2.Distance(enemyPosition, leftPatrolSpot);
        
        nearestSpot = distanceToRightSpot > distanceToLeftSpot ? leftPatrolSpot : rightPatrolSpot;
        
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = playerTransform.GetComponent<Player>();

        if (Vector2.Distance(playerTransform.position, animator.transform.position) <= 5f && !player.isStealth)
        {
            animator.SetBool(EnemyAIStates.IsFollowing, true);
            animator.SetBool(EnemyAIStates.IsPatrolling, false);
        }
        
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, nearestSpot, speed * Time.deltaTime);
        
        if (Vector2.Distance(animator.transform.position, nearestSpot) <= 1f)
        {
            if (nearestSpot == patrolSpots[0])
            {
                nearestSpot = patrolSpots[1];
            }
            else if(nearestSpot == patrolSpots[1])
            {
                nearestSpot = patrolSpots[0];
            }
        }

        var enemy = animator.GetComponent<Enemy>();
        if (enemy.isUnderAttack)
        {
            animator.SetBool(EnemyAIStates.IsFollowing, true);
            animator.SetBool(EnemyAIStates.IsPatrolling, false);
        }

    }

}
