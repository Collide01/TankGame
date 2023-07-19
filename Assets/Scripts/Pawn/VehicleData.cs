using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleData : MonoBehaviour
{
    public Mesh meshCollider;
    public float health;
    public float speed;
    public float turnSpeed;
    public Vector3 blasterLocation;
    public Vector3 firePoint;
    public Vector3 specialFirePoint;
    public Vector3 minePoint;

    private void Start()
    {
        if (GetComponentInParent<MeshCollider>() != null)
        {
            GetComponentInParent<MeshCollider>().sharedMesh = meshCollider;
            GetComponentInParent<MeshCollider>().convex = true;
        }
        if (GetComponentInParent<Health>() != null)
        {
            GetComponentInParent<Health>().maxHealth = health;
            GetComponentInParent<Health>().currentHealth = health;
        }
        if (GetComponentInParent<Pawn>() != null)
        {
            GetComponentInParent<Pawn>().moveSpeed = speed;
            GetComponentInParent<Pawn>().turnSpeed = turnSpeed;
        }
        if (GameObject.Find("Blaster") != null)
        {
            GameObject.Find("Blaster").transform.position = blasterLocation;
        }
        if (GameObject.Find("FirePoint") != null)
        {
            GameObject.Find("FirePoint").transform.position = firePoint;
        }
        if (GameObject.Find("SpecialFirePoint") != null)
        {
            GameObject.Find("SpecialFirePoint").transform.position = specialFirePoint;
        }
        if (GameObject.Find("MinePoint") != null)
        {
            GameObject.Find("MinePoint").transform.position = minePoint;
        }
    }
}
