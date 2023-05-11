using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
    }
    // Update is called once per frame
    public virtual void Update()
    {
    }

    public abstract void Move(int direction, float moveSpeed);
    public abstract void Rotate(int direction, float turnSpeed);
}
