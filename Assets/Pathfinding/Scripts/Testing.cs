using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class Testing : MonoBehaviour {
    
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;
    [SerializeField] private CharacterPathfindingMovementHandler characterPathfinding;
    [SerializeField] private int width;
    [SerializeField] private int height;
    private Vector3 randomPosition;
    private bool inMovement = false;
    private Pathfinding pathfinding;

    private void Start() {
        pathfinding = new Pathfinding(width, height);
        pathfindingDebugStepVisual.Setup(pathfinding.GetGrid());
        pathfindingVisual.SetGrid(pathfinding.GetGrid());
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null) {
                for (int i=0; i<path.Count - 1; i++) {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i+1].x, path[i+1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            Debug.Log(mouseWorldPosition.x + ", " + mouseWorldPosition.y);
            characterPathfinding.SetTargetPosition(mouseWorldPosition);
        }

        if (!inMovement)
        {
            inMovement = true;
            randomPosition = new Vector3(Random.Range(0, width*10), Random.Range(0, height*10));
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(randomPosition, out int x, out int y);
            //List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);

            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
            characterPathfinding.SetTargetPosition(randomPosition);
            StartCoroutine("AutoUnlock");
        }

        if (Input.GetMouseButtonDown(1)) {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            pathfinding.GetNode(x, y).SetIsWalkable(!pathfinding.GetNode(x, y).isWalkable);
        }
    }


    private IEnumerator AutoUnlock() {
        yield return new WaitForSeconds(4.0f);
        inMovement = false;
    }

}
