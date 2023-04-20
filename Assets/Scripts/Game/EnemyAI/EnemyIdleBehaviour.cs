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
        if (Vector2.Distance(_playerPos.position, animator.transform.position) <= 5f)
        {
            animator.SetBool(IsFollowing, true);
        }
    }

}
