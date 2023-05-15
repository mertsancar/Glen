using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : StateMachineBehaviour
{
    public Transform enemyTransform;
    
    public float speed;
    public bool isRight = true;
    private int randomSpot;
    private List<Vector2> patrolSpots;
    private Vector2 nearestSpot;
    private Transform playerTransform;
    private GameObject enemyEyes;


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
        
        
        if (isRight)
        {
            nearestSpot = rightPatrolSpot;
            isRight = true;
            animator.transform.localScale = new Vector3(animator.transform.localScale.x,
                animator.transform.localScale.y, animator.transform.localScale.z);
        }
        else
        {
            nearestSpot = leftPatrolSpot;
            isRight = false;
            animator.transform.localScale = new Vector3(-animator.transform.localScale.x,
                animator.transform.localScale.y, animator.transform.localScale.z);
        }

        enemyEyes = GameObject.Find("Eyes");

    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var hit = Physics2D.Raycast(enemyEyes.transform.position,  isRight ? Vector2.right : Vector2.left, 5f);

        if (hit.collider)
        {
            if (hit.collider.CompareTag("Player") && !playerTransform.GetComponent<Player>().isStealth)
            {
                animator.SetBool(EnemyAIStates.IsFollowing, true);
                animator.SetBool(EnemyAIStates.IsPatrolling, false);
            }
        }

        animator.transform.position = Vector2.MoveTowards(animator.transform.position, nearestSpot, speed * Time.deltaTime);
        
        if (Vector2.Distance(animator.transform.position, nearestSpot) <= 1f)
        {
            if (nearestSpot == patrolSpots[0])
            {
                nearestSpot = patrolSpots[1];
                if (isRight)
                {
                    isRight = false;
                    animator.transform.localScale = new Vector3(-1,
                        animator.transform.localScale.y, animator.transform.localScale.z);
                }
                else
                {
                    isRight = true;
                    animator.transform.localScale = new Vector3(1,
                        animator.transform.localScale.y, animator.transform.localScale.z);
                }
            }
            else if(nearestSpot == patrolSpots[1])
            {
                nearestSpot = patrolSpots[0];
                if (isRight)
                {
                    isRight = false;
                    animator.transform.localScale = new Vector3(-1,
                        animator.transform.localScale.y, animator.transform.localScale.z);
                }
                else
                {
                    isRight = true;
                    animator.transform.localScale = new Vector3(1,
                        animator.transform.localScale.y, animator.transform.localScale.z);
                }
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
