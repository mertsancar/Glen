using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBehaviour : StateMachineBehaviour
{
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enemy attacking!");
        animator.SetBool(IsAttacking, false);
    }
    
}
