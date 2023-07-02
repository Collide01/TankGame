using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAggressive : AIController
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

                    if (CanSee(playerController.gameObject))
                    {
                        Debug.Log("I saw a player");
                        target = playerController.gameObject;
                        ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.gameObject))
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
                //if (Vector3.SqrMagnitude(target.transform.position - transform.position) > attackRange)
                //{
                //    ChangeAIState(AIState.Chase);
                //    return;
                //}
                //if (!CanSee(target))
                //{
                //    target = null;
                //    ChangeAIState(AIState.Scan);
                //    return;
                //}
                break;
            case AIState.Chase:
                // Do that state's behavior
                DoChaseState();

                // Check for transitions
                //if (!CanSee(target))
                //{
                //    target = null;
                //    ChangeAIState(AIState.Scan);
                //    return;
                //}
                //if (Vector3.SqrMagnitude(target.transform.position - transform.position) <= attackRange)
                //{
                //    ChangeAIState(AIState.Attack);
                //    return;
                //}
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
                foreach (Controller playerController in GameManager.instance.players)
                {

                    if (CanSee(playerController.pawn.gameObject))
                    {
                        Debug.Log("I saw a player");
                        target = playerController.gameObject;
                        //ChangeAIState(AIState.Chase);
                        return;
                    }
                    if (CanHear(playerController.pawn.gameObject))
                    {
                        //ChangeAIState(AIState.Scan);
                        return;
                    }
                }
        break;
            case AIState.Scan:
                // Do that state's behavior
                DoScanState();

                // Check for transitions
                //foreach (Controller playerController in GameManager.instance.players)
                //{
                //    if (CanSee(playerController.gameObject))
                //    {
                //        target = playerController.gameObject;
                //        ChangeAIState(AIState.Chase);
                //        return;
                //    }
                //}
                //if (Time.time - lastStateChangeTime > 3f)
                //{
                //    ChangeAIState(AIState.BackToPost);
                //    return;
                //}
                break;
            case AIState.BackToPost:
                // Do that state's behavior
                DoBackToPostState();

                // Check for transitions
                if (Vector3.SqrMagnitude(post.position - transform.position) <= 1f)
                {
                    ChangeAIState(AIState.Idle);
                    return;
                }
                break;
            default:
                Debug.LogWarning("AI Controller doesn't have that state implemented");
                break;
        }
    }
}
