using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Instance of GameManager singleton
    [HideInInspector] public static GameManager instance;

    private bool spawnedObjects;

    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;

    // List that holds our player(s)
    public List<PlayerController> players;
    // List that holds our AI
    public List<AIController> aiControllers;
    // List that holds our controller(s)
    public List<Controller> controllers;
    // List that holds our pawn(s)
    public List<Pawn> pawns;
    public List<PawnSpawnPoint> pawnSpawnPoints = new List<PawnSpawnPoint>();

    [Header("Prefabs")]
    public GameObject ambulance;
    public GameObject delivery;
    public GameObject deliveryFlat;
    public GameObject fireTruck;
    public GameObject garbageTruck;
    public GameObject hatchbackSports;
    public GameObject police;
    public GameObject race;
    public GameObject raceFuture;
    public GameObject sedan;
    public GameObject sedanSports;
    public GameObject suv;
    public GameObject suvLuxury;
    public GameObject taxi;
    public GameObject tractor;
    public GameObject tractorPolice;
    public GameObject tractorShovel;
    public GameObject truck;
    public GameObject truckFlat;
    public GameObject van;
    public GameObject blasterA;
    public GameObject blasterB;
    public GameObject blasterC;
    public GameObject blasterD;
    public GameObject blasterE;
    public GameObject blasterF;
    public GameObject blasterG;
    public GameObject blasterH;
    public GameObject blasterI;
    public GameObject blasterJ;
    public GameObject blasterK;
    public GameObject blasterL;
    public GameObject blasterM;
    public GameObject blasterN;
    public GameObject blasterO;
    public GameObject blasterP;
    public GameObject blasterQ;
    public GameObject blasterR;

    // Awake is called when the object is first created - before even Start can run!
    private void Awake()
    {
        // If the instance doesn't exist yet...
        if (instance == null)
        {
            // This is the instance
            instance = this;
            //Don't destroy it if we load a new scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Otherwise, there is already an instance, so destroy this gameObject
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnedObjects && pawnSpawnPoints.Count > 0)
        {
            SpawnPlayer();
            for (int i = 0; i < pawnSpawnPoints.Count - 1; i++)
            {
                SpawnAI();
            }
            spawnedObjects = true;
        }
    }

    public void SpawnPlayer()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while (spawn.spawnedPawn == null)
        {
            spawn = GetRandomSpawnPoint();
        }
        spawn.spawned = true;

        // Spawn the Player Controller at the spawn point
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, spawn.gameObject.transform.position, spawn.gameObject.transform.rotation) as GameObject;

        // Spawn the Pawn and connect it to the Controller
        GameObject newPawnObj = Instantiate(spawn.spawnedPawn.gameObject, spawn.gameObject.transform.position, spawn.gameObject.transform.rotation) as GameObject;

        // Get the Player Controller component and Pawn component. 
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        // Hook them up!
        newController.pawn = newPawn;
    }

    public void SpawnAI()
    {
        PawnSpawnPoint spawn = GetRandomSpawnPoint();
        while (spawn.aiPawns.Count == 0 || spawn.spawned)
        {
            spawn = GetRandomSpawnPoint();
        }
        spawn.spawned = true;

        // Choose a random behavior for the AI
        int randomBehavior = Random.Range(0, 4);

        // Spawn the AI prefab
        GameObject aiPawn = Instantiate(spawn.aiPawns[randomBehavior], spawn.gameObject.transform.position, spawn.gameObject.transform.rotation);

        // Pass the patrol points to the AI
        aiPawn.GetComponent<AIController>().patrolPoints = spawn.patrolPoints;
    }

    private PawnSpawnPoint GetRandomSpawnPoint()
    {
        return pawnSpawnPoints[Random.Range(0, pawnSpawnPoints.Count)];
    }
}
