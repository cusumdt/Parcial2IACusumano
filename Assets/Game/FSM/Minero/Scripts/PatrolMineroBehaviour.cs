using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class PatrolMineroBehaviour : BaseMineroBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector3 randomPosition;
    [SerializeField] private float timeToChangeState = 5;
    private bool inMovement = false;
    private float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        randomPosition = Vector3.zero;
        inMovement = false;
      //  lastChange = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inMovement)
        {
            inMovement = true;
            randomPosition = new Vector3(Random.Range(0, width * 10), Random.Range(0, height * 10));
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            Testing.pathfinding.GetGrid().GetXY(randomPosition, out int x, out int y);
 
            owner.gameObject.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(randomPosition);
         
            owner.gameObject.GetComponent<CharacterPathfindingMovementHandler>().colRotation(new Vector3(x*10,y*10) + Vector3.one * 5f);
        }
        if (time > timeToChangeState)
        {
            time = 0;
            animator.SetTrigger(hashToIdle);
        }
        time += Time.deltaTime;
            
    }

 
}
