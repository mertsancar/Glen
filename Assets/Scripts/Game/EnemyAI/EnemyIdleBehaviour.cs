using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleBehaviour : StateMachineBehaviour
{
    private Transform _playerPos;
    private static readonly int IsFollowing = Animator.StringToHash("isFollowing");
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = _playerPos.GetComponent<Player>();

        var enemy = animator.GetComponent<Enemy>();
        if (enemy.isUnderAttack)
        {
            animator.SetBool(IsFollowing, true);
        }
        if (Vector2.Distance(_playerPos.position, animator.transform.position) <= 5f && !player.isStealth)
        {
            animator.SetBool(IsFollowing, true);
        }

    }

}
