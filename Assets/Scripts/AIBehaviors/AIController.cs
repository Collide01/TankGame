using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIController : Controller
{
    public enum AIState { Idle, Chase, Flee, Patrol, Attack, Scan, BackToPost, GoToSpot };
    protected enum MoveDirection { Neither, Forward, Backward };

    public float attackRange = 100f;
    public AIState currentState = AIState.Idle;
    protected float lastStateChangeTime = 0f;
    public GameObject target;
    public Transform post;
    [Range(0, 360)] public float fieldOfView = 30f;
    public float viewDistance = 15f;
    [HideInInspector] public bool seeTarget = false;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public float hearingDistance = 15f;
    public List<Transform> patrolPoints;
    [HideInInspector] public int currentPatrolPoint = 0; // Set to the patrolPoints index
    protected MoveDirection moveDirection = MoveDirection.Neither;
    [HideInInspector] public Vector3 heardPosition = Vector3.zero; // For GoToSpot state

    // Simple steering
    private float steeringDistance = 0;
    private List<float> steeringAmounts;
    private float totalSteeringAmount;
    public float minSteerDistance = 2;
    public float maxSteerDistance = 3;
    public GameObject obstacleCheckPrefab;
    private GameObject obstacleCheck;
    private ObstacleCheck obstacleCheckScript;

    public override void Start()
    {
        pawn = GetComponent<Pawn>();
        steeringAmounts = new List<float>();
        obstacleCheck = Instantiate(obstacleCheckPrefab, transform.position, Quaternion.identity);
        obstacleCheckScript = obstacleCheck.GetComponent<ObstacleCheck>();

        GameObject postTransform = GameObject.FindGameObjectWithTag("Post");
        post = postTransform.transform;
    
        GameObject[] patrolTransforms = GameObject.FindGameObjectsWithTag("PatrolPoint");
        for (int i = 0; i < patrolTransforms.Length; i++)
        {
            patrolPoints.Add(patrolTransforms[i].transform);
        }

        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the AI
            if (GameManager.instance.aiControllers != null)
            {
                // Register with the GameManager
                GameManager.instance.aiControllers.Add(this);
            }
        }

        base.Start();
    }

    public override void Update()
    {
        MakeDecisions();
        base.Update();
        obstacleCheck.transform.position = transform.position;
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
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    seeTarget = true;
                    return true;
                }
                else
                {
                    seeTarget = false;
                    return false;
                }
            }
            else
            {
                seeTarget = false;
                return false;
            }
        }
        else
        {
            seeTarget = false;
            return false;
        }
    }

    public virtual void DoAttackState()
    {
        if (target != null)
        {
            pawn.RotateTowards(target.transform.position);
            pawn.Shoot();
            moveDirection = MoveDirection.Neither;
        }
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

    public virtual void DoPatrolState(bool changeState = false) // changeState prevents changing the patrol point so it can manually be done in MakeDecisions()
    {
        // Turn to face patrol point
        pawn.RotateTowards(patrolPoints[currentPatrolPoint].transform.position, totalSteeringAmount);
        // Move forward
        pawn.MoveForward();
        moveDirection = MoveDirection.Forward;

        if (Vector3.SqrMagnitude(patrolPoints[currentPatrolPoint].transform.position - transform.position) < 1f && !changeState)
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
        moveDirection = MoveDirection.Neither;
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
        pawn.StayStill();
    }

    // Make sure to set heardPosition before entering the state to prevent heardPosition from updating while in the state
    public virtual void DoGoToSpotState()
    {
        //throw new NotImplementedException();
        pawn.RotateTowards(heardPosition, totalSteeringAmount);
        pawn.MoveForward();
        moveDirection = MoveDirection.Forward;
    }

    public void ChangeAIState(AIState newState)
    {
        lastStateChangeTime = Time.time;
        currentState = newState;
        Debug.Log("Switching to " + currentState);
    }

    // This helps AI avoid obstacles and walls
    private void AdjustSimpleSteering()
    {
        steeringAmounts.Clear();
        for (int i = 0; i < obstacleCheckScript.closestPoints.Count; i++)
        {
            steeringAmounts.Add(0);

            // Determines if the tank is moving towards the obstacle
            steeringDistance = obstacleCheckScript.distances[i];
            float sightAngle = Vector3.Angle(obstacleCheckScript.directions[i], transform.forward);

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    // This prevents the tank from continuing to turn after passing by the obstacle
                    if (sightAngle <= 91)
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
                            Vector3 perp = Vector3.Cross(transform.forward, obstacleCheckScript.directions[i]);
                            float dir = Vector3.Dot(perp, transform.up);
                            if (dir >= 0f)
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
                    if (sightAngle >= 89)
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
                            Vector3 perp = Vector3.Cross(transform.forward, obstacleCheckScript.directions[i]);
                            float dir = Vector3.Dot(perp, transform.up);
                            if (dir <= 0f)
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