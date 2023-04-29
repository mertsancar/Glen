using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolBehaviour : StateMachineBehaviour
{
    public float speed;
    private bool isRight;
    private int randomSpot;
    private List<Vector2> patrolSpots;
    private Vector2 nearestSpot;
    private Transform _playerPos;
    private static readonly int IsFollowing = Animator.StringToHash("isFollowing");
    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        var rightPatrolSpot = new Vector2(animator.transform.position.x + 3, animator.transform.position.y);
        var leftPatrolSpot = new Vector2(animator.transform.position.x - 3, animator.transform.position.y);
        patrolSpots = new List<Vector2>() {rightPatrolSpot, leftPatrolSpot};
        
        var distanceToRightSpot = Vector2.Distance(animator.transform.position, rightPatrolSpot);
        var distanceToLeftSpot = Vector2.Distance(animator.transform.position, leftPatrolSpot);
        
        if (distanceToRightSpot > distanceToLeftSpot)
        {
            nearestSpot = leftPatrolSpot;
        }
        else
        {
            nearestSpot = rightPatrolSpot;
        }
        
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = _playerPos.GetComponent<Player>();

        if (Vector2.Distance(_playerPos.position, animator.transform.position) <= 5f && !player.isStealth)
        {
            animator.SetBool(IsFollowing, true);
            animator.SetBool(IsPatrolling, false);
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

    }

}
