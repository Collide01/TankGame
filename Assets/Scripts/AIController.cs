using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BackToPost };

    public float attackRange = 100f;
    public AIState currentState = AIState.Scan;
    protected float lastStateChangeTime = 0f;
    public GameObject target;
    public Transform post;
    public float fieldOfView = 30f;
    public List<Transform> patrolPoints;
    private int currentPatrolPoint = 0; // Set to the patrolPoints index
    public float turnSpeed;

    // Simple steering
    private int steeringDistance = 0;
    private float steeringAmount = 0;
    private float minSteerDistance = 1;
    private float maxSteerDistance = 2;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        post = transform;
        base.Start();
    }

    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public abstract void MakeDecisions();

    public virtual bool CanHear(GameObject targetGameObject)
    {
        return false;
    }

    public virtual bool CanSee(GameObject targetGameObject)
    {
        Vector3 agentToTargetVector = targetGameObject.transform.position - transform.position;

        if (Vector3.Angle(agentToTargetVector, transform.forward) <= fieldOfView)
        {

            Vector3 raycastDirection = targetGameObject.transform.position - pawn.transform.position;
            RaycastHit hit;
            Physics.Raycast(transform.position, raycastDirection, out hit);
            if (Physics.Raycast(transform.position, raycastDirection, out hit))
            {
                if (hit.collider.transform.parent != null)
                {
                    return (hit.collider.transform.parent.gameObject == targetGameObject);
                }
            }
        }
        return false;
    }

    public virtual void DoAttackState()
    {
        pawn.RotateTowards(target.transform.position);
        pawn.Shoot();
    }

    public virtual void DoChaseState()
    {
        // Turn to face target
        pawn.RotateTowards(target.transform.position);
        // Move forward
        pawn.MoveForward();
    }

    public virtual void DoFleeState()
    {
        //throw new NotImplementedException();
        // Turn to face target
        pawn.RotateTowards(target.transform.position);
        // Move backward
        pawn.MoveBackward();
    }

    public virtual void DoPatrolState()
    {
        // Turn to face patrol point
        pawn.RotateTowards(patrolPoints[currentPatrolPoint].transform.position);
        // Move forward
        pawn.MoveForward();

        if (Mathf.Abs(transform.position.x - patrolPoints[currentPatrolPoint].transform.position.x) < 0.1f && Mathf.Abs(transform.position.z - patrolPoints[currentPatrolPoint].transform.position.z) < 0.1f)
        {
            currentPatrolPoint++;
            if (currentPatrolPoint > patrolPoints.Count - 1)
            {
                currentPatrolPoint = 0;
            }
        }
    }

    public virtual void DoScanState()
    {
        // Rotate Clockwise
        pawn.Rotate(turnSpeed);
    }

    public virtual void DoBackToPostState()
    {
        //throw new NotImplementedException();
        pawn.RotateTowards(post.position);
        pawn.MoveForward();
    }

    public virtual void DoIdleState()
    {
        //throw new NotImplementedException();
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }
}