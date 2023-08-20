using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TankMover : Mover
{
    // Variable to hold the Rigidbody Component
    private Rigidbody rb;

    // Start is called before the first frame update
    public void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        if (rb != null)
        {
            Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
            if (rb.velocity.magnitude < speed)
            {
                rb.AddForce(moveVector, ForceMode.Impulse);
            }
            base.Move(direction, speed);
        }
    }

    public override void Rotate(float speed)
    {
        gameObject.transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
        base.Rotate(speed);
    }
}
