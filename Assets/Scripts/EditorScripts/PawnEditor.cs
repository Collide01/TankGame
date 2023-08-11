using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(TankPawn)), CanEditMultipleObjects]
public class PawnEditor : Editor
{
    private void OnSceneGUI()
    {
        Pawn tank = (Pawn)target;

        if (tank.noiseMaker != null)
        {
            Handles.color = Color.cyan;
            Handles.DrawWireArc(tank.transform.position, Vector3.up, Vector3.forward, 360, tank.noiseMaker.volumeDistance);
        }
    }
}
#endif