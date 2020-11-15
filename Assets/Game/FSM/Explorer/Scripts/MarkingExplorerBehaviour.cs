using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class MarkingExplorerBehaviour : BaseMineroBehaviour
{
   
    private bool inMovement = false;
    public Vector3 ObjectPos = Vector3.zero;
    [SerializeField] private float timeToChangeState = 5;
    public bool marking = false;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        inMovement = false;
        marking = true;
       // lastChange = Time.time;
    }



    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!inMovement)
        {
            inMovement = true;
            Testing.pathfinding.GetGrid().GetXY(ObjectPos, out int x, out int y);
            //List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            List<PathNode> path = Testing.pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            owner.gameObject.GetComponent<CharacterPathfindingMovementHandler>().SetTargetPosition(ObjectPos);

            owner.gameObject.GetComponent<CharacterPathfindingMovementHandler>().colRotation(new Vector3(x * 10, y * 10) + Vector3.one * 5f);
        }
        if (Time.time - lastChange > timeToChangeState)
        {
            lastChange = Time.time;
            marking = false;
            animator.SetTrigger(hashToIdle);
        }
    }
}
