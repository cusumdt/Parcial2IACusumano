/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using V_AnimationSystem;
using CodeMonkey.Utils;

public class CharacterPathfindingMovementHandler : MonoBehaviour {

    private const float speed = 40f;
    private static readonly Vector3 vector3Down = new Vector3(0, -1);
    private V_UnitSkeleton unitSkeleton;
    private V_UnitAnimation unitAnimation;
    private AnimatedWalker animatedWalker;
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    private const float MathfRad2Deg = Mathf.Rad2Deg;
    [SerializeField] GameObject col;
    Quaternion qt = Quaternion.identity;
    Vector3 target = Vector3.zero;

    private void Start() {
        Transform bodyTransform = transform.Find("Body");
        unitSkeleton = new V_UnitSkeleton(1f, bodyTransform.TransformPoint, (Mesh mesh) => bodyTransform.GetComponent<MeshFilter>().mesh = mesh);
        unitAnimation = new V_UnitAnimation(unitSkeleton);
        animatedWalker = new AnimatedWalker(unitAnimation, UnitAnimType.GetUnitAnimType("dMarine_Idle"), UnitAnimType.GetUnitAnimType("dMarine_Walk"), 1f, 1f);
    }

    private void Update() {
        HandleMovement();
        unitSkeleton.Update(Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            SetTargetPosition(UtilsClass.GetMouseWorldPosition());
        }
        Vector3 vectorToTarget = target - col.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        qt = Quaternion.AngleAxis(angle, Vector3.forward);
        col.transform.rotation = Quaternion.RotateTowards(col.transform.rotation, qt, Time.deltaTime * 1000);
    }
    
    private void HandleMovement() {
        if (pathVectorList != null) {
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position, targetPosition) > 1f) {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                animatedWalker.SetMoveVector(moveDir);
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            } else {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count) {
                    StopMoving();
                    animatedWalker.SetMoveVector(Vector3.zero);
                }
            }
        } else {
            animatedWalker.SetMoveVector(Vector3.zero);
        }
    }

    private void StopMoving() {
        pathVectorList = null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void SetTargetPosition(Vector3 targetPosition) {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPosition);

        if (pathVectorList != null && pathVectorList.Count > 1) {
            pathVectorList.RemoveAt(0);
        }
    }

    public void colRotation(Vector3 _target)
    {
        target = _target;
    }

    public static double GetAngleFromVector(Vector3 dir)
    {
        if (dir.x == 0f && dir.y == 0f) dir = vector3Down;

        double n = Math.Atan2(dir.y, dir.x) * MathfRad2Deg;
        if (n < 0) n += 360;
        int angle = (int)Math.Round(n / 45);

        return n;
    }

}