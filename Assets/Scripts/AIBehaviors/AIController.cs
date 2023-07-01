using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BackToPost };
    protected enum MoveDirection { Neither, Forward, Backward };

    public float attackRange = 100f;
    public AIState currentState = AIState.Scan;
    protected float lastStateChangeTime = 0f;
    public GameObject target;
    public Transform post;
    public float fieldOfView = 30f;
    public float hearingDistance = 50f;
    public List<Transform> patrolPoints;
    private int currentPatrolPoint = 0; // Set to the patrolPoints index
    protected MoveDirection moveDirection = MoveDirection.Neither;

    // Simple steering
    private float steeringDistance = 0;
    private List<float> steeringAmounts;
    private float totalSteeringAmount;
    private float minSteerDistance = 2;
    private float maxSteerDistance = 3;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        post = transform;
        steeringAmounts = new List<float>();
        base.Start();
    }

    public override void Update()
    {
        MakeDecisions();
        base.Update();
        AdjustSimpleSteering();
    }

    public abstract void MakeDecisions();

    public virtual bool CanHear(GameObject targetGameObject)
    {
        // Get the target's NoiseMaker
        NoiseMaker noiseMaker = targetGameObject.GetComponent<NoiseMaker>();
        // If they don't have one, they can't make noise, so return false
        if (noiseMaker == null)
        {
            return false;
        }
        // If they are making 0 noise, they also can't be heard
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, targetGameObject.transform.position) <= totalDistance)
        {
            // ... then we can hear the target
            return true;
        }
        else
        {
            // Otherwise, we are too far away to hear them
            return false;
        }
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
        pawn.RotateTowards(target.transform.position, totalSteeringAmount);
        pawn.Shoot();
    }

    public virtual void DoChaseState()
    {
        // Turn to face target
        pawn.RotateTowards(target.transform.position, totalSteeringAmount);
        // Move forward
        pawn.MoveForward();
        moveDirection = MoveDirection.Forward;
    }

    public virtual void DoFleeState()
    {
        //throw new NotImplementedException();
        // Turn to face target
        pawn.RotateTowards(target.transform.position, totalSteeringAmount);
        // Move backward
        pawn.MoveBackward();
        moveDirection = MoveDirection.Backward;
    }

    public virtual void DoPatrolState()
    {
        // Turn to face patrol point
        pawn.RotateTowards(patrolPoints[currentPatrolPoint].transform.position, totalSteeringAmount);
        // Move forward
        pawn.MoveForward();
        moveDirection = MoveDirection.Forward;

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
        pawn.Rotate(pawn.turnSpeed);
    }

    public virtual void DoBackToPostState()
    {
        //throw new NotImplementedException();
        pawn.RotateTowards(post.position, totalSteeringAmount);
        pawn.MoveForward();
        moveDirection = MoveDirection.Forward;
    }

    public virtual void DoIdleState()
    {
        //throw new NotImplementedException();
        moveDirection = MoveDirection.Neither;
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
    }

    // This helps AI avoid obstacles and walls
    private void AdjustSimpleSteering()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        steeringAmounts.Clear();
        for (int i = 0; i < obstacles.Length; i++)
        {
            steeringAmounts.Add(0);

            // Determines if the tank is moving towards the obstacle
            Vector3 direction = obstacles[i].transform.position - transform.position;
            steeringDistance = direction.magnitude;
            float sightAngle = Vector3.Angle(direction, transform.forward);

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    // This prevents the tank from continuing to turn after passing by the obstacle
                    if (sightAngle < 60)
                    {
                        if (steeringDistance <= maxSteerDistance)
                        {
                            if (steeringDistance < minSteerDistance)
                            {
                                steeringAmounts[i] = pawn.turnSpeed * 2;
                            }
                            else
                            {
                                steeringAmounts[i] = (Mathf.Abs(steeringDistance - maxSteerDistance) / Mathf.Abs(minSteerDistance - maxSteerDistance)) * pawn.turnSpeed * 2;
                            }

                            // Checks if the obstacle is to the right of the tank
                            Vector3 perp = Vector3.Cross(transform.forward, direction);
                            float dir = Vector3.Dot(perp, transform.up);
                            if (dir > 0f)
                            {
                                steeringAmounts[i] *= -1; // Turn left
                            }
                        }
                        else
                        {
                            steeringAmounts[i] = 0;
                        }
                    }
                    else
                    {
                        steeringAmounts[i] = 0;
                    }
                    break;
                case MoveDirection.Backward:
                    // This prevents the tank from continuing to turn after passing by the obstacle
                    if (sightAngle > 120)
                    {
                        if (steeringDistance <= maxSteerDistance)
                        {
                            if (steeringDistance < minSteerDistance)
                            {
                                steeringAmounts[i] = pawn.turnSpeed * 2;
                            }
                            else
                            {
                                steeringAmounts[i] = (Mathf.Abs(steeringDistance - maxSteerDistance) / Mathf.Abs(minSteerDistance - maxSteerDistance)) * pawn.turnSpeed * 2;
                            }

                            // Checks if the obstacle is to the right of the tank
                            Vector3 perp = Vector3.Cross(transform.forward, direction);
                            float dir = Vector3.Dot(perp, transform.up);
                            if (dir > 0f)
                            {
                                steeringAmounts[i] *= -1; // Turn left
                            }
                        }
                        else
                        {
                            steeringAmounts[i] = 0;
                        }
                    }
                    else
                    {
                        steeringAmounts[i] = 0;
                    }
                    break;
                default:
                    steeringAmounts[i] = 0;
                    break;
            }
        }

        totalSteeringAmount = 0;
        for (int i = 0; i < steeringAmounts.Count; i++)
        {
            totalSteeringAmount += steeringAmounts[i];
        }
    }
}