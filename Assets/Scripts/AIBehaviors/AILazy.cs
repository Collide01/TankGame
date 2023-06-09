using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AILazy : AIController
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
                        ChangeAIState(AIState.Scan);
                        return;
                    }
                }
                break;
            case AIState.Attack:
                // Do that state's behavior
                DoAttackState();

                // Check for transitions
                if (!CanSee(target) || target == null)
                {
                    target = null;
                    ChangeAIState(AIState.Idle);
                    return;
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
                        ChangeAIState(AIState.Attack);
                        return;
                    }
                }
                if (Time.time - lastStateChangeTime > 3f)
                {
                    ChangeAIState(AIState.Idle);
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
