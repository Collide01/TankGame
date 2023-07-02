using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(AILazy)), CanEditMultipleObjects]
public class AILazyEditor : Editor
{
    private void OnSceneGUI()
    {
        AIController aiTank = (AIController)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(aiTank.transform.position, Vector3.up, Vector3.forward, 360, aiTank.viewDistance);

        Vector3 viewAngle01 = DirectionFromAngle(aiTank.transform.eulerAngles.y, -aiTank.fieldOfView / 2);
        Vector3 viewAngle02 = DirectionFromAngle(aiTank.transform.eulerAngles.y, aiTank.fieldOfView / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(aiTank.transform.position, aiTank.transform.position + viewAngle01 * aiTank.viewDistance);
        Handles.DrawLine(aiTank.transform.position, aiTank.transform.position + viewAngle02 * aiTank.viewDistance);

        if (aiTank.seeTarget && aiTank.target != null)
        {
            Handles.color = Color.green;
            Handles.DrawLine(aiTank.transform.position, aiTank.target.transform.position);
        }

        Handles.color = Color.blue;
        Handles.DrawWireArc(aiTank.transform.position, Vector3.up, Vector3.forward, 360, aiTank.hearingDistance);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
#endif