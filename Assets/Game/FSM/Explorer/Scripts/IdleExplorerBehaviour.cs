using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleExplorerBehaviour : BaseExplorerBehaviour
{
    [SerializeField] private float timeToChangeState = 5;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Time.time - lastChange > timeToChangeState)
        {
            lastChange = Time.time;
            animator.SetTrigger(hashToPatrol);
        }
    }
}
