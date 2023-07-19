using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public Pawn spawnedPawn;
    public List<GameObject> aiPawns;
    public List<Transform> patrolPoints;
    [HideInInspector] public bool spawned;

    private void Start()
    {
        GameManager.instance.pawnSpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.pawnSpawnPoints.Remove(this);
    }
}
