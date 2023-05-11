using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the Update() function from the parent (base) class
        base.Update();
    }

    public override void Move(int direction, float moveSpeed)
    {
        gameObject.GetComponent<Rigidbody>()
            .MovePosition(new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z + direction * moveSpeed * Time.deltaTime));
    }

    public override void Rotate(int direction, float turnSpeed)
    {
        gameObject.transform.Rotate(new Vector3(0, direction * turnSpeed * Time.deltaTime, 0));
    }
}
