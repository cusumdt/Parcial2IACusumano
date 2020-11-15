using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class PatrolExplorerBehaviour : BaseExplorerBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector3 randomPosition;
    private bool inMovement = false;
    const int timeToChangeState = 5;
    private float time;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        randomPosition = Vector3.zero;
        inMovement = false;
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
            List<PathNode> path = Testing.pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
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
