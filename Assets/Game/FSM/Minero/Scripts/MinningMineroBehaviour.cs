using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinningMineroBehaviour : BaseMineroBehaviour
{
    private bool inMovement = false;
    public Vector3 ObjectPos = Vector3.zero;
    [SerializeField] private int maxGold = 5;
    private Vector3 scaleAnim = Vector3.one;
    private Vector3 LimitScaleAnim = new Vector3(1.5f, 1.5f);
    bool switchAnim;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        inMovement = false;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (!inMovement)
        {
            inMovement = true;
            Testing.pathfinding.GetGrid().GetXY(ObjectPos, out int x, out int y);
            //List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            List<PathNode> path = Testing.pathfinding.FindPath(0, 0, x, y);
            Debug.Log(x + ", " + y);
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
        else 
        { 
            if (scaleAnim.x > LimitScaleAnim.x && scaleAnim.y > LimitScaleAnim.y)
                switchAnim = true;
            else if (scaleAnim.x < 1.0f && scaleAnim.y < 1.0f)
                switchAnim = false;

            if (switchAnim)
                scaleAnim = new Vector3(scaleAnim.x - Time.deltaTime, scaleAnim.y - Time.deltaTime);
            else
                scaleAnim = new Vector3(scaleAnim.x + Time.deltaTime, scaleAnim.y + Time.deltaTime);

            owner.gameObject.transform.localScale = scaleAnim;
        }
        if (gold == maxGold) 
        {
            owner.gameObject.transform.localScale = Vector3.one;
            animator.SetTrigger(hashToReturning);
        }

    }
}
