using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMineroBehaviour : StateMachineBehaviour
{
    [SerializeField] protected GameObject owner;
    [SerializeField] protected float lastChange = 0;
    [SerializeField] public int gold;

    protected int hashToIdle = Animator.StringToHash("ToIdle");
    protected int hashToPatrol = Animator.StringToHash("ToPatrol");
    protected int hashToMinning = Animator.StringToHash("ToMinning");
    protected int hashToReturning = Animator.StringToHash("ToReturning");


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!owner)
        {
            owner = animator.gameObject;
        }
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
