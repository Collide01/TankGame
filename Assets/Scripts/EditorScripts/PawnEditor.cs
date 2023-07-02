using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TankPawn)), CanEditMultipleObjects]
public class PawnEditor : Editor
{

    public SerializedProperty
        // Values that appear all the time
        moveSpeed_Pawn,
        turnSpeed_Pawn,
        firepointTransform_Pawn,
        shellPrefab_Pawn,
        fireForce_Pawn,
        damageDone_Pawn,
        shellLifespawn_Pawn,
        shotsPerSecond_Pawn,
        moveNoise_Pawn,
        shootNoise_Pawn,
        specialShotNoise_Pawn,
        sShotPrefab_Pawn,
        sChargeTime_Pawn,
        // Values that appear depending on the special shot
        state_Pawn,
        sFirepoint_Pawn,
        mFirepoint_Pawn,
        lifespawn_Pawn;

    void OnEnable()
    {
        // Setup the SerializedProperties
        // Always show up
        moveSpeed_Pawn = serializedObject.FindProperty("moveSpeed");
        turnSpeed_Pawn = serializedObject.FindProperty("turnSpeed");
        firepointTransform_Pawn = serializedObject.FindProperty("firepointTransform");
        shellPrefab_Pawn = serializedObject.FindProperty("shellPrefab");
        fireForce_Pawn = serializedObject.FindProperty("fireForce");
        damageDone_Pawn = serializedObject.FindProperty("damageDone");
        shellLifespawn_Pawn = serializedObject.FindProperty("shellLifespan");
        shotsPerSecond_Pawn = serializedObject.FindProperty("shotsPerSecond");
        sShotPrefab_Pawn = serializedObject.FindProperty("specialShotPrefab");
        sChargeTime_Pawn = serializedObject.FindProperty("specialChargeTime");
        moveNoise_Pawn = serializedObject.FindProperty("moveNoise");
        shootNoise_Pawn = serializedObject.FindProperty("shootNoise");
        specialShotNoise_Pawn = serializedObject.FindProperty("specialShotNoise");
        // Based on special shot
        state_Pawn = serializedObject.FindProperty("specialShotType");
        sFirepoint_Pawn = serializedObject.FindProperty("specialFirepointTransform");
        mFirepoint_Pawn = serializedObject.FindProperty("mineTransform");
        lifespawn_Pawn = serializedObject.FindProperty("specialLifespan");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(moveSpeed_Pawn);
        EditorGUILayout.PropertyField(turnSpeed_Pawn);
        EditorGUILayout.PropertyField(firepointTransform_Pawn);
        EditorGUILayout.PropertyField(shellPrefab_Pawn);
        EditorGUILayout.PropertyField(fireForce_Pawn);
        EditorGUILayout.PropertyField(damageDone_Pawn);
        EditorGUILayout.PropertyField(shellLifespawn_Pawn);
        EditorGUILayout.PropertyField(shotsPerSecond_Pawn);
        EditorGUILayout.PropertyField(moveNoise_Pawn);
        EditorGUILayout.PropertyField(shootNoise_Pawn);
        EditorGUILayout.PropertyField(specialShotNoise_Pawn);

        EditorGUILayout.PropertyField(state_Pawn);
        EditorGUILayout.PropertyField(sShotPrefab_Pawn);
        EditorGUILayout.PropertyField(sChargeTime_Pawn);

        Pawn.SpecialShotType type = (Pawn.SpecialShotType)state_Pawn.enumValueIndex;

        switch (type)
        {
            case Pawn.SpecialShotType.BouncyShot:
                EditorGUILayout.PropertyField(sFirepoint_Pawn);
                break;

            case Pawn.SpecialShotType.LaserBeam:
                EditorGUILayout.PropertyField(sFirepoint_Pawn);
                EditorGUILayout.PropertyField(lifespawn_Pawn);
                break;

            case Pawn.SpecialShotType.Mine:
                EditorGUILayout.PropertyField(mFirepoint_Pawn);
                EditorGUILayout.PropertyField(lifespawn_Pawn);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}