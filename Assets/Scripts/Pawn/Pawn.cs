using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Pawn : MonoBehaviour
{
    // Variable to hold the Rigidbody Component
    protected Rigidbody rb;

    [HideInInspector] public int playerNumber; // 0 if this pawn isn't controlled by a player
    [HideInInspector] public Mover mover;
    [HideInInspector] public Shooter shooter;
    [HideInInspector] public Health health;
    [HideInInspector] public NoiseMaker noiseMaker;
    public Image healthBar;

    // Variable for move speed
    public float moveSpeed;
    // Variable for turn speed
    public float turnSpeed;

    public Transform firepointTransform;

    // Variable for our shell prefab
    public GameObject shellPrefab;
    // Variable for our firing force
    public float fireForce;
    // Variable for our damage done
    public float damageDone;
    // Variable for how long our bullets survive if they don't collide
    public float shellLifespan;

    // Variable for Rate of Fire
    [HideInInspector] public float fireRate;
    public float shotsPerSecond;

    public int pointsOnKilled = 100;

    // Variables for making noise
    public float moveNoise;
    public float shootNoise;
    public float specialShotNoise;

    public enum SpecialShotType
    {
        BouncyShot,
        LaserBeam,
        Mine
    }
    public SpecialShotType specialShotType;
    // Variable for our special shot prefab
    public GameObject specialShotPrefab;

    // Variable for the special shot's charge time
    public float specialChargeTime; // In seconds
    public Transform specialFirepointTransform;
    public Transform mineTransform;
    // Variable for how long the special shot lasts; only applies to laser beam and mine
    public float specialLifespan;
    [HideInInspector] public float specialShotTimer;
    [HideInInspector] public bool overcharge;

    [HideInInspector] public PawnSpawnPoint spawnPoint;

    [SerializeField] protected GameObject tankAudioPrefab;

    public abstract void RotateTowards(Vector3 targetPosition, float avoidanceSpeed = 0);

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
        noiseMaker = GetComponent<NoiseMaker>();

        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.pawns != null)
            {
                // Register with the GameManager
                GameManager.instance.pawns.Add(this);
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (transform.position.y < -10)
        {
            health.TakeDamage(999, this);
        }

        healthBar.fillAmount = health.currentHealth / health.maxHealth;
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void Rotate(float setTurnSpeed);
    public abstract void Shoot();
    public abstract void SpecialShoot();
    public abstract void StayStill();

    public void OnDestroy()
    {
        if (spawnPoint != null)
        {
            spawnPoint.spawned = false;
        }

        //GameObject tankAudio = Instantiate(tankAudioPrefab, transform.position, Quaternion.identity);
        //tankAudio.GetComponent<GameAudioSource>().PlayAudio(2);

        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.pawns != null)
            {
                // Deregister with the GameManager
                GameManager.instance.pawns.Remove(this);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Vehicle")
        {
            health.TakeDamage(999, this);
            collision.gameObject.GetComponent<Health>().TakeDamage(999, collision.gameObject.GetComponent<Pawn>());
        }
    }
}
