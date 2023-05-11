using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{
    private Vector2 vInput;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process out inputs
        Debug.Log(vInput);

        // Run the Update() function from the parent (base) class
        base.Update();
    }

    private void OnMove(InputValue value)
    {
        vInput = value.Get<Vector2>();
    }
}
