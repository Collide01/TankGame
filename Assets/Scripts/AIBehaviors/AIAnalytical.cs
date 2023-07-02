using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnalytical : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do that state's behavior
                DoIdleState();

                // Check for transitions
                
                break;
            case AIState.Attack:
                // Do that state's behavior
                DoAttackState();

                // Check for transitions
                if (target != null)
                {
                    if (Vector3.SqrMagnitude(target.transform.position - transform.position) < attackRange)
                    {
                        ChangeAIState(AIState.Flee);
                        return;
                    }
                    if (!CanSee(target))
                    {
                        target = null;
                        currentPatrolPoint = NearestPatrolPoint();
                        ChangeAIState(AIState.Patrol);
                        return;
                    }
                }
                else
                {
                    currentPatrolPoint = NearestPatrolPoint();
                    ChangeAIState(AIState.Patrol);
                }
                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();

                // Check for transitions

                break;
            case AIState.Flee:
                // Do that state's behavior
                DoFleeState();

                // Check for transitions
                if (target != null)
                {
                    if (Vector3.SqrMagnitude(target.transform.position - transform.position) >= attackRange)
                    {
                        ChangeAIState(AIState.Attack);
                        return;
                    }
                    if (!CanSee(target))
                    {
                        target = null;
                        currentPatrolPoint = NearestPatrolPoint();
                        ChangeAIState(AIState.Patrol);
                        return;
                    }
                }
                else
                {
                    currentPatrolPoint = NearestPatrolPoint();
                    ChangeAIState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:
                // Do that state's behavior
                DoPatrolState(true);

                // Check for transitions
                foreach (Controller playerController in GameManager.instance.players)
                {
                    if (playerController.pawn != null && CanSee(playerController.pawn.gameObject))
                    {
                        target = playerController.pawn.gameObject;
                        ChangeAIState(AIState.Flee);
                        return;
                    }
                }
                if (Vector3.SqrMagnitude(patrolPoints[currentPatrolPoint].position - transform.position) < 1.5f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint > patrolPoints.Count - 1)
                    {
                        currentPatrolPoint = 0;
                    }
                    ChangeAIState(AIState.Scan);
                    return;
                }
                break;
            case AIState.Scan:
                // Do that state's behavior
                DoScanState();

                // Check for transitions
                foreach (Controller playerController in GameManager.instance.players)
                {
                    if (playerController.pawn != null && CanSee(playerController.pawn.gameObject))
                    {
                        target = playerController.pawn.gameObject;
                        ChangeAIState(AIState.Flee);
                        return;
                    }
                }
                if (Time.time - lastStateChangeTime > 5f)
                {
                    ChangeAIState(AIState.Patrol);
                    return;
                }
                break;
            case AIState.BackToPost:
                // Do that state's behavior
                DoBackToPostState();

                // Check for transitions
                
                break;
            case AIState.GoToSpot:
                // Do that state's behavior
                DoGoToSpotState();

                // Check for transitions

                break;
            default:
                Debug.LogWarning("AI Controller doesn't have that state implemented");
                break;
        }
    }
}
