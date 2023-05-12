using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    // Variable to hold our Pawn
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // If we have a GameManager
        if (GameManager.instance != null)
        {
            // And it tracks the player(s)
            if (GameManager.instance.controllers != null)
            {
                // Register with the GameManager
                GameManager.instance.controllers.Add(this);
            }
        }
    }
    // Update is called once per frame
    public virtual void Update()
    {
    }
}
