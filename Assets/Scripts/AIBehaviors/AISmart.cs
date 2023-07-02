using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISmart : AIController
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
                foreach (Controller playerController in GameManager.instance.players)
                {
                    if (playerController.pawn != null && CanHear(playerController.pawn.gameObject))
                    {
                        heardPosition = playerController.pawn.transform.position;
                        ChangeAIState(AIState.GoToSpot);
                        return;
                    }
                }
                break;
            case AIState.Attack:
                // Do that state's behavior
                DoAttackState();

                // Check for transitions
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) > attackRange)
                {
                    ChangeAIState(AIState.Chase);
                    return;
                }
                if (!CanSee(target))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }
                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();

                // Check for transitions
                if (Vector3.Magnitude(post.position - transform.position) > 20f)
                {
                    ChangeAIState(AIState.BackToPost);
                    return;
                }
                if (Vector3.SqrMagnitude(target.transform.position - transform.position) <= attackRange)
                {
                    ChangeAIState(AIState.Attack);
                    return;
                }
                if (!CanSee(target))
                {
                    target = null;
                    ChangeAIState(AIState.Scan);
                    return;
                }
                break;
            case AIState.Flee:
                // Do that state's behavior
                DoFleeState();

                // Check for transitions

                break;
            case AIState.Patrol:
                // Do that state's behavior
                DoPatrolState();

                // Check for transitions

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
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                }
                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.BackToPost);
                    return;
                }
                break;
            case AIState.BackToPost:
                // Do that state's behavior
                DoBackToPostState();

                // Check for transitions
                if (Vector3.Magnitude(post.position - transform.position) <= 1f)
                {
                    ChangeAIState(AIState.Idle);
                    return;
                }
                break;
            case AIState.GoToSpot:
                // Do that state's behavior
                DoGoToSpotState();

                // Check for transitionss
                if (Vector3.Magnitude(heardPosition - transform.position) <= 1f)
                {
                    ChangeAIState(AIState.Scan);
                    return;
                }
                foreach (Controller playerController in GameManager.instance.players)
                {
                    if (playerController.pawn != null && CanSee(playerController.pawn.gameObject))
                    {
                        target = playerController.pawn.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                }
                break;
            default:
                Debug.LogWarning("AI Controller doesn't have that state implemented");
                break;
        }
    }
}
