using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollowBehaviour : StateMachineBehaviour
{
    private Transform _playerPos;
    private static readonly int IsFollowing = Animator.StringToHash("isFollowing");
    private static readonly int IsPatrolling = Animator.StringToHash("isPatrolling");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var distance = Vector2.Distance(_playerPos.position, animator.transform.position);
        var enemy = animator.GetComponent<Enemy>();
        if (enemy.isUnderAttack)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, _playerPos.position, 1f * Time.deltaTime);
        }
        else if (distance > 5f)
        {
            animator.SetBool(IsFollowing, false);
            animator.SetBool(IsPatrolling, true);
        }
        else if (distance is > 2f and <= 5f)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position
                , _playerPos.position, 1f * Time.deltaTime);
        }
        else if (distance <= 1f)
        {
            animator.SetBool(IsAttacking, true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var enemy = animator.GetComponent<Enemy>();
        enemy.isUnderAttack = false;
        
        animator.SetBool(IsPatrolling, true);
    }


}
