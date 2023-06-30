using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public Pawn spawnedPawn;

    private void Start()
    {
        GameManager.instance.pawnSpawnPoints.Add(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.pawnSpawnPoints.Remove(this);
    }
}
